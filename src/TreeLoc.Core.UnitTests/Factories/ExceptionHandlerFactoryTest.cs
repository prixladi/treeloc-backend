using System;
using TreeLoc.Exceptions;
using TreeLoc.Factories;
using TreeLoc.Handlers.Exceptions;
using Xunit;

namespace TreeLoc.UnitTests.Factories
{
  public class ExceptionHandlerFactoryTest
  {
    [Fact]
    public void CreateOne_Success_Test()
    {
      var handlers = new IExceptionHandler[] { new NotFoundExceptionHandler() };

      var factory = new ExceptionHandlerFactory(handlers);

      Assert.Equal(handlers[0], factory.CreateOne(new NotFoundException("Not found")));
    }

    [Fact]
    public void CreateOne_Fail_Test()
    {
      var handlers = new IExceptionHandler[] { new NotFoundExceptionHandler() };

      var factory = new ExceptionHandlerFactory(handlers);

      Assert.Null(factory.CreateOne(new InvalidOperationException("Not found")));
    }
  }
}
