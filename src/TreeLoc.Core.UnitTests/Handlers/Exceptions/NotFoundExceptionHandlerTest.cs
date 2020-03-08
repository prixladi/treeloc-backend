using TreeLoc.Exceptions;
using TreeLoc.Handlers.Exceptions;
using Xunit;

namespace TreeLoc.UnitTests.Handlers.Exceptions
{
  public class NotFoundExceptionHandlerTest
  {
    [Fact]
    public void Handle()
    {
      new NotFoundExceptionHandler().Handle(new NotFoundException("aaa"), out var message, out var code);

      Assert.Equal("aaa", message);
      Assert.Equal(404, code);
    }
  }
}
