using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCore3_1.API.Helpers
{
    public class ExceptionActionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
        }
    }
}