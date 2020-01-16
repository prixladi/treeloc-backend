using MediatR;
using MongoDB.Bson;
using TreeLoc.Api.Models;

namespace TreeLoc.Api.Requests.WoodyPlants
{
  public class GetWoodyPlantRequest: IRequest<WoodyPlantDetailModel>
  {
    public ObjectId PlantId { get; }

    public GetWoodyPlantRequest(ObjectId plantId)
    {
      PlantId = plantId;
    }
  }
}
