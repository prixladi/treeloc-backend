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

      await collection.InsertOneAsync(new WoodyPlantDocument
      {
        ImageUrl = "www.google.com",
        Location = new Location
        {
          Geometry = new PointGeometry
          {
            Coordinates = new double[] { 50, 14 }
          }
        },
        Name = "Praha",
        Note = "Lore impsum dolor",
        Species = "Specie special",
        LocalizedNames = new Tuple<string, string>[] { new Tuple<string, string>("cs", "Namee"), new Tuple<string, string>("en", "Nameee") }
      });

      await collection.InsertOneAsync(new WoodyPlantDocument
      {
        ImageUrl = "www.google.com",
        Location = new Location
        {
          Geometry = new PointGeometry
          {
            Coordinates = new double[] { 40, -73 }
          }
        },
        Name = "New york",
        Note = "Lore impsum dolor",
        Species = "Specie special",
        LocalizedNames = new Tuple<string, string>[] { new Tuple<string, string>("cs", "Namee"), new Tuple<string, string>("en", "Nameee") }
      });

      await collection.InsertOneAsync(new WoodyPlantDocument
      {
        ImageUrl = "www.google.com",
        Location = new Location
        {
          Geometry = new PointGeometry
          {
            Coordinates = new double[] { 11, 22 }
          }
        },
        Name = "Random",
        Note = "Lore impsum dolor",
        Species = "Namee specie",
        LocalizedNotes = new Tuple<string, string>[] { new Tuple<string, string>("cs", "Namee"), new Tuple<string, string>("en", "Nameee") }
      });
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }

    private IEnumerable<CreateIndexModel<WoodyPlantDocument>> GetWoodyPlantsIndexes()
    {
      var builder = Builders<WoodyPlantDocument>.IndexKeys;

      yield return new CreateIndexModel<WoodyPlantDocument>(builder.Ascending(x => x.Species));
      yield return new CreateIndexModel<WoodyPlantDocument>(builder.Ascending(x => x.Note));
      yield return new CreateIndexModel<WoodyPlantDocument>(builder.Ascending(x => x.Name));
      yield return new CreateIndexModel<WoodyPlantDocument>(builder.Geo2DSphere(x => x.Location!.Geometry));
    }
  }
}
