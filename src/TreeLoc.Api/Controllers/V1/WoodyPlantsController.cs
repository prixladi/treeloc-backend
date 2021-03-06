﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TreeLoc.Api.Models;
using TreeLoc.Api.Requests.WoodyPlants;
using TreeLoc.Middlewares;

namespace TreeLoc.Api.Controllers.V1
{
  [Route("api/v1/woodyPlants")]
  public class WoodyPlantsController: ControllerBase
  {
    private readonly IMediator fMediator;

    public WoodyPlantsController(IMediator mediator)
    {
      fMediator = mediator;
    }

    /// <summary>
    /// Vrátí náhledy dřevin
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="sort"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Seznam modelů náhledů dřevin</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WoodyPlantListModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<WoodyPlantListModel> GetManyAsync([FromQuery]WoodyPlantFilterModel filter, [FromQuery]WoodyPlantSortModel sort, CancellationToken cancellationToken)
    {
      return fMediator.Send(new GetWoodyPlantsByFilterRequest(filter, sort), cancellationToken);
    }

    /// <summary>
    /// Vrátí detail dřeviny
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Model detailu dřeviny</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WoodyPlantDetailModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResposeMessageModel))]
    public Task<WoodyPlantDetailModel> GetAsync([FromRoute]ObjectId id, CancellationToken cancellationToken)
    {
      return fMediator.Send(new GetWoodyPlantRequest(id), cancellationToken);
    }
  }
}
