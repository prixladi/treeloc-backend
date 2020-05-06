using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TreeLoc.Loader.Configs;
using TreeLoc.Loader.Repositories;
using TreeLoc.Loader.Services;
using TreeLoc.Loader.SignalR;
using TreeLoc.OFN;
using Xunit;

namespace TreeLoc.Loader.UnitTests.Services
{
  public class LoaderServiceTest
  {
    private readonly IHttpService fHttpService;
    private readonly ILoaderServiceConfig fConfig;
    private readonly IResourcesRepository fResourcesRepository;
    private readonly IVersionRepository fVersionRepository;
    private readonly IWoodyPlantRepository fWoodyPlantRepository;
    private readonly IHubContext<ClientHub> fHubContext;

    private readonly LoaderService fService;

    public LoaderServiceTest()
    {
      fHttpService = Substitute.For<IHttpService>();
      fConfig = Substitute.For<ILoaderServiceConfig>();
      fResourcesRepository = Substitute.For<IResourcesRepository>();
      fVersionRepository = Substitute.For<IVersionRepository>();
      fWoodyPlantRepository = Substitute.For<IWoodyPlantRepository>();
      fHubContext = Substitute.For<IHubContext<ClientHub>>();

      fService = new LoaderService(fHttpService, fConfig, fResourcesRepository, fVersionRepository, fWoodyPlantRepository, fHubContext);
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public async Task RunService_Success_TestAsync(WoodyPlant[] plants)
    {
      fConfig
        .Interval
        .Returns(TimeSpan.FromSeconds(7));

      fResourcesRepository
        .GetFalseAsync(Arg.Any<CancellationToken>())
        .Returns(new List<string> { "http://dot.com/" });

      fHttpService
        .LoadAsync(Arg.Is<Uri>(x => x.AbsoluteUri == "http://dot.com/"), Arg.Any<CancellationToken>())
        .Returns(plants);

      await fService.StartAsync(default);
      await Task.Delay(600);

      await fResourcesRepository
        .Received()
        .SetTrueAsync(Arg.Is("http://dot.com/"), Arg.Any<CancellationToken>());

      await fService.StopAsync(default);
      await fService.StopAsync(default);
    }

    [Fact]
    public async Task RunService_Exception_TestAsync()
    {
      fConfig
        .Interval
        .Returns(TimeSpan.FromSeconds(7));

      fConfig
        .RemoveOld
        .Returns(false);

      fResourcesRepository
        .GetFalseAsync(Arg.Any<CancellationToken>())
        .Returns(new List<string> { "http://dot.com/" });

      fHttpService
        .LoadAsync(Arg.Is<Uri>(x => x.AbsoluteUri == "http://dot.com/"), Arg.Any<CancellationToken>())
        .Throws<Exception>();

      await fService.StartAsync(default);
      await Task.Delay(600);

      await fService.StopAsync(default);
      await fService.StopAsync(default);
    }

    public static IEnumerable<object[]> GetData()
    {
      yield return new object[]
      {
        new WoodyPlant[]
        {
          new WoodyPlant
          {
            ImageUrls = Array.Empty<string>(),
            Type = "Plant",
            WoodyPlants = Array.Empty<WoodyPlant>(),
            LocalizedNames = new LocalizedString { Czech = "Name" },
            LocalizedNotes = new LocalizedString { Czech = "Note"},
            LocalizedSpecies = new LocalizedString { Czech = "Species" },
            Location = new Location
            {
              Name = "Location",
              Geometry = new Geometry
              {
                Type = "Point",
                Coordinates = JArray.FromObject(new double[2])
              }
            }
          },
          new WoodyPlant
          {
            Location = new Location
            {
              Geometry = new Geometry
              {
                Type = "MultiPoint",
                Coordinates =  JArray.FromObject(new JArray[] { JArray.FromObject(new double[2]) })
              }
            }
          },
          new WoodyPlant
          {
            Location = new Location
            {
              Geometry = new Geometry
              {
                Type = "LineString",
                Coordinates = JArray.FromObject(new JArray[] { JArray.FromObject(new double[2]) })
              }
            }
          },
          new WoodyPlant
          {
            Location = new Location
            {
              Geometry = new Geometry
              {
                Type = "MultiLineString",
                Coordinates = JArray.FromObject(new JArray[] { JArray.FromObject( new JArray[] { JArray.FromObject(new double[2]) }) })
              }
            }
          },
          new WoodyPlant
          {
            Location = new Location
            {
              Geometry = new Geometry
              {
                Type = "Polygon",
                Coordinates = JArray.FromObject(new JArray[] { JArray.FromObject( new JArray[] { JArray.FromObject(new double[2]) }) })
              }
            }
          },
          new WoodyPlant
          {
            Location = new Location
            {
              Geometry = new Geometry
              {
                Type = "MultiPolygon",
                Coordinates = JArray.FromObject(new JArray[] { JArray.FromObject( new JArray[] { JArray.FromObject(new JArray[] { JArray.FromObject(new double[2]) }) }) })
              }
            }
          }
        }
      };
    }
  }
}
