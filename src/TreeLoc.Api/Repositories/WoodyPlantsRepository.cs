using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
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

    public Task<int> CountAsync(WoodyPlantFilterModel woodyPlantFilterModel, CancellationToken cancellationToken)
    {
      return Query
        .FilterNoPage(woodyPlantFilterModel)
        .CountAsync(cancellationToken);
    }

    public Task<List<WoodyPlantDocument>> GetByFilterAsync(WoodyPlantFilterModel woodyPlantFilterModel, CancellationToken cancellationToken)
    {
      return Query
        .Filter(woodyPlantFilterModel)
        .ToListAsync(cancellationToken);
    }
  }
}
