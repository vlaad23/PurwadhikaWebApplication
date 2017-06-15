using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using PurwadhikaWebApplication.Models;
using PurwadhikaWebApplication.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PurwadhikaWebApplication.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using(AuthRepository _repo = new AuthRepository())
            {
                ApplicationUser user = await _repo.FindUser(context.UserName, context.Password);

                if(user == null)
                {
                    context.Rejected();
                    context.SetError("invalid_grant", "The username or password is incorrect");   
                    return;
                }
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "Student"));
            context.Validated(identity);
        }
    }
}