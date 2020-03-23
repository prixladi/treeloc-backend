using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TreeLoc.Loader.Configs;
using TreeLoc.Loader.Repositories;
using TreeLoc.Loader.Services;
using TreeLoc.Loader.SignalR;

namespace TreeLoc.Loader
{
  public sealed class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<DbConfig>();
      services.AddSignalR();

      services.AddHostedService<LoaderService>();
      services.AddHostedService<DiscoveryService>();

      services.AddSingleton<IResourcesRepository, ResourcesRepository>();

      services.AddTransient<IWoodyPlantRepository, WoodyPlantRepository>();
      services.AddTransient<IVersionRepository, VersionRepository>();
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
