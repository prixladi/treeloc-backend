using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TreeLoc.Database;
using TreeLoc.Database.Documents;
using TreeLoc.Repositories;

namespace TreeLoc.Loader.Repositories
{
  [ExcludeFromCodeCoverage]
  public class ResourcesRepository: RepositoryBase<ResourceDocument>, IResourcesRepository
  {
    public ResourcesRepository(DbContext dbContext)
      : base(dbContext) { }

    public async Task AddAsync(string url, CancellationToken cancellationToken)
    {
      var update = Builders<ResourceDocument>.Update
        .SetOnInsert(doc => doc.Fetched, false)
        .SetOnInsert(doc => doc.Url, url);

      await Collection.UpdateOneAsync(x => x.Url == url, update,
        new UpdateOptions { IsUpsert = true }, cancellationToken);
    }

    public async Task SetTrueAsync(string url, CancellationToken cancellationToken)
    {
      var update = Builders<ResourceDocument>.Update
        .Set(doc => doc.Fetched, true)
        .SetOnInsert(doc => doc.Url, url);

      await Collection.UpdateOneAsync(x => x.Url == url, update,
        new UpdateOptions { IsUpsert = true }, cancellationToken);
    }

    public async Task<List<string>> GetFalseAsync(CancellationToken cancellationToken)
    {
      return await Query
        .Where(doc => doc.Fetched == false)
        .Select(x => x.Url)
        .ToListAsync(cancellationToken);
    }

    public async Task ClearAsync(CancellationToken cancellationToken)
    {
      await Collection.DeleteManyAsync(Builders<ResourceDocument>.Filter.Empty, cancellationToken);
    }
  }
}
