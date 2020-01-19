using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using TreeLoc.Database;
using TreeLoc.Database.Documents;

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
      yield return new CreateIndexModel<WoodyPlantDocument>(builder.Ascending(x => x.LocalizedNames.Czech), new CreateIndexOptions { Collation = new Collation("cs")  });
      yield return new CreateIndexModel<WoodyPlantDocument>(builder.Ascending(x => x.LocalizedNotes.Czech), new CreateIndexOptions { Collation = new Collation("cs") });
      yield return new CreateIndexModel<WoodyPlantDocument>(builder.Geo2DSphere(x => x.Location!.Geometry));
    }
  }
}
