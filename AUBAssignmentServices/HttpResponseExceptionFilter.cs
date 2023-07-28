using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AUBAssignmentServices
{
    public class HttpResponseExceptionFilter : IActionFilter//, IOrderedFilter
    {
        //public int Order => throw new NotImplementedException();

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                context.Result = new ObjectResult(exception.Value)
                {
                    StatusCode = exception.Status
                };
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }

    public class HttpResponseException : ApplicationException
    {
        public int Status { get; set; } = 500;

        public object Value { get; set; }

        public HttpResponseException()
        {

        }

        public HttpResponseException(int status)
        {
            this.Status = status;
        }
    }
}
