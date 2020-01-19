using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TreeLoc.Api.Extensions;
using TreeLoc.Api.Models;
using TreeLoc.Api.Repositories;
using TreeLoc.Api.Requests.WoodyPlants;
using TreeLoc.Exceptions;

namespace TreeLoc.Api.Handlers.Requests
{
  public class WoodyPlantsRequestHandler:
    IRequestHandler<GetWoodyPlantRequest, WoodyPlantDetailModel>,
    IRequestHandler<GetWoodyPlantsByFilterRequest, WoodyPlantListModel>
  {
    private readonly IWoodyPlantsRepository fWoodyPlantsRepository;

    public WoodyPlantsRequestHandler(IWoodyPlantsRepository woodyPlantsRepository)
    {
      fWoodyPlantsRepository = woodyPlantsRepository;
    }

    public async Task<WoodyPlantDetailModel> Handle(GetWoodyPlantRequest request, CancellationToken cancellationToken)
    {
      var plant = await fWoodyPlantsRepository.GetByIdAsync(request.PlantId, cancellationToken);
      if (plant == null)
        throw new NotFoundException($"Woody plant with ID '{request.PlantId}' was not found.");

      return plant.ToDetail();
    }

    public async Task<WoodyPlantListModel> Handle(GetWoodyPlantsByFilterRequest request, CancellationToken cancellationToken)
    {
      var plants = await fWoodyPlantsRepository.GetByFilterAsync(request.Filter, request.Sort, cancellationToken);

      return new WoodyPlantListModel
      {
        TotalCount = await fWoodyPlantsRepository.CountByFilterAsync(request.Filter, cancellationToken),
        WoodyPlants = plants.ToPreview()
      };
    }
  }
}
