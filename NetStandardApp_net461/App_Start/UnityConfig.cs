using System.Web.Mvc;
using System.Net.Http;
using Unity;
using Unity.Mvc5;
using Xero.NetStandard.OAuth2.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetStandardApp_net461.Controllers;


namespace NetStandardApp_net461
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            // container.RegisterType<IOptions, Options>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

        }
    }
}