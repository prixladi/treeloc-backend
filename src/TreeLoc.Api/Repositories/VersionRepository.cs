using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using TreeLoc.Database;
using TreeLoc.Database.Documents;
using TreeLoc.Repositories;

namespace TreeLoc.Api.Repositories
{
  [ExcludeFromCodeCoverage]
  public class VersionRepository: RepositoryBase<VersionDocument, DbConfigDocument>, IVersionRepository
  {
    public VersionRepository(DbContext dbContext)
      : base(dbContext) { }

    public async Task<VersionDocument> GetSingleAsync(CancellationToken cancellationToken)
    {
      return await Query.SingleOrDefaultAsync(cancellationToken);
    }
  }
}
