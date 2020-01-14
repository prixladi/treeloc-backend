﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TreeLoc.Facades;

namespace TreeLoc.Middlewares
{
  public class ExceptionHandlingMiddleware
  {
    private readonly RequestDelegate fNext;
    private readonly IExceptionHandlingFacade fExceptionHandlingFacade;

    public ExceptionHandlingMiddleware(
       RequestDelegate next,
      IExceptionHandlingFacade exceptionHandlingFacade)
    {
      fNext = next;
      fExceptionHandlingFacade = exceptionHandlingFacade;
    }


    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
         await fNext(context);
      }
      catch(Exception ex)
      {
        fExceptionHandlingFacade.Handle(ex, out var message, out int statusCode);

        var response  = await JsonConvert.SerializeAsync(new MessageModel { Message = message }, context.RequestAborted);

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(response, context.RequestAborted);
      }
    }
  }
}
