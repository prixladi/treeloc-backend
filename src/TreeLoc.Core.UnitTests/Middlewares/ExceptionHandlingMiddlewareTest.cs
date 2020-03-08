using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using TreeLoc.Exceptions;
using TreeLoc.Facades;
using TreeLoc.Middlewares;
using Xunit;

namespace TreeLoc.UnitTests.Middlewares
{
  public class ExceptionHandlingMiddlewareTest
  {
    private readonly IExceptionHandlingFacade fExceptionHandlingFacade;

    public ExceptionHandlingMiddlewareTest()
    {
      fExceptionHandlingFacade = Substitute.For<IExceptionHandlingFacade>();
    }

    [Fact]
    public async Task Invoke_Exception_Handle_TestAsync()
    {
      var exception = new NotFoundException("test");
      Task next(HttpContext context) => throw exception;
      var context = Substitute.For<HttpContext>();
      var response = Substitute.For<HttpResponse>();
      var body = Substitute.For<Stream>();

      response.Body.Returns(body);   
      context.Response.Returns(response);

      await new ExceptionHandlingMiddleware(next, fExceptionHandlingFacade).InvokeAsync(context);

      var message = new ResposeMessageModel();
      var bytes = Encoding.UTF8.GetBytes(await JsonConvert.SerializeAsync(message, default));
      await body
        .Received()
        .WriteAsync(Arg.Any<byte[]>(), Arg.Is(0), Arg.Is(bytes.Length), Arg.Is(default(CancellationToken)));
    }

    [Fact]
    public async Task Invoke_500Exception_Handle_TestAsync()
    {
      var exception = new NotFoundException("test");
      Task next(HttpContext context) => throw exception;
      var context = Substitute.For<HttpContext>();
      var response = Substitute.For<HttpResponse>();
      var body = Substitute.For<Stream>();

      response.Body.Returns(body);
      context.Response.Returns(response);

      fExceptionHandlingFacade
        .When(x => x.Handle(Arg.Is(exception), out Arg.Any<string>(), out Arg.Any<int>()))
        .Do(x => x[2] = 500);
        
      await Assert.ThrowsAsync<NotFoundException>(async () => await new ExceptionHandlingMiddleware(next, fExceptionHandlingFacade).InvokeAsync(context));
    }

    [Fact]
    public async Task Invoke_Handle_TestAsync()
    {
      var exception = new NotFoundException("test");
      static Task next(HttpContext context) => Task.CompletedTask;
      var context = Substitute.For<HttpContext>();

      await new ExceptionHandlingMiddleware(next, fExceptionHandlingFacade).InvokeAsync(context);
    }
  }
}
