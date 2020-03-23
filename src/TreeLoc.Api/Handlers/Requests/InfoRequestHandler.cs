using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TreeLoc.Api.Models;
using TreeLoc.Api.Repositories;
using TreeLoc.Api.Requests.WoodyPlants;

namespace TreeLoc.Api.Handlers.Requests
{
  public class InfoRequestHandler: IRequestHandler<GetInfoRequest, InfoModel>
  {
    private readonly IVersionRepository fVersionRepository;

    public InfoRequestHandler(IVersionRepository versionRepository)
    {
      fVersionRepository = versionRepository;
    }

    public async Task<InfoModel> Handle(GetInfoRequest request, CancellationToken cancellationToken)
    {
      var document = await fVersionRepository.GetSingleAsync(cancellationToken);

      return new InfoModel
      {
        Version = document?.Version
      };
    }
  }
}
