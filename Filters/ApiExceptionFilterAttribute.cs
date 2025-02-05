using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Task_Web_API.Middlewares;

namespace Task_Web_API.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is TaskNotFoundException)
            {
                context.Result = new NotFoundObjectResult(new
                {
                    context.Exception.Message
                });
                context.ExceptionHandled = true;
            }
        }
    }
}
