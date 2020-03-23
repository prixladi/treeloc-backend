using Microsoft.Extensions.DependencyInjection;
using TreeLoc.Api.Repositories;

namespace TreeLoc.Api.IoC
{
  public static class Repositories
  {
    public static void AddRepositories(this IServiceCollection services)
    {
      services.AddTransient<IWoodyPlantsRepository, WoodyPlantsRepository>();
      services.AddTransient<IVersionRepository, VersionRepository>();
    }
  }
}
