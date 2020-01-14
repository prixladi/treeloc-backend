using System;

namespace TreeLoc.Handlers.Exceptions
{
  public interface IExceptionHandler
  {
    bool CanHandle(Exception ex);
    void Handle(Exception ex, out string message, out int statusCode);
  }
}
