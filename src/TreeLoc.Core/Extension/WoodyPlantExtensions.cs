using System;
using System.Collections.Generic;
using System.Linq;
using TreeLoc.Database.Documents;
using TreeLoc.OFN;

namespace TreeLoc.Extension
{
  public static class WoodyPlantExtensions
  {
    public static WoodyPlantDocument ToDocument(this WoodyPlant plant, string? version = null)
    {
      if (plant is null)
        throw new ArgumentNullException(nameof(plant));

      return new WoodyPlantDocument
      {
        ImageUrls = plant.ImageUrls,
        LocalizedNames = plant.LocalizedNames.ToDocument(),
        LocalizedNotes = plant.LocalizedNotes.ToDocument(),
        LocalizedSpecies = plant.LocalizedSpecies.ToDocument(),
        Location = plant.Location.ToDocument(),
        Type = plant.Type.ToType(),
        Version = version
      };
    }

    public static WoodyPlantDocument[] ToDocument(this IEnumerable<WoodyPlant> plants, string? version = null)
    {
      if (plants is null)
        throw new ArgumentNullException(nameof(plants));

      return plants
        .Select(x => x.ToDocument(version))
        .ToArray();
    }

    private static PlantType ToType(this string? typeString)
    {
      return typeString switch
      {
        "https://data.mvcr.gov.cz/zdroj/číselníky/typy-dřevin/položky/strom" => PlantType.Tree,
        "https://data.mvcr.gov.cz/zdroj/číselníky/typy-dřevin/položky/skupina-stromů" => PlantType.TreeGroup,
        _ => PlantType.AreaOfTrees,
      };
    }
  }
}
