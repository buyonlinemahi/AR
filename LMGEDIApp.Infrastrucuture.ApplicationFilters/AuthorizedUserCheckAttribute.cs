using LMGEDIApp.Infrastructure.Global;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LMGEDIApp.Infrastructure.ApplicationFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AuthorizedUserCheckAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool _authorize = httpContext.Request.Cookies[GlobalConst.SessionKeys.userDetailMgmt] != null;
            if (!_authorize)
                httpContext.Items["redirectToLogin"] = true;

            return _authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Items.Contains("redirectToLogin"))
            {
                var routeValues = new RouteValueDictionary(new
                {
                    controller = GlobalConst.Controllers.User,
                    action = GlobalConst.Actions.UserController.Index,
                });
                filterContext.Result = new RedirectToRouteResult(routeValues);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}