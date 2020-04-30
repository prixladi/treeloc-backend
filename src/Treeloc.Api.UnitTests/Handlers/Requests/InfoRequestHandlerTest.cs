using System.Threading.Tasks;
using MongoDB.Bson;
using NSubstitute;
using TreeLoc.Api.Handlers.Requests;
using TreeLoc.Api.Repositories;
using TreeLoc.Api.Requests.WoodyPlants;
using TreeLoc.Database.Documents;
using Xunit;

namespace TreeLoc.Api.UnitTests.Handlers.Requests
{
  public class InfoRequestHandlerTest
  {
    private readonly IVersionRepository fVersionRepository;

    private readonly InfoRequestHandler fHandler;

    public InfoRequestHandlerTest()
    {
      fVersionRepository = Substitute.For<IVersionRepository>();
      fHandler = new InfoRequestHandler(fVersionRepository);
    }

    [Fact]
    public async Task Handle_TestAsync()
    {
      fVersionRepository
        .GetSingleAsync(default)
        .Returns(new VersionDocument
        {
          Id = ObjectId.GenerateNewId(),
          Version = "version"
        });

      var result = await fHandler.Handle(new GetInfoRequest(), default);

      Assert.Equal("version", result.Version);
    }
  }
}
