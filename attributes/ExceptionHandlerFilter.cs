using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNetTest.Attributes;

class ExceptionHandlerFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionHandlerFilter> _logger;
    public ExceptionHandlerFilter(ILogger<ExceptionHandlerFilter> logger)
    {
        _logger = logger;
    }
  
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is NotImplementedException)
        {
            context.Result = new JsonResult(new
            {
                Message = "The feature is not yet implemented",
                ErrorCode = "NotImplemented"
            })
            {
                StatusCode = 501
            };
        }
        else if (context.Exception is UnauthorizedAccessException)
        {
            context.Result = new JsonResult(new
            {
                Message = context.Exception.Message,
                ErrorCode = "Unauthorized"
            })
            {
                StatusCode = 403
            };
        }
        else if (context.Exception is KeyNotFoundException)
        {
            context.Result = new JsonResult(new
            {
                Message = context.Exception.Message,
                ErrorCode = "ResourceNotFound"
            })
            {
                StatusCode = 404
            };
        }
        else if (context.Exception is MissingFieldException || context.Exception is BadHttpRequestException)
        {
            context.Result = new JsonResult(new
            {
                Message = context.Exception.Message,
                ErrorCode = "MissingField"
            })
            {
                StatusCode = 400
            };
        }
        else
        {
            context.Result = new JsonResult(new
            {
                Message = "An unexpected error occurred",
                ErrorCode = "InternalServerError"
            })
            {
                StatusCode = 500
            };
            _logger.LogError("{Stacktrace}", context.Exception.StackTrace);
        }
    }
}