using TreeLoc.Database;
using TreeLoc.Database.Documents;

namespace TreeLoc.Repositories
{
  public class WoodyPlantsRepository: RepositoryBase<WoodyPlantDocument>, IWoodyPlantsRepository
  {
    public WoodyPlantsRepository(DbContext dbContext)
      : base(dbContext) { }
  }
}
