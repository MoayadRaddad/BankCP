using BankCP.Models;
using BusinessCommon.ExceptionsWriter;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(BankCP.Startup))]

namespace BankCP
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static string PublicClientId { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            try
            {
                ConfigureAuth(app);
                ConfigureOAuth(app);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
            }
        }

        private static void ConfigureAuth(IAppBuilder app)
        {
            try
            {
                app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    ExpireTimeSpan = TimeSpan.FromMinutes(30),
                    CookieName = "BankConfigurationPortal",
                    LoginPath = new PathString("/Login/login")
                });
                AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
            }
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            try
            {
                OAuthOptions = new OAuthAuthorizationServerOptions
                {
                    TokenEndpointPath = new PathString("/api/Token"),
                    Provider = new AppOAuthProvider("self"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                    AllowInsecureHttp = true //Don't do this in production ONLY FOR DEVELOPING: ALLOW INSECURE HTTP!
                };
                app.UseOAuthBearerTokens(OAuthOptions);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
            }
        }
    }
}