using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Bson;
using NSubstitute;
using TreeLoc.Api.Controllers.V1;
using TreeLoc.Api.Models;
using TreeLoc.Api.Requests.WoodyPlants;
using Xunit;

namespace Treeloc.Api.UnitTests.Controllers
{
  public class WoodyPlantsControllerTest
  {
    private readonly IMediator fMediator;

    public WoodyPlantsControllerTest()
    {
      fMediator = Substitute.For<IMediator>();
    }

    [Fact]
    public async Task Get_TestAsync()
    {
      var id = ObjectId.GenerateNewId();
      var model = new WoodyPlantDetailModel();

      fMediator
        .Send(Arg.Is<GetWoodyPlantRequest>(x => x.PlantId == id), Arg.Is(default(CancellationToken)))
        .Returns(model);

      var result = await new WoodyPlantsController(fMediator).GetAsync(id, default);
      Assert.Equal(model, result);
    }

    [Fact]
    public async Task GetMany_TestAsync()
    {
      var filter = new WoodyPlantFilterModel();
      var sort = new WoodyPlantSortModel();

      var model = new WoodyPlantListModel();

      fMediator
        .Send(Arg.Is<GetWoodyPlantsByFilterRequest>(x => x.Filter == filter && x.Sort == sort), Arg.Is(default(CancellationToken)))
        .Returns(model);

      var result = await new WoodyPlantsController(fMediator).GetManyAsync(filter, sort, default);
      Assert.Equal(model, result);
    }
  }
}
