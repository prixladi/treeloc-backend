using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using TreeLoc.Database;
using TreeLoc.Database.Documents;
using TreeLoc.Repositories;

namespace TreeLoc.Loader.Repositories
{
  public class VersionRepository: RepositoryBase<VersionDocument, DbConfigDocument>, IVersionRepository
  {
    public VersionRepository(DbContext dbContext)
      : base(dbContext) { }

    public async Task UpdateAsync(string version, CancellationToken cancellationToken)
    {
      var update = Builders<VersionDocument>.Update
        .Set(x => x.Version, version);

      await Collection.UpdateOneAsync(
        Builders<VersionDocument>.Filter.Where(x => true), update,
        new UpdateOptions { IsUpsert = true }, cancellationToken);
    }
  }
}
