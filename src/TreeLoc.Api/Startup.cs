using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TreeLoc.Api.Configs;
using TreeLoc.Api.Filters;

namespace Treeloc
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors();
      services.AddExceptionHandling();

      services.AddControllers(options => options.Filters.Add<ApiValidationAttribute>())
        .AddJsonOptions(JsonConfig.Setup);

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
