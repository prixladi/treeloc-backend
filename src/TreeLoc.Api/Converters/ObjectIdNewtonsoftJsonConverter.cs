using System;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace TreeLoc.Api.Converters
{
  public class ObjectIdNewtonsoftJsonConverter: JsonConverter
  {
    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof(ObjectId)
        || objectType == typeof(ObjectId?);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
      if (reader == null)
        throw new ArgumentNullException(nameof(reader));

      if (reader.TokenType == JsonToken.Null && objectType == typeof(ObjectId?))
        return null;

      if (reader.TokenType != JsonToken.String)
        throw new InvalidOperationException($"Unexpected token while convering '{typeof(ObjectId)}'. Očekáván '{JsonToken.String}', přijato {reader.TokenType}.");

      string? value = (string?)reader.Value;
      if (ObjectId.TryParse(value, out var objectId))
        return objectId;
      else
        throw new JsonException($"String '{value}' cannot be converted to '{typeof(ObjectId)}'.");
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
      if (writer == null)
        throw new ArgumentNullException(nameof(writer));

      if (value is ObjectId objectId)
        writer.WriteValue(objectId.ToString());
      else
        throw new ArgumentException($"Parameter is not of type '{typeof(ObjectId)}'.", nameof(value));
    }
  }
}
