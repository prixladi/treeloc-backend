using Newtonsoft.Json;

namespace TreeLoc.OFN
{
  public class Location
  {
    [JsonProperty("název")]
    public string? Name { get; set; }

    [JsonProperty("geometrie")]
    public Geometry? Geometry { get; set; }
  }
}
