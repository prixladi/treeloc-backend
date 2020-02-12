using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using TreeLoc.Database;
using TreeLoc.Database.Documents;
using TreeLoc.Database.Documents.Locations;

namespace TreeLoc.Api.Services
{
  public class DbInitializationService: IHostedService
  {
    private readonly DbContext fDbContext;

    public DbInitializationService(DbContext dbContext)
    {
      fDbContext = dbContext;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
      var collection = fDbContext.Database.GetCollection<WoodyPlantDocument>();
      var indexManager = collection.Indexes;
      await indexManager.CreateManyAsync(GetWoodyPlantsIndexes());

      await FillDbAsync(collection, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }

    private IEnumerable<CreateIndexModel<WoodyPlantDocument>> GetWoodyPlantsIndexes()
    {
      var builder = Builders<WoodyPlantDocument>.IndexKeys;

      yield return new CreateIndexModel<WoodyPlantDocument>(builder.Combine(
        builder.Text(x => x.LocalizedNames.Czech),
        builder.Text(x => x.LocalizedNotes.Czech),
        builder.Text(x => x.LocalizedSpecies.Czech)),
        new CreateIndexOptions { LanguageOverride = "none" });

      yield return new CreateIndexModel<WoodyPlantDocument>(builder.Ascending(x => x.LocalizedSpecies.Czech), new CreateIndexOptions { Collation = new Collation("cs") });
      yield return new CreateIndexModel<WoodyPlantDocument>(builder.Ascending(x => x.LocalizedNames.Czech), new CreateIndexOptions { Collation = new Collation("cs") });
      yield return new CreateIndexModel<WoodyPlantDocument>(builder.Ascending(x => x.LocalizedNotes.Czech), new CreateIndexOptions { Collation = new Collation("cs") });
      yield return new CreateIndexModel<WoodyPlantDocument>(builder.Geo2DSphere(x => x.Location!.Geometry));
    }

    private async Task FillDbAsync(IMongoCollection<WoodyPlantDocument> collection, CancellationToken cancellationToken)
    {
      if (await collection.AsQueryable().AnyAsync(cancellationToken))
        return;

      var random = new Random();

      for (int i = 0; i < 3; i++)
      {
        await collection.InsertManyAsync(new WoodyPlantDocument[]
        {
          new WoodyPlantDocument
          {
            LocalizedNames = new LocalizedString { Czech = "Lípa u nemocnice" },
            LocalizedNotes = new LocalizedString { Czech = "Je hodně stará"},
            LocalizedSpecies = new LocalizedString { Czech = "Lípa srdčitá" },
            ImageUrl = "https://google.com",
            Location = new Location
            {
              Geometry = GetPointGeometry(random)
            }
          },
          new WoodyPlantDocument
          {
            LocalizedNames = new LocalizedString { Czech = "Smrky u Netolic" },
            LocalizedNotes = new LocalizedString { Czech = "Pár stromů, žádnej les"},
            LocalizedSpecies = new LocalizedString { Czech = "Smrk obecný" },
            ImageUrl = "https://google.com",
            Location = new Location
            {
              Geometry = GetMultiPointGeometry(random)
            }
          },
          new WoodyPlantDocument
          {
            LocalizedNotes = new LocalizedString { Czech = "Nevíme jak to pojmenovat"},
            LocalizedSpecies = new LocalizedString { Czech = "Dub" },
            ImageUrl = "https://google.com",
            Location = new Location
            {
              Geometry = GetLineStringGeometry(random)
            }
          },
          new WoodyPlantDocument
          {
            LocalizedNotes = new LocalizedString { Czech = "No idea co to je ale je to tam" },
            ImageUrl = "https://google.com",
            Location = new Location
            {
              Geometry = GetMultiLineStringGeometry(random)
            }
          },
          new WoodyPlantDocument
          {
            LocalizedNames = new LocalizedString { Czech = "Lesík u Vitějovic" },
            LocalizedNotes = new LocalizedString { Czech = "Malý lesík"},
            ImageUrl = "https://google.com",
            Location = new Location
            {
              Geometry = GetPolygonGeometry(random)
            }
          },
          new WoodyPlantDocument
          {
            LocalizedNames = new LocalizedString { Czech = "Lesy u" },
            LocalizedNotes = new LocalizedString { Czech = "Skupina lesů u"},
            ImageUrl = "https://google.com",
            Location = new Location
            {
              Geometry = GetMultiPolygonGeometry(random)
            }
          }
        });
      }
    }

    private PointGeometry GetPointGeometry(Random random)
    {
      return new PointGeometry
      {
        Coordinates = GetPoint(random)
      };
    }

    private double[] GetPoint(Random random)
    {
      return new double[] { 12.661745182000601 + random.NextDouble() * 5, 48.806820963248754 + random.NextDouble() * 2 };
    }


    private MultiPointGeometry GetMultiPointGeometry(Random random)
    {
      return new MultiPointGeometry { Coordinates = GetMultiPoint(random) };
    }

    private double[][] GetMultiPoint(Random random)
    {
      return new double[][] { GetPoint(random), GetPoint(random) };
    }

    private LineStringGeometry GetLineStringGeometry(Random random)
    {
      return new LineStringGeometry { Coordinates = GetLineString(random) };
    }

    private double[][] GetLineString(Random random)
    {
      return new double[][] { GetPoint(random), GetPoint(random) };
    }

    private MultiLineStringGeometry GetMultiLineStringGeometry(Random random)
    {
      return new MultiLineStringGeometry { Coordinates = GetMultiLineString(random) };
    }

    private double[][][] GetMultiLineString(Random random)
    {
      return new double[][][] { GetLineString(random), GetLineString(random) };
    }

    private PolygonGeometry GetPolygonGeometry(Random random)
    {
      return new PolygonGeometry { Coordinates = GetPolygon(random) };
    }

    private double[][][] GetPolygon(Random random)
    {
      var start = new double[] { 12.661745182000601 + random.NextDouble(), 48.806820963248754 + random.NextDouble() / 5 };
      var points = new double[][]
      {
        start,
        new double[] { 13.661745182000601 + random.NextDouble(), 48.806820963248754 + random.NextDouble() / 5 },
        new double[] { 13.661745182000601 + random.NextDouble(), 49.106820963248754 + random.NextDouble() / 5 },
        new double[] { 12.661745182000601 + random.NextDouble(), 49.106820963248754 + random.NextDouble() / 5 },
        start
      };

      return new double[][][] { points };
    }

    private MultiPolygonGeometry GetMultiPolygonGeometry(Random random)
    {
      return new MultiPolygonGeometry { Coordinates = GetMultiPolygon(random) };
    }

    private double[][][][] GetMultiPolygon(Random random)
    {
      return new double[][][][] { GetPolygon(random), GetPolygon(random), GetPolygon(random), GetPolygon(random), };
    }
  }
}
