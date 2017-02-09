using System.Web.Mvc;

namespace pip_air.Filters
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter

    {
        public void OnException (ExceptionContext filterContext)
        {
            switch (filterContext.Exception.Message)
            {
                case "400":
                    {
                        filterContext.Result = new RedirectResult("~/Error/NullParameter");
                        filterContext.ExceptionHandled = true;
                        break;
                    }
                case "403":
                    { 
                        filterContext.Result = new RedirectResult("~/Error/Forbidden");
                        filterContext.ExceptionHandled = true;
                        break;
                    }
                case "404":
                    {
                        filterContext.Result = new RedirectResult("~/Error/NotFound");
                        filterContext.ExceptionHandled = true;
                        break;
                    }
                case "410":
                    {
                        filterContext.Result = new RedirectResult("~/Error/Error");
                        filterContext.ExceptionHandled = true;
                        break;
                    }
            }
        }
    }
}