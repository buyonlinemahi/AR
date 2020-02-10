using LMGEDIApp;
using System;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity.Configuration;
using Unity.Mvc5;
using Microsoft.Practices.Unity;
using System.Web.Mvc;

namespace LMGEDI
{
    public class Global : System.Web.HttpApplication
    {
        private IUnityContainer container;
        protected void Application_Start(object sender, EventArgs e)
        {
            container = new UnityContainer().LoadConfiguration();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMapping();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}