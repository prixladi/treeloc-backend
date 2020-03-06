using System;
using TreeLoc.Handlers.Exceptions;

namespace TreeLoc.Factories
{
  public interface IExceptionHandlerFactory
  {
    IExceptionHandler? CreateOne(Exception ex);
  }
}