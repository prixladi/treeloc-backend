using Newtonsoft.Json.Linq;

namespace TreeLoc.OFN
{
  public class Geometry
  {
    public string Type { get; set; } = default!;
    public object Coordinates { get; set; } = default!;
  }
}
