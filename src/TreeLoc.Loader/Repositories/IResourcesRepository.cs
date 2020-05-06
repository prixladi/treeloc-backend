using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TreeLoc.Loader.Repositories
{
  public interface IResourcesRepository
  {
    Task AddAsync(string url, CancellationToken cancellationToken);
    Task SetTrueAsync(string url, CancellationToken cancellationToken);
    Task<List<string>> GetFalseAsync(CancellationToken cancellationToken);
    Task ClearAsync(CancellationToken cancellationToken);
  }
}