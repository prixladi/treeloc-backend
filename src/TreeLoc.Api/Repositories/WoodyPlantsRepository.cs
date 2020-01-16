using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using TreeLoc.Api.Extensions;
using TreeLoc.Api.Models;
using TreeLoc.Database;
using TreeLoc.Database.Documents;
using TreeLoc.Repositories;

namespace TreeLoc.Api.Repositories
{
  public class WoodyPlantsRepository: RepositoryBase<WoodyPlantDocument>, IWoodyPlantsRepository
  {
    public WoodyPlantsRepository(DbContext dbContext)
      : base(dbContext) { }

    public Task<long> CountByFilterAsync(WoodyPlantFilterModel filter, CancellationToken cancellationToken)
    {
      return Collection.CountAsync(filter.ToFilterDefinition(), cancellationToken: cancellationToken);
    }

    public async Task<List<WoodyPlantDocument>> GetByFilterAsync(WoodyPlantFilterModel filter, CancellationToken cancellationToken)
    {
      if (filter is null)
        throw new ArgumentNullException(nameof(filter));

      var cursor = await Collection.FindAsync(filter.ToFilterDefinition(), new FindOptions<WoodyPlantDocument, WoodyPlantDocument>
      {
        Skip = filter.Skip,
        Limit = filter.Take
      });

      var plants = new List<WoodyPlantDocument>();

      while (await cursor.MoveNextAsync(cancellationToken))
        plants.AddRange(cursor.Current);

      return plants;
    }
  }
}
