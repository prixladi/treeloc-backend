using System.Linq;
using Newtonsoft.Json.Linq;
using TreeLoc.Database.Documents;
using TreeLoc.Database.Documents.Locations;
using TreeLoc.OFN;

namespace TreeLoc.Extension
{
  public static class LocationExtensions
  {
    public static LocationDocument ToDocument(this Location? location)
    {
      if (location == null)
        return new LocationDocument();

      return new LocationDocument
      {
        Name = location.Name,
        Geometry = Transform(location.Geometry)
      };
    }

    private static GeometryBase? Transform(Geometry? geometry)
    {
      if (geometry == null)
        return null;

      return geometry.Type switch
      {
        "Point" => ToPointGeometry((JArray)geometry.Coordinates),
        "MultiPoint" => ToMultiPointGeometry((JArray)geometry.Coordinates),
        "LineString" => ToLineStringGeometry((JArray)geometry.Coordinates),
        "MultiLineString" => ToMultiLineStringGeometry((JArray)geometry.Coordinates),
        "Polygon" => ToPolygonGeometry((JArray)geometry.Coordinates),
        "MultiPolygon" => ToMultiPolygonGeometry((JArray)geometry.Coordinates),
        _ => null,
      };
    }

    private static PointGeometry ToPointGeometry(JArray coords)
    {
      return new PointGeometry
      {
        Coordinates = ToDoubleArray(coords)
      };
    }

    private static MultiPointGeometry ToMultiPointGeometry(JArray coords)
    {
      return new MultiPointGeometry
      {
        Coordinates = ToDoubleDoubleArray(coords)
      };
    }

    private static LineStringGeometry ToLineStringGeometry(JArray coords)
    {
      return new LineStringGeometry
      {
        Coordinates = ToDoubleDoubleArray(coords)
      };
    }

    private static MultiLineStringGeometry ToMultiLineStringGeometry(JArray coords)
    {
      return new MultiLineStringGeometry
      {
        Coordinates = ToDoubleDoubleDoubleArray(coords)
      };
    }

    private static PolygonGeometry ToPolygonGeometry(JArray coords)
    {
      return new PolygonGeometry
      {
        Coordinates = ToDoubleDoubleDoubleArray(coords)
      };
    }

    private static MultiPolygonGeometry ToMultiPolygonGeometry(JArray coords)
    {
      return new MultiPolygonGeometry
      {
        Coordinates = ToDoubleDoubleDoubleDoubleArray(coords)
      };
    }

    private static double[][][][] ToDoubleDoubleDoubleDoubleArray(JArray coords)
    {
      return coords.Select(x => ToDoubleDoubleDoubleArray((JArray)x)).ToArray();
    }

    private static double[][][] ToDoubleDoubleDoubleArray(JArray coords)
    {
      return coords.Select(x => ToDoubleDoubleArray((JArray)x)).ToArray();
    }

    private static double[][] ToDoubleDoubleArray(JArray coords)
    {
      return coords.Select(x => ToDoubleArray((JArray)x)).ToArray();
    }

    private static double[] ToDoubleArray(JArray coords)
    {
      return coords.Select(x => (double)x).ToArray();
    }
  }
}
