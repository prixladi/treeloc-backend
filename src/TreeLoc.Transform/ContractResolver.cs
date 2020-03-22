using System.Collections;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TreeLoc.OFN;

namespace TreeLoc.Transform
{
  public class ContractResolver: DefaultContractResolver
  {
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
      var property = base.CreateProperty(member, memberSerialization);

      if (property.DeclaringType!.IsArray)
      {
        property.ShouldSerialize = instance =>
        {
          if (instance is IEnumerable enumer)
            return enumer.GetEnumerator().MoveNext();
          else
            return true;
        };
      }

      if (property.DeclaringType == typeof(LocalizedString))
      {
        property.ShouldSerialize = instance =>
        {
          if (instance is LocalizedString str)
            return str.Czech != null;
          else
            return true;
        };
      }

      if (property.DeclaringType == typeof(Location))
      {
        property.ShouldSerialize = instance =>
        {
          if (instance is Location loc)
            return loc.Name != null || loc.Geometry != null;
          else
            return true;
        };
      }

      return property;
    }
  }
}
