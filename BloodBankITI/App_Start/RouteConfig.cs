﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BloodBankITI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            

            routes.MapRoute(
                name: "Default3",
                url: "{controller}/{action}/{id}/{name}",
                defaults: new { controller = "Home", action = "Index", nid = UrlParameter.Optional , name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default2",
                url: "{controller}/{action}/{nid}/{did}",
                defaults: new { controller = "Home", action = "Index", nid = UrlParameter.Optional, did = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default4",
                url: "{controller}/{action}/{id:int}/{count:int}/{fname:alpha}/{lname:alpha}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, count = UrlParameter.Optional, fname = UrlParameter.Optional, lname = UrlParameter.Optional }
            );
        }
    }
}
