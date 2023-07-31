using System.Net;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using WebAPi_Ado;

namespace Rest.BasicAuth
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                context.Result = new UnauthorizedObjectResult("Login Failed");
                return;
            }

            string authToken = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Basic ", "");

            try
            {
                string decodedAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));

                string[] usernamePassword = decodedAuthToken.Split(':');
                string username = usernamePassword[0];
                string password = usernamePassword[1];

                if (!ValidateUser.Login(username, password))
                {
                    context.Result = new UnauthorizedObjectResult("Login Failed");
                    return;
                }

                context.HttpContext.User = new GenericPrincipal(new GenericIdentity(username), null);
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedObjectResult("Invalid Authorization Header");
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}