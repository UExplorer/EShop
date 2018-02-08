using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShop.Exceptions
{
    /// <summary>
    /// Exception for users, who is get ArgumentException
    /// </summary>
    public class NullIdException : FilterAttribute, IExceptionFilter
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled && exceptionContext.Exception is ArgumentException) //Catch our exc
            {
                exceptionContext.Result = new RedirectResult("/Content/ArgumentNull.html"); // User get that page
                logger.Error($"Another User get ArgumentException");
                exceptionContext.ExceptionHandled = true; // exc handled, huray
            }
        }
    }
}