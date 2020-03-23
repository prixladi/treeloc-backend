﻿using System;
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

    public async Task<long> CountAsync(CancellationToken cancellationToken)
    {
      return await Collection.CountDocumentsAsync(Builders<WoodyPlantDocument>.Filter.Empty, null, cancellationToken);
    }

    public async Task<List<WoodyPlantDocument>> GetAsync(CancellationToken cancellationToken)
    {
      return await Query.ToListAsync(cancellationToken);
    }

    public async Task<long> CountByFilterAsync(WoodyPlantFilterModel filter, CancellationToken cancellationToken)
    {
      if (filter.Point != null)
        return -1;

      return await Collection.CountDocumentsAsync(filter.ToFilterDefinition(), cancellationToken: cancellationToken);
    }

    public async Task<List<WoodyPlantDocument>> GetByFilterAsync(WoodyPlantFilterModel filter, WoodyPlantSortModel sort, CancellationToken cancellationToken)
    {
      if (filter is null)
        throw new ArgumentNullException(nameof(filter));
      if (sort is null)
        throw new ArgumentNullException(nameof(sort));

      var cursor = await Collection.FindAsync(
        filter.ToFilterDefinition(),
        sort.ToFindOptions(filter.Skip, filter.Take, filter.Text != null),
        cancellationToken);

      var plants = new List<WoodyPlantDocument>();

      while (await cursor.MoveNextAsync(cancellationToken))
        plants.AddRange(cursor.Current);

      return plants;
    }
  }
}
