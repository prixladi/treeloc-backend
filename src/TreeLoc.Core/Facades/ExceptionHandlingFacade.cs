using System;
using Microsoft.AspNetCore.Http;
using TreeLoc.Factories;

namespace TreeLoc.Facades
{
  public class ExceptionHandlingFacade: IExceptionHandlingFacade
  {
    private readonly IExceptionHandlerFactory fExceptionHandlerFactory;

    public ExceptionHandlingFacade(IExceptionHandlerFactory exceptionHandlerFactory)
    {
      fExceptionHandlerFactory = exceptionHandlerFactory;
    }

    public void Handle(Exception ex, out string message, out int statusCode)
    {
      var handler = fExceptionHandlerFactory.CreateOne(ex);
      if (handler != null)
        handler.Handle(ex, out message, out statusCode);
      else
      {
        message = ex.Message;
        statusCode = StatusCodes.Status500InternalServerError;
      }
    }
  }
}
