using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TreeLoc.Extension;
using TreeLoc.Loader.Repositories;
using TreeLoc.OFN;

namespace TreeLoc.Loader.Services
{
  public class LoaderService: IHostedService
  {
    private static readonly HttpClient fContext = new HttpClient();

    private static readonly HashSet<string> fResources = new HashSet<string>
    {
      "Hl.m. Praha",
      "Jihomoravský",
      "Jihočeský",
      "Karlovarský",
      "Královéhradecký",
      "Liberecký",
      "Moravskoslezský",
      "Olomoucký",
      "Pardubický",
      "Plzeňský",
      "Středočeský",
      "Vysočina",
      "Zlínský",
      "Ústecký"
    };

    private readonly IWoodyPlantRepository fWoodyPlantRepository;
    private Task? fLoaderTask;
    private CancellationTokenSource? fCancellationTokenSource;

    public LoaderService(IWoodyPlantRepository woodyPlantRepository)
    {
      fWoodyPlantRepository = woodyPlantRepository;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      fCancellationTokenSource = new CancellationTokenSource();

      var delay = EnvironmentVariables.Get(EnvironmentVariables._LoaderDelay);

      fLoaderTask = LoadAsync(TimeSpan.FromSeconds(int.Parse(delay ?? "60")), fCancellationTokenSource.Token);

      return Task.CompletedTask;
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
      string version = DateTime.UtcNow.GetHashCode().ToString();
      foreach(var resource in fResources)
      {
        try
        {
          var data = await LoadAsync(resource, cancellationToken);
          var documents = data.ToDocument(version);

          await fWoodyPlantRepository.InsertManyAsync(documents, cancellationToken);
          await fWoodyPlantRepository.DelteInvalidAsync(version, cancellationToken);

          Console.WriteLine($"Successfuly loaded data from resource '{resource}'");
        }
        catch(Exception ex)
        {
          Console.WriteLine($"Error loading data from resource '{resource}'");
          Console.WriteLine(ex);
        }

        await Task.Delay(delay, cancellationToken);
      }
    }

    private async Task<WoodyPlant[]> LoadAsync(string resource, CancellationToken cancellationToken)
    {
      const string baseAddress = "https://raw.githubusercontent.com/prixladi/treeloc-data/master/";

      var builder = new UriBuilder(baseAddress);
      builder.Path = Path.Combine(builder.Path, $"{resource}.json");

      var data = await fContext.GetStringAsync(builder.Uri);

     return Newtonsoft.Json.JsonConvert.DeserializeObject<WoodyPlant[]>(data);
    }
  }
}
