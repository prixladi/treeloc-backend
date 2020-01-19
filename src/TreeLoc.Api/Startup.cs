using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TreeLoc.Api.Configs;
using TreeLoc.Api.IoC;

namespace Treeloc
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors();
      services.AddExceptionHandling();

      services.AddControllers(ControllersConfig.Configure)
        .AddNewtonsoftJson(JsonConfig.Setup);

      services.AddDbContext<DbConfig>();

      services.AddMediatR(typeof(Startup));

      services.AddRepositories();
      services.AddHostedServices();

      services.AddSwaggerGen(SwaggerConfig.SetupSwaggerGen);
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseRouting();
      app.UseCors(CorsConfig.Setup);

      app.UseExceptionHandling();

      app.UseEndpoints(builder =>
      {
        builder.MapControllers();
      });

      app.UseSwagger(SwaggerConfig.SetupSwagger);
      app.UseSwaggerUI(SwaggerConfig.SetupSwaggerUI);
    }
  }
}
