using System;
using MediatR;
using TreeLoc.Api.Models;

namespace TreeLoc.Api.Requests.WoodyPlants
{
  public class GetWoodyPlantsByFilterRequest: IRequest<WoodyPlantListModel>
  {
    public WoodyPlantFilterModel Filter { get; }
    public WoodyPlantSortModel Sort { get; }

    public GetWoodyPlantsByFilterRequest(
      WoodyPlantFilterModel filter,
      WoodyPlantSortModel sort)
    {
      Filter = filter ?? throw new ArgumentNullException(nameof(filter));
      Sort = sort ?? throw new ArgumentNullException(nameof(sort));
    }
  }
}
