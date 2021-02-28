using BankConfigurationPortal.Models;
using BusinessCommon.ExceptionsWriter;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BankCP.Models
{
    public class AppOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public AppOAuthProvider(string publicClientId)
        {
            try
            {
                if (publicClientId == null)
                {
                    throw new ArgumentNullException(nameof(publicClientId));
                }
                _publicClientId = publicClientId;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
            }
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                string usernameVal = context.UserName;
                string passwordVal = context.Password;
                BusinessObjects.Models.User user = UserSecurity.Login(usernameVal, passwordVal);
                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    context.Rejected();
                }
                else
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim("BankId", user.bankId.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.userName));
                    ClaimsIdentity oAuthClaimIdentity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                    ClaimsIdentity cookiesClaimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);
                    AuthenticationProperties properties = CreateProperties(user.userName);
                    AuthenticationTicket ticket = new AuthenticationTicket(oAuthClaimIdentity, properties);
                    context.Validated(ticket);
                    context.Request.Context.Authentication.SignIn(cookiesClaimIdentity);
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            try
            {
                foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
                {
                    context.AdditionalResponseParameters.Add(property.Key, property.Value);
                }
                return Task.FromResult<object>(null);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            try
            {
                if (context.ClientId == null)
                {
                    context.Validated();
                }
                return Task.FromResult<object>(null);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            try
            {
                if (context.ClientId == _publicClientId)
                {
                    Uri expectedRootUri = new Uri(context.Request.Uri, "/");
                    if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                    {
                        context.Validated();
                    }
                }
                return Task.FromResult<object>(null);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            try
            {
                IDictionary<string, string> data = new Dictionary<string, string> { { "userName", userName } };
                return new AuthenticationProperties(data);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
    }
}