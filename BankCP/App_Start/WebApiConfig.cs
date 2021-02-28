
using BusinessCommon.ExceptionsWriter;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;

namespace BankCP.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            try
            {
                config.SuppressDefaultHostAuthentication();
                config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
                config.MapHttpAttributeRoutes();

                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
            }
        }
    }
}