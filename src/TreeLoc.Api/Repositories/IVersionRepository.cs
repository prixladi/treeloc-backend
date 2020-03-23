using System.Threading;
using System.Threading.Tasks;
using TreeLoc.Database.Documents;

namespace TreeLoc.Api.Repositories
{
  public interface IVersionRepository
  {
    Task<VersionDocument> GetSingleAsync(CancellationToken cancellationToken);
  }
}