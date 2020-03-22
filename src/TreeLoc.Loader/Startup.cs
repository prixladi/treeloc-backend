using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TreeLoc.Loader.Configs;
using TreeLoc.Loader.Repositories;
using TreeLoc.Loader.Services;

namespace TreeLoc.Loader
{
  public sealed class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddHostedService<LoaderService>();
      services.AddDbContext<DbConfig>();
      services.AddTransient<IWoodyPlantRepository, WoodyPlantRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseRouting();
    }
  }
}
