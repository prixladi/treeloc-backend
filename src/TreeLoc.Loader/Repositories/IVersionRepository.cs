using System.Threading;
using System.Threading.Tasks;

namespace TreeLoc.Loader.Repositories
{
  public interface IVersionRepository
  {
    Task UpdateAsync(string version, CancellationToken cancellationToken);
  }
}