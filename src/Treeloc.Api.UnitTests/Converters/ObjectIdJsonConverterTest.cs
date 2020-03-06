using System;
using MongoDB.Bson;
using Newtonsoft.Json;
using NSubstitute;
using TreeLoc.Api.Converters;
using Xunit;

namespace Treeloc.Api.UnitTests.Converters
{
  public class ObjectIdJsonConverterTest
  {
    [Fact]
    public void CanConvert_Success_Test()
    {
      Assert.True(new ObjectIdJsonConverter().CanConvert(typeof(ObjectId)));
    }

    [Theory]
    [InlineData(typeof(string))]
    [InlineData(typeof(int))]
    [InlineData(null)]
    public void CanConvert_Fail_Test(Type type)
    {
      Assert.False(new ObjectIdJsonConverter().CanConvert(type));
    }

    [Fact]
    public void ReadJson_Success_Nullable_Test()
    {
      var reader = Substitute.For<JsonReader>();

      reader.TokenType.Returns(JsonToken.Null);

      Assert.Null(new ObjectIdJsonConverter().ReadJson(reader, typeof(ObjectId?), null, new JsonSerializer()));
    }

    [Fact]
    public void ReadJson_Success_ValidObjectId_Test()
    {
      var reader = Substitute.For<JsonReader>();
      var id = ObjectId.GenerateNewId();
      reader.TokenType.Returns(JsonToken.String);
      reader.Value.Returns(id.ToString());

      var result = new ObjectIdJsonConverter().ReadJson(reader, typeof(ObjectId), id, new JsonSerializer());

      Assert.NotNull(result);
      Assert.Equal(id, result);
    }

    [Fact]
    public void ReadJson_Exception_InvalidOperation_Test()
    {
      var reader = Substitute.For<JsonReader>();

      reader.TokenType.Returns(JsonToken.Null);

      Assert.Throws<InvalidOperationException>(() => new ObjectIdJsonConverter().ReadJson(reader, typeof(ObjectId), null, new JsonSerializer()));
    }

    [Fact]
    public void ReadJson_Exception_InvalidObject_Test()
    {
      var reader = Substitute.For<JsonReader>();
      var id = "invalid id";
      reader.TokenType.Returns(JsonToken.String);
      reader.Value.Returns(id);

      Assert.Throws<JsonException>(() => new ObjectIdJsonConverter().ReadJson(reader, typeof(ObjectId), id, new JsonSerializer()));
    }

    [Fact]
    public void WriteJson_Success_Test()
    {
      var writer = Substitute.For<JsonWriter>();
      var id = ObjectId.GenerateNewId();

      new ObjectIdJsonConverter().WriteJson(writer, id, new JsonSerializer());

      writer
       .Received()
       .WriteValue(Arg.Is<string>(x => x == id.ToString()));
    }

    [Theory]
    [InlineData(5)]
    [InlineData("ss")]
    [InlineData(null)]
    public void WriteJson_Exception_Test(object? value)
    {
      var writer = Substitute.For<JsonWriter>();

      Assert.Throws<ArgumentException>(() => new ObjectIdJsonConverter().WriteJson(writer, value, new JsonSerializer()));
    }
  }
}
