using System;
using System.Net;
using AspBusiness.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspCoreAPIStarter.Handlers
{
    public class UnHandledExceptionHandle: IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is { } exception)
            {
                if (!(exception is BaseException))
                {
                    context.Result = new ObjectResult(exception.Message)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };

                    var ex = exception;
                    while (ex != null)
                    {
                        Console.Error.WriteLine(context.Exception.Message);
                        Console.Error.WriteLine(context.Exception.StackTrace);
                        ex = ex.InnerException;
                    }
                }
            }
        }
    }
}