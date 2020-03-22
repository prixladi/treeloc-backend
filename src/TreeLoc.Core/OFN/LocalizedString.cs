using Newtonsoft.Json;

namespace TreeLoc.OFN
{
  public class LocalizedString
  {
    [JsonProperty("cs")]
    public string? Czech { get; set; }
  }
}
