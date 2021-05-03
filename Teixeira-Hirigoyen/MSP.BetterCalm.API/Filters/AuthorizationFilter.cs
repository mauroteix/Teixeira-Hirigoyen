using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MSP.BetterCalm.BusinessLogicInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSP.BetterCalm.API.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly ISessionLogic sessions;

        public AuthorizationFilter(ISessionLogic sessionsLogic)
        {
            this.sessions = sessionsLogic;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string headerToken = context.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(headerToken))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Administrador no logeado."
                };
            }
            else
            {
                try
                {
                    Guid token = Guid.Parse(headerToken);
                    if (!sessions.IsCorrectToken(token))
                    {
                        context.Result = new ContentResult()
                        {
                            StatusCode = 403,
                            Content = "Token invalido."
                        };
                    }
                }
                catch (FormatException)
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 401,
                        Content = "Formato invalido de token"
                    };
                }
            }
        }
    }
}

