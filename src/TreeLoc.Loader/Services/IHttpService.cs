using System;
using System.Threading;
using System.Threading.Tasks;
using TreeLoc.OFN;

namespace TreeLoc.Loader.Services
{
  public interface IHttpService
  {
    Task<string[]> DiscoveryAsync(Uri url, CancellationToken _);
    Task<WoodyPlant[]> LoadAsync(Uri resource, CancellationToken _);
  }
}