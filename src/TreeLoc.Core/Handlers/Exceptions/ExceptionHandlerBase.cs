using System;

namespace TreeLoc.Handlers.Exceptions
{
  public abstract class ExceptionHandlerBase<TException>: IExceptionHandler
    where TException : Exception
  {
    public bool CanHandle(Exception ex)
    {
      return ex is TException;
    }

    public void Handle(Exception ex, out string message, out int statusCode)
    {
      message = GetMessage((TException)ex);
      statusCode = GetStatusCode((TException)ex);
    }

    protected abstract int GetStatusCode(TException exception);

    protected virtual string GetMessage(TException exception)
    {
      return exception.Message;
    }
  }
}
