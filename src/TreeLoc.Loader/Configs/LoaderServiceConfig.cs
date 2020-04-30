using System;
using System.Diagnostics.CodeAnalysis;

namespace TreeLoc.Loader.Configs
{
  [ExcludeFromCodeCoverage]
  public class LoaderServiceConfig: ILoaderServiceConfig
  {
    public TimeSpan Interval => TimeSpan.FromSeconds(int.Parse(EnvironmentVariables.Get(EnvironmentVariables._LoaderInterval) ?? "60"));
  }
}
