using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TreeLoc.Api;

namespace Treeloc
{
  [ExcludeFromCodeCoverage]
  public sealed class Program
  {
    public static Task Main()
    {
      return CreateHostBuilder()
        .Build()
        .RunAsync();
    }

    public static IHostBuilder CreateHostBuilder()
    {
      return new HostBuilder()
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseStartup<Startup>();
          webBuilder.UseSentry(EnvironmentVariables.Get(EnvironmentVariables._SentryUrl));
        });
    }
  }
}
