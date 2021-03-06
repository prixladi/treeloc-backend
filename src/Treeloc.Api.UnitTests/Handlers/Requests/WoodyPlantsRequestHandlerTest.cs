﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using NSubstitute;
using TreeLoc;
using TreeLoc.Api.Handlers.Requests;
using TreeLoc.Api.Models;
using TreeLoc.Api.Repositories;
using TreeLoc.Api.Requests.WoodyPlants;
using TreeLoc.Database.Documents;
using TreeLoc.Database.Documents.Locations;
using TreeLoc.Exceptions;
using Xunit;

namespace Treeloc.Api.UnitTests.Handlers.Requests
{
  public class WoodyPlantsRequestHandlerTest
  {
    private readonly IWoodyPlantsRepository fWoodyPlantsRepository;
    private readonly IVersionRepository fVersionRepository;

    public WoodyPlantsRequestHandlerTest()
    {
      fWoodyPlantsRepository = Substitute.For<IWoodyPlantsRepository>();
      fVersionRepository = Substitute.For<IVersionRepository>();
    }

    [Fact]
    public async Task HandleGetByFilter_TestAsync()
    {
      var filter = new WoodyPlantFilterModel();
      var sort = new WoodyPlantSortModel();
      var request = new GetWoodyPlantsByFilterRequest(filter, sort);
      var id = ObjectId.GenerateNewId();
      var expectedPlants = new List<WoodyPlantDocument>() { new WoodyPlantDocument { Id = id } };

      fWoodyPlantsRepository
        .GetByFilterAsync(Arg.Is(filter), Arg.Is(sort), Arg.Is(default(CancellationToken)))
        .Returns(expectedPlants);

      fWoodyPlantsRepository
        .CountByFilterAsync(Arg.Is(filter), Arg.Is(default(CancellationToken)))
        .Returns(56);

      var result = await new WoodyPlantsRequestHandler(fWoodyPlantsRepository, fVersionRepository).Handle(request, default);

      Assert.NotNull(result);
      Assert.Equal(expectedPlants.Count, result.WoodyPlants.Count);
      Assert.Equal(expectedPlants[0].Id, result.WoodyPlants[0].Id);
      Assert.Equal(56, result.TotalCount);
      Assert.Null(result.DataVersion);
    }

    [Fact]
    public async Task HandleGetByFilter_OptimalizedGet_TestAsync()
    {
      var filter = new WoodyPlantFilterModel
      {
        Distance = null,
        Skip = 0,
        Text = null,
        Point = new Point
        {
          Latitude = 50,
          Longitude = 50
        },
        Take = 70
      };

      var sort = new WoodyPlantSortModel
      {
        SortBy = null,
        Ascending = true
      };

      var request = new GetWoodyPlantsByFilterRequest(filter, sort);
      var id = ObjectId.GenerateNewId();
      var expectedPlants = new List<WoodyPlantDocument>() { new WoodyPlantDocument { Id = id } };

      fWoodyPlantsRepository
        .GetWithCoordsAsync(Arg.Is(default(CancellationToken)))
        .Returns(expectedPlants);

      fWoodyPlantsRepository
        .CountByFilterAsync(Arg.Is(filter), Arg.Is(default(CancellationToken)))
        .Returns(1);

      var result = await new WoodyPlantsRequestHandler(fWoodyPlantsRepository, fVersionRepository).Handle(request, default);

      Assert.NotNull(result);
      Assert.True(sort.Ascending);
      Assert.Equal(expectedPlants.Count, result.WoodyPlants.Count);
      Assert.Equal(expectedPlants[0].Id, result.WoodyPlants[0].Id);
      Assert.Equal(1, result.TotalCount);
    }

    [Fact]
    public async Task HandleGet_Exception_TestAsync()
    {
      var id = ObjectId.GenerateNewId();
      var request = new GetWoodyPlantRequest(id);
      var expectedPlant = new WoodyPlantDocument { Id = id };

      fWoodyPlantsRepository
        .GetByIdAsync(Arg.Is(id), Arg.Is(default(CancellationToken)))
        .Returns((WoodyPlantDocument?)null);

      await Assert.ThrowsAsync<NotFoundException>(async () => await new WoodyPlantsRequestHandler(fWoodyPlantsRepository, fVersionRepository).Handle(request, default));
    }

    [Fact]
    public async Task HandleGet_Success_TestAsync()
    {
      var id = ObjectId.GenerateNewId();
      var request = new GetWoodyPlantRequest(id);
      var expectedPlant = new WoodyPlantDocument
      {
        Id = id,
        TextMatchScore = 5,
        ImageUrls = new[] { "http://obj.cz" },
        Location = new LocationDocument { Name = "Loc", Geometry = new PointGeometry() },
        InnerWoodyPlantIds = Array.Empty<ObjectId>(),
        LocalizedNames = new LocalizedStringDocument { Czech = "name" },
        LocalizedNotes = new LocalizedStringDocument { Czech = "note" },
        LocalizedSpecies = new LocalizedStringDocument { Czech = "specie" },
        Type  = PlantType.AreaOfTrees,
        Version = null,
      };

      fWoodyPlantsRepository
        .GetByIdAsync(Arg.Is(id), Arg.Is(default(CancellationToken)))
        .Returns(expectedPlant);

      var result = await new WoodyPlantsRequestHandler(fWoodyPlantsRepository, fVersionRepository).Handle(request, default);

      Assert.NotNull(result);
      Assert.Equal(expectedPlant.Id, result.Id);
      Assert.Equal(expectedPlant.LocalizedSpecies.Czech, result.LocalizedSpecies.Czech);
      Assert.Equal(expectedPlant.LocalizedNames.Czech, result.LocalizedNames.Czech);
      Assert.Equal(expectedPlant.LocalizedNotes.Czech, result.LocalizedNotes.Czech);
      Assert.Equal(expectedPlant.Location.Name, result.Location!.Name);
      Assert.Equal(expectedPlant.Location.Geometry.Type, result.Location.Geometry!.Type);
      var image = Assert.Single(result.ImageUrls);
      Assert.Equal("http://obj.cz", image);
    }
  }
}
