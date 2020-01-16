using System;
using MongoDB.Driver.Linq;
using TreeLoc.Api.Models;
using TreeLoc.Database.Documents;

namespace TreeLoc.Api.Repositories
{
  public static class MongoQueriableExtensions
  {
    public static IMongoQueryable<WoodyPlantDocument> Filter(this IMongoQueryable<WoodyPlantDocument> query, WoodyPlantFilterModel filter)
    {
      if (query is null)
        throw new ArgumentNullException(nameof(query));

      return query
        .FilterNoPage(filter)
        .Page(filter.Skip, filter.Take);
    }

    public static IMongoQueryable<WoodyPlantDocument> FilterNoPage(this IMongoQueryable<WoodyPlantDocument> query, WoodyPlantFilterModel filter)
    {
      if (query is null)
        throw new ArgumentNullException(nameof(query));

      if (filter.Name != null)
      {
        var name = filter.Name.ToLower();
        query = query.Where(doc => doc.Name.ToLower().Contains(name));
      }

      return query;
    }

    public static IMongoQueryable<TDocument> Page<TDocument>(this IMongoQueryable<TDocument> query, int skip, int take)
      where TDocument: DocumentBase
    {
      if (query is null)
        throw new ArgumentNullException(nameof(query));
      if (skip < 0)
        throw new ArgumentOutOfRangeException(nameof(skip));
      if (take < 1)
        throw new ArgumentOutOfRangeException(nameof(take));

      return query
        .Skip(skip)
        .Take(take);
    }
  }
}
