
using OnlineHotelBookingApplication.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineHotelBookingApplication 
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MapperConfig.Maps();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
