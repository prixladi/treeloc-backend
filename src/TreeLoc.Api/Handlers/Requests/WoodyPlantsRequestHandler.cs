﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TreeLoc.Api.Extensions;
using TreeLoc.Api.Models;
using TreeLoc.Api.Repositories;
using TreeLoc.Api.Requests.WoodyPlants;
using TreeLoc.Exceptions;

namespace TreeLoc.Api.Handlers.Requests
{
  public class WoodyPlantsRequestHandler:
    IRequestHandler<GetWoodyPlantRequest, WoodyPlantDetailModel>,
    IRequestHandler<GetWoodyPlantsByFilterRequest, WoodyPlantListModel>
  {
    private readonly IWoodyPlantsRepository fWoodyPlantsRepository;
    private readonly IVersionRepository fVersionRepository;

    public WoodyPlantsRequestHandler(IWoodyPlantsRepository woodyPlantsRepository, IVersionRepository versionRepository)
    {
      fWoodyPlantsRepository = woodyPlantsRepository;
      fVersionRepository = versionRepository;
    }

    public async Task<WoodyPlantDetailModel> Handle(GetWoodyPlantRequest request, CancellationToken cancellationToken)
    {
      var plant = await fWoodyPlantsRepository.GetByIdAsync(request.PlantId, cancellationToken);
      if (plant == null)
        throw new NotFoundException($"Woody plant with ID '{request.PlantId}' was not found.");

      return plant.ToDetail();
    }

    public async Task<WoodyPlantListModel> Handle(GetWoodyPlantsByFilterRequest request, CancellationToken cancellationToken)
    {
      var versionDoc = await fVersionRepository.GetSingleAsync(cancellationToken);
      if (await IsFillCoordsFilterAsync(request.Filter, request.Sort, cancellationToken))
      {
        var all = await fWoodyPlantsRepository.GetWithCoordsAsync(cancellationToken);
        return new WoodyPlantListModel
        {
          TotalCount = all.Count,
          WoodyPlants = all.ToPreview(),
          DataVersion = versionDoc?.Version
        };
      }

      var plants = await fWoodyPlantsRepository.GetByFilterAsync(request.Filter, request.Sort, cancellationToken);
      return new WoodyPlantListModel
      {
        TotalCount = await fWoodyPlantsRepository.CountByFilterAsync(request.Filter, cancellationToken),
        WoodyPlants = plants.ToPreview(),
        DataVersion = versionDoc?.Version
      };
    }

    private async Task<bool> IsFillCoordsFilterAsync(WoodyPlantFilterModel filter, WoodyPlantSortModel sort, CancellationToken cancellationToken)
    {
      var pointFilter = filter.Distance == null
        && filter.Text == null
        && filter.Skip == 0
        && sort.SortBy == null
        && filter.Point != null;

      if (!pointFilter)
        return false;

      var count = await fWoodyPlantsRepository.CountWithCoordsAsync(cancellationToken);

      return count <= filter.Take;
    }
  }
}
