using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MongoDB.Bson.Serialization.Attributes;
using TreeLoc.Database.Documents.Locations;
using Xunit;

namespace TreeLoc.UnitTests.Database.Documents.Locations
{
  public class GeometryTest
  {
    private readonly  List<string> fExpectedTypes;

    public GeometryTest()
    {
      fExpectedTypes = new List<string>()
      {
        "Point",
        "MultiPoint",
        "LineString",
        "MultiLineString",
        "Polygon",
        "MultiPolygon"
      };
    }

    [Fact]
    public void KnownTypes_Test()
    {
      var attribute = typeof(GeometryBase).GetCustomAttribute<BsonKnownTypesAttribute>();
      Assert.NotNull(attribute);

      var knownTypes = new List<string>();

      foreach(var type in attribute.KnownTypes)
        knownTypes.Add(ValidateType(type));

      Assert.Empty(fExpectedTypes.Except(knownTypes));
      Assert.Empty(knownTypes.Except(fExpectedTypes));
    }

    private string ValidateType(Type type)
    {
      var geometry = (GeometryBase)Activator.CreateInstance(type);
      var concreteType = geometry.GetType();
      var propetry = concreteType.GetProperty("Coordinates");

      Assert.NotNull(propetry);

      var propType = propetry.PropertyType;
      
      propetry.SetValue(geometry, Activator.CreateInstance(propType, 0));
      propetry.GetValue(geometry);

      return geometry.Type = geometry.Type;
    }
  }
}
