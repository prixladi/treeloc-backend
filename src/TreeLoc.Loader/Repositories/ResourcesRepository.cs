using System;
using System.Collections.Concurrent;
using System.Linq;

namespace TreeLoc.Loader.Repositories
{
  public class ResourcesRepository: IResourcesRepository
  {
    private readonly ConcurrentDictionary<Uri, bool> fResources;

    public ResourcesRepository()
    {
      fResources = new ConcurrentDictionary<Uri, bool>();
    }

    public void Add(Uri url)
    {
      fResources.TryAdd(url, false);
    }

    public void SetTrue(Uri url)
    {
      fResources.TryUpdate(url, true, false);
    }

    public Uri[] GetFalse()
    {
      return fResources
        .Where(res => !res.Value)
        .Select(res => res.Key)
        .ToArray();
    }
  }
}
