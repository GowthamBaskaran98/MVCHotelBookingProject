﻿using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineHotelBookingApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Hotel", action = "ManageHotel", Status = "Approved", id = UrlParameter.Optional }
            );
        }   
    }
}
