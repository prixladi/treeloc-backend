using System;
using System.Collections.Generic;
using System.Linq;
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

      var list = new List<WoodyPlantDocument>();

      for (int i = 0; i < 7000; i++)
      {
        list.Add(GetPointDocument(random));
      }

      for (int i = 0; i < 1000; i++)
      {
        list.Add(new WoodyPlantDocument
        {
          LocalizedNotes = new LocalizedString { Czech = "Nevíme jak to pojmenovat" },
          LocalizedSpecies = new LocalizedString { Czech = "Dub" },
          ImageUrl = "https://google.com",
          Location = new Location
          {
            Geometry = GetLineStringGeometry(random)
          }
        });
      }

      for (int i = 0; i < 0; i++)
      {
        list.Add(new WoodyPlantDocument
        {
          LocalizedNames = new LocalizedString { Czech = "Lesík u Vitějovic" },
          LocalizedNotes = new LocalizedString { Czech = "Malý lesík" },
          ImageUrl = "https://google.com",
          Location = new Location
          {
            Geometry = GetPolygonGeometry(random)
          }
        });
      }

        await collection.InsertManyAsync(list, cancellationToken: cancellationToken);
    }

    private WoodyPlantDocument GetPointDocument(Random random)
    {
      var number = random.Next(1, 5);

      switch (number)
      {
        case 1:
          return new WoodyPlantDocument
          {
            LocalizedNames = new LocalizedString { Czech = "Lípa u nemocnice" },
            LocalizedNotes = new LocalizedString { Czech = "Je hodně stará" },
            LocalizedSpecies = new LocalizedString { Czech = "Lípa srdčitá" },
            ImageUrl = "https://google.com",
            Location = new Location
            {
              Geometry = GetPointGeometry(random)
            }
          };
        case 2:
          return new WoodyPlantDocument
          {
            LocalizedNames = new LocalizedString { Czech = "Smrk v lesíku" },
            LocalizedSpecies = new LocalizedString { Czech = "Smrk obecný" },
            ImageUrl = "https://google.com",
            Location = new Location
            {
              Geometry = GetPointGeometry(random)
            }
          };
        case 3:
          return new WoodyPlantDocument
          {
            LocalizedNames = new LocalizedString { Czech = "Památný Hloch" },
            LocalizedSpecies = new LocalizedString { Czech = "Hloch pouličný" },
            ImageUrl = "https://google.com",
            Location = new Location
            {
              Geometry = GetPointGeometry(random)
            }
          };
        case 4:
          return new WoodyPlantDocument
          {
            LocalizedNotes = new LocalizedString { Czech = "Roste zde již po 150 let a stále nejsme schopni zjistit o jaký strom se jedná a ani nevíme jak ho pojmenovat." },
            ImageUrl = "https://google.com",
            Location = new Location
            {
              Geometry = GetPointGeometry(random)
            }
          };
        default:
          return new WoodyPlantDocument
          {
            LocalizedNames = new LocalizedString { Czech = "Strom" },
            LocalizedNotes = new LocalizedString { Czech = "Strom obecný" },
            LocalizedSpecies = new LocalizedString { Czech = "Obyčený neutrální strom." },
            ImageUrl = "https://google.com",
            Location = new Location
            {
              Geometry = GetPointGeometry(random)
            }
          };
      }
    }

    private PointGeometry GetPointGeometry(Random random)
    {
      return new PointGeometry
      {
        Coordinates = GetPoint(random)
      };
    }

    private double[] GetPoint(Random random, double xOffset = 0, double yOffset = 0, double xCoef = 5, double yCoef = 2)
    {
      return new double[] { 12.661745182000601 + xOffset + random.NextDouble() * xCoef, 48.806820963248754 + yOffset + random.NextDouble() * yCoef };
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
      var firstPoint = GetPoint(random);
      var secondPoint = new double[] { firstPoint[0] + random.NextDouble() * 0.01, firstPoint[1] + random.NextDouble() * 0.01 };
      var thirdPoint = new double[] { secondPoint[0] + random.NextDouble() * 0.01, secondPoint[1] + random.NextDouble() * 0.01 };
      return new double[][] { firstPoint, secondPoint, thirdPoint };
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
      var offsetX = random.NextDouble();
      var offsetY = random.NextDouble() / 5;
      var start = new double[] { 12.661745182000601 + offsetX + random.NextDouble() / 5, 48.806820963248754 + offsetY + random.NextDouble() / 5 };
      var points = new double[][]
      {
        start,
        new double[] { 13.661745182000601+ offsetX + random.NextDouble(), 48.806820963248754 + offsetY + random.NextDouble() / 5 },
        new double[] { 13.661745182000601 + offsetX + random.NextDouble(), 49.106820963248754 + offsetY + random.NextDouble() / 5 },
        new double[] { 12.661745182000601 + offsetX + random.NextDouble(), 49.106820963248754 + offsetY + random.NextDouble() / 5 },
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
