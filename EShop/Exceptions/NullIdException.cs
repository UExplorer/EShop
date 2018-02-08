using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShop.Exceptions
{
    public class NullIdException : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled && exceptionContext.Exception is ArgumentException)
            {
                exceptionContext.Result = new RedirectResult("/Content/ArgumentNull.html");
                exceptionContext.ExceptionHandled = true;
            }
        }
    }
}