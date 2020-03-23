using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using TreeLoc.Database;
using TreeLoc.Database.Documents;
using TreeLoc.Repositories;

namespace TreeLoc.Loader.Repositories
{
  public class WoodyPlantRepository: RepositoryBase<WoodyPlantDocument>, IWoodyPlantRepository
  {
    public WoodyPlantRepository(DbContext dbContext)
      : base(dbContext) { }

    public async Task InsertManyAsync(WoodyPlantDocument[] documents, CancellationToken cancellationToken)
    {
      await Collection.InsertManyAsync(documents, cancellationToken: cancellationToken);
    }

    public async Task DeleteInvalidAsync(string version, CancellationToken cancellationToken)
    {
      await Collection.DeleteManyAsync(doc => doc.Version != version, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(CancellationToken cancellationToken)
    {
      await Collection.DeleteManyAsync(Builders<WoodyPlantDocument>.Filter.Empty, cancellationToken: cancellationToken);
    }
  }
}
