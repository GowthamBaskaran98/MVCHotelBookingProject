using System;
using System.Web.Mvc;

namespace OnlineHotelBookingApplication.App_Start
{
    public class CustomExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is Exception)
            {
            }
        }
    }
}
