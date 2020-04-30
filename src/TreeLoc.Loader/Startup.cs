using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TreeLoc.Loader.Configs;
using TreeLoc.Loader.Repositories;
using TreeLoc.Loader.Services;
using TreeLoc.Loader.SignalR;

namespace TreeLoc.Loader
{
  [ExcludeFromCodeCoverage]
  public sealed class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<DbConfig>();
      services.AddSignalR();

      services.AddHostedService<LoaderService>();
      services.AddHostedService<DiscoveryService>();

      services.AddSingleton<IResourcesRepository, ResourcesRepository>();
      services.AddSingleton<IDiscoveryServiceConfig, DiscoveryServiceConfig>();
      services.AddSingleton<ILoaderServiceConfig, LoaderServiceConfig>();

      services.AddTransient<IWoodyPlantRepository, WoodyPlantRepository>();
      services.AddTransient<IVersionRepository, VersionRepository>();
      services.AddTransient<IHttpService, HttpService>();
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseRouting();

      app.UseEndpoints(conf =>
      {
        conf.MapHub<ClientHub>("signalr");
      });
    }
  }
}
