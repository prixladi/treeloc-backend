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
    Task<int> CountAsync(WoodyPlantFilterModel woodyPlantFilterModel, CancellationToken cancellationToken);
    Task<List<WoodyPlantDocument>> GetByFilterAsync(WoodyPlantFilterModel woodyPlantFilterModel, CancellationToken cancellationToken);
  }
}