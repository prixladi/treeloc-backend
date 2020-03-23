using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TreeLoc.Api.Models;
using TreeLoc.Database.Documents;
using TreeLoc.Repositories;

namespace TreeLoc.Api.Repositories
{
  public interface IWoodyPlantsRepository: IRepositoryBase<WoodyPlantDocument>
  {
    Task<long> CountAsync(CancellationToken cancellationToken);
    Task<long> CountByFilterAsync(WoodyPlantFilterModel woodyPlantFilterModel, CancellationToken cancellationToken);
    Task<List<WoodyPlantDocument>> GetAsync(CancellationToken cancellationToken);
    Task<List<WoodyPlantDocument>> GetByFilterAsync(WoodyPlantFilterModel filter, WoodyPlantSortModel sort, CancellationToken cancellationToken);
  }
}