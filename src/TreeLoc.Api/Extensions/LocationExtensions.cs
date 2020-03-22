using TreeLoc.Api.Models;
using TreeLoc.Database.Documents;

namespace TreeLoc.Api.Extensions
{
  public static class LocationExtensions
  {
    public static LocationModel ToModel(this LocationDocument? document)
    {
      if (document is null)
        return new LocationModel();

      return new LocationModel
      {
        Name = document.Name,
        Geometry = document.Geometry
      };
    }
  }
}
