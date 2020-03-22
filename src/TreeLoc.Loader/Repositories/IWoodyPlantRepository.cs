using System.Threading;
using System.Threading.Tasks;
using TreeLoc.Database.Documents;
using TreeLoc.Repositories;

namespace TreeLoc.Loader.Repositories
{
  public interface IWoodyPlantRepository: IRepositoryBase<WoodyPlantDocument>
  {
    Task InsertManyAsync(WoodyPlantDocument[] documents, CancellationToken cancellationToken);
    Task DelteInvalidAsync(string version, CancellationToken cancellationToken);
  }
}