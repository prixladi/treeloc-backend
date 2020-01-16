using System.Buffers;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace TreeLoc
{
  public static class JsonConvert
  {
    public static async Task<string> SerializeAsync<T>(T value, JsonSerializerOptions? options, CancellationToken cancellationToken)
    {
      using var stream = new MemoryStream();
      await JsonSerializer.SerializeAsync(stream, value, options, cancellationToken);
      var jsonLength = checked((int)stream.Length);

      using (var owner = MemoryPool<byte>.Shared.Rent(jsonLength))
      {
        var memory = owner.Memory.Slice(0, jsonLength);
        stream.Seek(0, SeekOrigin.Begin);
        await stream.ReadAsync(memory, cancellationToken);
        return Encoding.UTF8.GetString(memory.Span);
      }
    }

    public static Task<string> SerializeAsync<T>(T value, JsonConverter converter, CancellationToken cancellationToken)
    {
      var options = new JsonSerializerOptions();
      options.Converters.Add(converter);

      return SerializeAsync(value, options, cancellationToken);
    }

    public static Task<string> SerializeAsync<T>(T value, CancellationToken cancellationToken)
    {
      return SerializeAsync(value, options: null, cancellationToken);
    }
  }
}
