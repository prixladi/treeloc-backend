using Microsoft.AspNetCore.Http;
using TreeLoc.Exceptions;

namespace TreeLoc.Handlers.Exceptions
{
  public class NotFoundExceptionHandler: ExceptionHandlerBase<NotFoundException>
  {
    protected override int GetStatusCode(NotFoundException exception)
    {
      return StatusCodes.Status404NotFound;
    }
  }
}
