using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson;

namespace TreeLoc.Api.Converters
{
  public class ObjectIdJsonConverter: JsonConverter<ObjectId>
  {
    public override ObjectId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ObjectId value, JsonSerializerOptions options)
    {
      writer.WriteStringValue(value.ToString());
    }
  }
}
