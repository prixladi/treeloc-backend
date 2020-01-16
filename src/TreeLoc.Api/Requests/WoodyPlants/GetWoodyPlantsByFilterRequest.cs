using System;
using MediatR;
using TreeLoc.Api.Models;

namespace TreeLoc.Api.Requests.WoodyPlants
{
  public class GetWoodyPlantsByFilterRequest: IRequest<WoodyPlantListModel>
  {
    public WoodyPlantFilterModel Filter { get; }

    public GetWoodyPlantsByFilterRequest(WoodyPlantFilterModel filter)
    {
      Filter = filter ?? throw new ArgumentNullException(nameof(filter));
    }
  }
}
