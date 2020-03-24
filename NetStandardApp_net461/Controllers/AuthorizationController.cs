using System;
using System.Configuration;
using System.Web.Mvc;
using System.Net.Http;
using Xero.NetStandard.OAuth2.Client;
using Xero.NetStandard.OAuth2.Config;
using Microsoft.Extensions.DependencyInjection;
using Xero.NetStandard.OAuth2.Token;
using Xero.NetStandard.OAuth2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetStandardApp_net461.Controllers
{
    public class AuthorizationController : Controller
    {

        // GET: /Authorization
        public ActionResult Index()
        {
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

            XeroConfiguration XeroConfig = new XeroConfiguration
            {
                ClientId = ConfigurationManager.AppSettings["XeroClientId"],
                ClientSecret = ConfigurationManager.AppSettings["XeroClientSecret"],
                CallbackUri = new Uri(ConfigurationManager.AppSettings["XeroCallbackUri"]),
                Scope = ConfigurationManager.AppSettings["XeroScope"],
                State = ConfigurationManager.AppSettings["XeroState"]
            };

            var client = new XeroClient(XeroConfig, httpClientFactory);

            return Redirect(client.BuildLoginUri());
        }

        // GET: /Authorization/Callback
        public async Task<ActionResult> Callback(string code, string state)
        {
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

            XeroConfiguration XeroConfig = new XeroConfiguration
            {
                ClientId = ConfigurationManager.AppSettings["XeroClientId"],
                ClientSecret = ConfigurationManager.AppSettings["XeroClientSecret"],
                CallbackUri = new Uri(ConfigurationManager.AppSettings["XeroCallbackUri"]),
                Scope = ConfigurationManager.AppSettings["XeroScope"],
                State = ConfigurationManager.AppSettings["XeroState"]
            };

            var client = new XeroClient(XeroConfig, httpClientFactory);

            var xeroToken = (XeroOAuth2Token)await client.RequestXeroTokenAsync(code);
            List<Tenant> tenants = await client.GetConnectionsAsync(xeroToken);

            Tenant firstTenant = tenants[0];

            TokenUtilities.StoreToken(xeroToken);

            return RedirectToAction("Index", "OrganisationInfo");
        }


    }
}