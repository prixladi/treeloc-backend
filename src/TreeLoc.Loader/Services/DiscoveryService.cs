using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TreeLoc.Loader.Configs;
using TreeLoc.Loader.Repositories;

namespace TreeLoc.Loader.Services
{
  public class DiscoveryService: IHostedService
  {
    private readonly IHttpService fHttpService;
    private readonly IResourcesRepository fResourcesRepository;
    private readonly IDiscoveryServiceConfig fConfig;

    private Task? fDiscoveryTask;
    private CancellationTokenSource? fCancellationTokenSource;

    public DiscoveryService(
      IHttpService httpService,
      IResourcesRepository resourcesRepository, 
      IDiscoveryServiceConfig config)
    {
      fHttpService = httpService;
      fResourcesRepository = resourcesRepository;
      fConfig = config;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      fCancellationTokenSource = new CancellationTokenSource();
      var url = new Uri(fConfig.Url);
      fDiscoveryTask = LoadAsync(url, fConfig.Interval, fCancellationTokenSource.Token);
      return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
      if (fDiscoveryTask == null)
        return;

      try
      {
        fCancellationTokenSource!.Cancel();
      }
      finally
      {
        await Task.WhenAny(fDiscoveryTask, Task.Delay(Timeout.Infinite, cancellationToken));
        fDiscoveryTask = null;
      }
    }

    private async Task LoadAsync(Uri discoveryUrl, TimeSpan delay, CancellationToken cancellationToken)
    {
      while (!cancellationToken.IsCancellationRequested)
      {
        try
        {
          var endpoints = await fHttpService.DiscoveryAsync(discoveryUrl, cancellationToken);

          Console.WriteLine($"Discovered '${discoveryUrl}', found endpoints: '{string.Join(',', endpoints)}'");

          foreach (var endpoint in endpoints)
            fResourcesRepository.Add(new Uri(endpoint));
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Error while discovering endpoint '${discoveryUrl}'.");
          Console.WriteLine(ex);
        }

        await Task.Delay(delay, cancellationToken);
      }
    }
  }
}
