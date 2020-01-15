using System;
using TreeLoc.Database;
using TreeLoc.Facades;
using TreeLoc.Factories;
using TreeLoc.Handlers.Exceptions;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ServiceCollectionExtensions
  {
    public static void AddExceptionHandling(this IServiceCollection services)
    {
      if (services is null)
        throw new ArgumentNullException(nameof(services));

      services.AddTransient<IExceptionHandlerFactory, ExceptionHandlerFactory>();
      services.AddTransient<IExceptionHandlingFacade, ExceptionHandlingFacade>();

      services.AddTransient<IExceptionHandler, NotFoundExceptionHandler>();
    }

    public static void AddDbContext<TDbConfig>(this IServiceCollection services)
      where TDbConfig: class, IDbConfig
    {
      if (services is null)
        throw new ArgumentNullException(nameof(services));

      services.AddTransient<IDbConfig, TDbConfig>();
      services.AddSingleton<DbContext>();
    }
  }
}
