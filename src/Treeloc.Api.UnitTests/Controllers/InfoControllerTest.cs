using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using TreeLoc.Api.Controllers;
using TreeLoc.Api.Models;
using TreeLoc.Api.Requests.WoodyPlants;
using Xunit;

namespace Treeloc.Api.UnitTests.Controllers
{
  public class InfoControllerTest
  {
    private readonly IMediator fMediator;

    public InfoControllerTest()
    {
      fMediator = Substitute.For<IMediator>();
    }

    [Fact]
    public async Task Get_TestAsync()
    {
      var controller = new InfoController(fMediator);

      fMediator
        .Send(Arg.Any<GetInfoRequest>(), Arg.Any<CancellationToken>())
        .Returns(new InfoModel { Version = "ver" });

      var result = await controller.GetAsync(default);
      var objResult = Assert.IsType<OkObjectResult>(result);
      var model = Assert.IsType<InfoModel>(objResult.Value);
      Assert.Equal("ver", model.Version);
    }
  }
}
