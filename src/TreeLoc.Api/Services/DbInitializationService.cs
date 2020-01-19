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

      for (int i = 0; i < 0; i++)
      {
        var documents = new List<WoodyPlantDocument>();
        for (int j = 0; j < 5000; j++)
        {
          documents.Add(new WoodyPlantDocument
          {
            ImageUrl = "www.google.com",
            Location = new Location
            {
              Geometry = new PointGeometry
              {
                Coordinates = new double[] { 50, 14 }
              }
            },
            LocalizedNames = new LocalizedString { Czech = "Praha" },
            LocalizedNotes = new LocalizedString { Czech = "Lore impsum dolor" },
            LocalizedSpecies = new LocalizedString { Czech = "Specie special" }
          });

          documents.Add(new WoodyPlantDocument
          {
            ImageUrl = "www.google.com",
            Location = new Location
            {
              Geometry = new PointGeometry
              {
                Coordinates = new double[] { 40, -73 }
              }
            },
            LocalizedNames = new LocalizedString { Czech = "New york pošuk" },
            LocalizedNotes = new LocalizedString { Czech = "Lore impsum dolor" },
            LocalizedSpecies = new LocalizedString { Czech = "Specie special" }
          });

          documents.Add(new WoodyPlantDocument
          {
            ImageUrl = "www.google.com",
            Location = new Location
            {
              Geometry = new PointGeometry
              {
                Coordinates = new double[] { 11, 22 }
              }
            },
            LocalizedNames = new LocalizedString { Czech = "Random" },
            LocalizedNotes = new LocalizedString { Czech = "Lore impsum dolor hululu" },
            LocalizedSpecies = new LocalizedString { Czech = "Namee specie locale řekl řecký řek" }
          });
        }

        await collection.InsertManyAsync(documents,cancellationToken: cancellationToken);
      }
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
  }
}
