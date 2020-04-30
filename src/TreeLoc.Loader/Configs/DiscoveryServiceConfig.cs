using System;
using System.Diagnostics.CodeAnalysis;

namespace TreeLoc.Loader.Configs
{
  [ExcludeFromCodeCoverage]
  public class DiscoveryServiceConfig: IDiscoveryServiceConfig
  {
    public TimeSpan Interval => TimeSpan.FromSeconds(int.Parse(EnvironmentVariables.Get(EnvironmentVariables._DiscoveryInterval) ?? "60"));
    public string Url => EnvironmentVariables.GetOrThrow(EnvironmentVariables._DiscoveryUrl);
  }
}
