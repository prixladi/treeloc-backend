using TreeLoc.Database.Documents.Locations;

namespace TreeLoc.Database.Documents
{
  public class Location
  {
    public string? Name { get; set; }
    public GeometryBase? Geometry { get; set; }
  }
}
