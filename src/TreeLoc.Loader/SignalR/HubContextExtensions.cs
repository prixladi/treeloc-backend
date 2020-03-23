using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TreeLoc.Loader.SignalR
{
  public static class HubContextExtensions
  {
    public static async Task SendVersionChangedAsync(this IHubContext<ClientHub> hubContext, string version, CancellationToken cancellationToken)
    {
      await hubContext
        .Clients
        .All
        .SendAsync("versionChanged", new { version }, cancellationToken);
    }
  }
}
