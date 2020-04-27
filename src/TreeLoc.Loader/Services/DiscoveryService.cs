using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TreeLoc.Loader.Repositories;

namespace TreeLoc.Loader.Services
{
  public class DiscoveryService: IHostedService
  {
    private readonly IResourcesRepository fResourcesRepository;
    private Task? fDiscoveryTask;
    private CancellationTokenSource? fCancellationTokenSource;

    public DiscoveryService(IResourcesRepository resourcesRepository)
    {
      fResourcesRepository = resourcesRepository;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      fCancellationTokenSource = new CancellationTokenSource();

      var delay = EnvironmentVariables.Get(EnvironmentVariables._DiscoveryInterval);
      var url = EnvironmentVariables.GetOrThrow(EnvironmentVariables._DiscoveryUrl);

      fDiscoveryTask = LoadAsync(url, TimeSpan.FromSeconds(int.Parse(delay ?? "60")), fCancellationTokenSource.Token);

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

    private async Task LoadAsync(string discoveryUrl, TimeSpan delay, CancellationToken cancellationToken)
    {
      while (true)
      {
        try
        {
          var endpoints = await DiscoveryAsync(discoveryUrl, cancellationToken);

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

    private async Task<string[]> DiscoveryAsync(string url, CancellationToken _)
    {
      var data = await HttpClientContext.Client.GetStringAsync(url);

      return data.Split(Environment.NewLine)
        .Where(s => !string.IsNullOrEmpty(s))
        .ToArray();
    }
  }
}
