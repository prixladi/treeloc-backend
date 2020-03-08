using System;
using NSubstitute;
using TreeLoc.Facades;
using TreeLoc.Factories;
using TreeLoc.Handlers.Exceptions;
using Xunit;

namespace TreeLoc.UnitTests.Facades
{
  public class ExceptionHandlingFacadeTest
  {
    private readonly IExceptionHandlerFactory fExceptionHandlerFactory;

    public ExceptionHandlingFacadeTest()
    {
      fExceptionHandlerFactory = Substitute.For<IExceptionHandlerFactory>();
    }


    [Fact]
    public void Handle_KnownException_Test()
    {
      var exception = new Exception();
      var handler = Substitute.For<IExceptionHandler>();
      handler
        .When(x => x.Handle(Arg.Is(exception), out Arg.Any<string>(), out Arg.Any<int>()))
        .Do(x => { x[1] = "test"; x[2] = 5; });

      fExceptionHandlerFactory
        .CreateOne(Arg.Is(exception))
        .Returns(handler);

      new ExceptionHandlingFacade(fExceptionHandlerFactory).Handle(exception, out var message, out var statusCode);

      Assert.Equal("test", message);
      Assert.Equal(5, statusCode);
    }

    [Fact]
    public void Handle_UnknownException_Test()
    {
      var exception = new Exception("test");

      fExceptionHandlerFactory
        .CreateOne(Arg.Is(exception))
        .Returns((IExceptionHandler)null);

      new ExceptionHandlingFacade(fExceptionHandlerFactory).Handle(exception, out var message, out var statusCode);

      Assert.Equal("test", message);
      Assert.Equal(500, statusCode);
    }
  }
}
