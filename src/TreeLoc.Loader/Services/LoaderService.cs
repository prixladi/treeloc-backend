using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using TreeLoc.Extension;
using TreeLoc.Loader.Configs;
using TreeLoc.Loader.Repositories;
using TreeLoc.Loader.SignalR;

namespace TreeLoc.Loader.Services
{
  public class LoaderService: IHostedService
  {
    private readonly IHttpService fHttpService;
    private readonly ILoaderServiceConfig fConfig;
    private readonly IResourcesRepository fResourcesRepository;
    private readonly IVersionRepository fVersionRepository;
    private readonly IWoodyPlantRepository fWoodyPlantRepository;
    private readonly IHubContext<ClientHub> fHubContext;

    private Task? fLoaderTask;
    private CancellationTokenSource? fCancellationTokenSource;

    public LoaderService(
      IHttpService httpService,
      ILoaderServiceConfig config,
      IResourcesRepository resourcesRepository,
      IVersionRepository versionRepository,
      IWoodyPlantRepository woodyPlantRepository,
      IHubContext<ClientHub> hubContext)
    {
      fHttpService = httpService;
      fConfig = config;
      fResourcesRepository = resourcesRepository;
      fVersionRepository = versionRepository;
      fWoodyPlantRepository = woodyPlantRepository;
      fHubContext = hubContext;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
      fCancellationTokenSource = new CancellationTokenSource();

      if (fConfig.RemoveOld)
      {
        await fResourcesRepository.ClearAsync(cancellationToken);
        await fWoodyPlantRepository.DeleteAsync(cancellationToken);
      }

      fLoaderTask = LoadAsync(fConfig.Interval, fCancellationTokenSource.Token);
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

      while (!cancellationToken.IsCancellationRequested)
      {
        var resources = await fResourcesRepository.GetFalseAsync(cancellationToken);
        foreach (var resource in resources.ConvertAll(x => new Uri(x)))
        {
          try
          {
            var data = await fHttpService.LoadAsync(resource, cancellationToken);
            var documents = data.ToDocument(runVersion);

            await fWoodyPlantRepository.InsertManyAsync(documents, cancellationToken);
            await fResourcesRepository.SetTrueAsync(resource.AbsoluteUri, cancellationToken);

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
          await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
      }
    }
  }
}
