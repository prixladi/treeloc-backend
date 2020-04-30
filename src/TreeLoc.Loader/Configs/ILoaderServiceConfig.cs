using System;

namespace TreeLoc.Loader.Configs
{
  public interface ILoaderServiceConfig
  {
    public TimeSpan Interval { get; }
  }
}