using System;

namespace TreeLoc.Loader.Configs
{
  public interface IDiscoveryServiceConfig
  {
    public TimeSpan Interval { get; }
    public string Url { get; }
  }
}