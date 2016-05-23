using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace BloodBankService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Default2Api",
                routeTemplate: "api/{controller}/{id}/{id2}",
                defaults: new { id = RouteParameter.Optional , id2 = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "Default3Api",
                routeTemplate: "api/{controller}/{id}/{id2}/{id3}",
                defaults: new { id = RouteParameter.Optional, id2 = RouteParameter.Optional, id3 = RouteParameter.Optional }
            );
        }
    }
}
