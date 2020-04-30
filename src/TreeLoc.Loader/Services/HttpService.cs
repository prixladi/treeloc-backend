using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TreeLoc.OFN;

namespace TreeLoc.Loader.Services
{
  [ExcludeFromCodeCoverage]
  public class HttpService: IHttpService
  {
    public async Task<string[]> DiscoveryAsync(Uri url, CancellationToken _)
    {
      var data = await HttpClientContext.Client.GetStringAsync(url);

      return data.Split(Environment.NewLine)
        .Where(s => !string.IsNullOrEmpty(s))
        .ToArray();
    }

    public async Task<WoodyPlant[]> LoadAsync(Uri resource, CancellationToken _)
    {
      var data = await HttpClientContext.Client.GetStringAsync(resource);
      return Newtonsoft.Json.JsonConvert.DeserializeObject<WoodyPlant[]>(data);
    }
  }
}
