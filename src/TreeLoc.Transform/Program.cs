using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TreeLoc.Database;
using TreeLoc.Database.Documents;
using TreeLoc.OFN;

namespace TreeLoc.Transform
{
  public class Program
  {
    private static Task Main()
    {
      var serviceCollection = new ServiceCollection();
      var provider = serviceCollection.BuildServiceProvider();

      var source = new CancellationTokenSource();

      return RunAsync(provider, source.Token);
    }

    private static async Task RunAsync(ServiceProvider provider, CancellationToken cancellationToken)
    {
      var woodyPants = new List<Tuple<string, WoodyPlant>>();

      var lines = await File.ReadAllLinesAsync(@"C:\Users\elix7\Repos\treeloc-backend\src\TreeLoc.Transform\Data.csv", cancellationToken);

      var c = new HashSet<string>();

      foreach (var line in lines)
      {
        string[] fields = line.Split(';').Select(p => p.Trim('"')).ToArray();

        woodyPants.Add(Tuple.Create(fields[7], new WoodyPlant
        {
          LocalizedNames = new LocalizedString
          {
            Czech = fields[2]
          },
          LocalizedNotes = new LocalizedString
          {
            Czech = fields[5]
          },
          Type = GetType(fields[1]),
          Location = new Location
          {
            Geometry = GetGeometry(fields[6])
          }
        }));

        c.Add(fields[7]);
      }

      foreach (var group in woodyPants.GroupBy(x => x.Item1, x => x.Item2))
      {
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(group.ToList(), new JsonSerializerSettings()
        {
          ContractResolver = new ContractResolver(),
          NullValueHandling = NullValueHandling.Ignore
        })
        .Replace("\"umístění\":{},", "")
        .Replace("\"členské_dřeviny\":[]", "")
        .Replace("\"obrázek\":[],", "")
        .Replace("\"druh_dřeviny\":{},", "");

        await File.WriteAllTextAsync($"./{group.Key}.json", json, cancellationToken);
      }
    }

    private static string GetType(string type)
    {
      return type switch
      {
        "Skupina stromů" => "https://data.mvcr.gov.cz/zdroj/číselníky/typy-dřevin/položky/skupina-stromů",
        "Stromořadí" => "https://data.mvcr.gov.cz/zdroj/číselníky/typy-dřevin/položky/skupina-stromů",
        "Jednotlivý strom" => "https://data.mvcr.gov.cz/zdroj/číselníky/typy-dřevin/položky/strom",
        _ => "https://data.mvcr.gov.cz/zdroj/číselníky/typy-dřevin/položky/oblast-dřevin",
      };
    }

    private static async Task InsertManyAsync(DbContext context, WoodyPlantDocument[] documents, CancellationToken cancellationToken)
    {
      var collection = context.Database.GetCollection<WoodyPlantDocument>();

      await collection.InsertManyAsync(documents, null, cancellationToken);
    }

    private static Geometry? GetGeometry(string geoString)
    {
      var parts = geoString
        .Split(' ')
        .Select(p => p.Replace("{", ""))
        .Select(p => p.Replace("}", ""))
        .Select(p => p.Replace("X:", ""))
        .Select(p => p.Replace("Y:", ""))
        .Select(p => p.TrimEnd(','))
        .Select(p => p.Replace(',', '.'))
        .Where(p => !string.IsNullOrEmpty(p))
        .Select(p => double.Parse(p))
        .ToArray();

      if (parts.Length == 0)
        return null;

      if ((parts.Length & 1) != 0)
        throw new InvalidOperationException("InvalidCoords");

      var convertor = new CoordsConvertor();

      return parts.Length switch
      {
        2 => GetPoint(convertor, parts[0], parts[1]),
        _ => GetLineString(convertor, parts),
      };
    }

    private static Geometry GetPoint(CoordsConvertor convertor, double x, double y)
    {
      return new Geometry { Type = "Point", Coordinates = convertor.JTSKtoWGS84(x, y).Reverse().ToArray() };
    }

    private static Geometry GetLineString(CoordsConvertor convertor, double[] parts)
    {
      var coordCollection = new double[parts.Length / 2][];
      for (int i = 0; i < parts.Length; i += 2)
        coordCollection[i / 2] = convertor.JTSKtoWGS84(parts[i], parts[i + 1]).Reverse().ToArray();

      return new Geometry { Type = "LineString", Coordinates = coordCollection };
    }
  }
}
