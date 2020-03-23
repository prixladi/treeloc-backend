using System;

namespace TreeLoc.Loader.Repositories
{
  public interface IResourcesRepository
  {
    void Add(Uri url);
    Uri[] GetFalse();
    void SetTrue(Uri url);
  }
}