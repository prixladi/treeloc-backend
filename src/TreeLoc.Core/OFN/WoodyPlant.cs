using System;
using Newtonsoft.Json;

namespace TreeLoc.OFN
{
  public class WoodyPlant
  {
    [JsonProperty("název")]
    public LocalizedString LocalizedNames { get; set; } = new LocalizedString();

    [JsonProperty("poznámka")]
    public LocalizedString LocalizedNotes { get; set; } = new LocalizedString();

    [JsonProperty("druh_dřeviny")]
    public LocalizedString LocalizedSpecies { get; set; } = new LocalizedString();

    [JsonProperty("obrázek")]
    public string[] ImageUrls { get; set; } = Array.Empty<string>();

    [JsonProperty("typ_dřeviny")]
    public string? Type { get; set; }

    [JsonProperty("umístění")]
    public Location? Location { get; set; } = new Location();

    [JsonProperty("členské_dřeviny")]
    public WoodyPlant[] WoodyPlants { get; set; } = Array.Empty<WoodyPlant>(); 
  }
}
