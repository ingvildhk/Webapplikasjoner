using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oppg1.Metoder
{
    public class SessionSjekker : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;


            if (session != null && session["Innlogget"] == null || Convert.ToBoolean(session["Innlogget"]) == false)
            {

                filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary
                {
                             {"Controller", "LoggInn" },
                              {"Action", "LoggInn" }
                });

            }
        }
    }
}