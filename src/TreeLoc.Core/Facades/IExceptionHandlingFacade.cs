using System;

namespace TreeLoc.Facades
{
  public interface IExceptionHandlingFacade
  {
    void Handle(Exception ex, out string message, out int statusCode);
  }
}