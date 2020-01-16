using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreeLoc.Api.Models;

namespace TreeLoc.Api.Controllers
{
  [Route("api/v1/info")]
  public class InfoController: Controller
  {
    /// <summary>
    /// Returns basic info about api
    /// </summary>
    /// <returns>Model with info</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoModel))]
    public InfoModel Get()
    {
      var version = Assembly
        .GetEntryAssembly()!
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
        ?.InformationalVersion;

      Debug.Assert(version != null);

      return new InfoModel { Version = version };
    }
  }
}
