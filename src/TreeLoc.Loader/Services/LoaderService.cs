using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using TreeLoc.Extension;
using TreeLoc.Loader.Repositories;
using TreeLoc.Loader.SignalR;
using TreeLoc.OFN;

namespace TreeLoc.Loader.Services
{
  public class LoaderService: IHostedService
  {
    private readonly IResourcesRepository fResourcesRepository;
    private readonly IVersionRepository fVersionRepository;
    private readonly IWoodyPlantRepository fWoodyPlantRepository;
    private readonly IHubContext<ClientHub> fHubContext;

    private Task? fLoaderTask;
    private CancellationTokenSource? fCancellationTokenSource;

    public LoaderService(
      IResourcesRepository resourcesRepository,
      IVersionRepository versionRepository,
      IWoodyPlantRepository woodyPlantRepository,
      IHubContext<ClientHub> hubContext)
    {
      fResourcesRepository = resourcesRepository;
      fVersionRepository = versionRepository;
      fWoodyPlantRepository = woodyPlantRepository;
      fHubContext = hubContext;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
      fCancellationTokenSource = new CancellationTokenSource();

      var delay = EnvironmentVariables.Get(EnvironmentVariables._LoaderInterval);
      await fWoodyPlantRepository.DeleteAsync(cancellationToken);
      fLoaderTask = LoadAsync(TimeSpan.FromSeconds(int.Parse(delay ?? "60")), fCancellationTokenSource.Token);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
      if (fLoaderTask == null)
        return;

      try
      {
        fCancellationTokenSource!.Cancel();
      }
      finally
      {
        await Task.WhenAny(fLoaderTask, Task.Delay(Timeout.Infinite, cancellationToken));
        fLoaderTask = null;
      }
    }

    private async Task LoadAsync(TimeSpan delay, CancellationToken cancellationToken)
    {
      string runVersion = DateTime.UtcNow.GetHashCode().ToString();

      while (true)
      {
        var resources = fResourcesRepository.GetFalse();
        foreach (var resource in resources)
        {
          try
          {
            var data = await LoadAsync(resource, cancellationToken);
            var documents = data.ToDocument(runVersion);

            await fWoodyPlantRepository.InsertManyAsync(documents, cancellationToken);
            await fWoodyPlantRepository.DeleteInvalidAsync(runVersion, cancellationToken);

            fResourcesRepository.SetTrue(resource);

            var dataVersion = DateTime.UtcNow.GetHashCode().ToString();
            await fVersionRepository.UpdateAsync(dataVersion, cancellationToken);
            await fHubContext.SendVersionChangedAsync(dataVersion, cancellationToken);

            Console.WriteLine($"Successfuly loaded data from resource '{resource}'");
          }
          catch (Exception ex)
          {
            Console.WriteLine($"Error loading data from resource '{resource}'");
            Console.WriteLine(ex);
          }

          await Task.Delay(delay, cancellationToken);
        }

        if (!resources.Any())
          await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationToken);
      }
    }

    private async Task<WoodyPlant[]> LoadAsync(Uri resource, CancellationToken _)
    {
      var data = await HttpClientContext.Client.GetStringAsync(resource);
      return Newtonsoft.Json.JsonConvert.DeserializeObject<WoodyPlant[]>(data);
    }
  }
}
