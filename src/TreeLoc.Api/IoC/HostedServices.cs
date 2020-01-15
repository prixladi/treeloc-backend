using Microsoft.Extensions.DependencyInjection;
using TreeLoc.Api.Services;

namespace TreeLoc.Api.IoC
{
  public static class HostedServices
  {
    public static void AddHostedServices(this IServiceCollection services)
    {
      services.AddHostedService<DbInitializationService>();
    }
  }
}
