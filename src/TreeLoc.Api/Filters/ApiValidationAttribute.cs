using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TreeLoc.Api.Filters
{
  [ExcludeFromCodeCoverage]
  public sealed class ApiValidationAttribute: ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      if (!context.ModelState.IsValid)
        context.Result = new BadRequestObjectResult(context.ModelState);

      base.OnActionExecuting(context);
    }
  }
}
