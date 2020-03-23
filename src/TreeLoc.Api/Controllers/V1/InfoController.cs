using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreeLoc.Api.Models;
using TreeLoc.Api.Requests.WoodyPlants;

namespace TreeLoc.Api.Controllers
{
  [Route("api/v1/info")]
  public class InfoController: Controller
  {
    private readonly IMediator fMediator;

    public InfoController(IMediator mediator)
    {
      fMediator = mediator;
    }

    /// <summary>
    /// Returns basic info about api
    /// </summary>
    /// <returns>Model with info</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoModel))]
    public async Task<InfoModel> GetAsync(CancellationToken cancellationToken)
    {
      return await fMediator.Send(new GetInfoRequest(), cancellationToken);
    }
  }
}
