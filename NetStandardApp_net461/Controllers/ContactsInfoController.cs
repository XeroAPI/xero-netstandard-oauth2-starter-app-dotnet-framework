using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using Xero.NetStandard.OAuth2.Model;
using Xero.NetStandard.OAuth2.Token;
using Xero.NetStandard.OAuth2.Api;
using Xero.NetStandard.OAuth2.Config;
using Xero.NetStandard.OAuth2.Client;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace NetStandardApp_net461.Controllers
{
    public class ContactsInfoController : Controller
    {
        // GET: ContactsInfo
        public async Task<ActionResult> Index()
        {
            var xeroToken = TokenUtilities.GetStoredToken();
            var utcTimeNow = DateTime.UtcNow;

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

            if (utcTimeNow > xeroToken.ExpiresAtUtc)
            {
                var client = new XeroClient(XeroConfig, httpClientFactory);
                xeroToken = (XeroOAuth2Token)await client.RefreshAccessTokenAsync(xeroToken);
                TokenUtilities.StoreToken(xeroToken);
            }

            string accessToken = xeroToken.AccessToken;
            string xeroTenantId = xeroToken.Tenants[0].TenantId.ToString();

            var AccountingApi = new AccountingApi();
            var response = await AccountingApi.GetContactsAsync(accessToken, xeroTenantId);

            var contacts = response._Contacts;

            return View(contacts);
        }


        // GET: /Contacts#Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Contacts#Create
        [HttpPost]
        public async Task<ActionResult> Create(string Name, string EmailAddress)
        {
            var xeroToken = TokenUtilities.GetStoredToken();
            var utcTimeNow = DateTime.UtcNow;

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

            if (utcTimeNow > xeroToken.ExpiresAtUtc)
            {
                var client = new XeroClient(XeroConfig, httpClientFactory);
                xeroToken = (XeroOAuth2Token)await client.RefreshAccessTokenAsync(xeroToken);
                TokenUtilities.StoreToken(xeroToken);
            }

            string accessToken = xeroToken.AccessToken;
            string xeroTenantId = xeroToken.Tenants[0].TenantId.ToString();

            var contact = new Contact
            {
                Name = Name,
                EmailAddress = EmailAddress
            };

            var contacts = new Contacts();
            contacts._Contacts = new List<Contact>() { contact };

            var AccountingApi = new AccountingApi();
            var response = await AccountingApi.CreateContactsAsync(accessToken, xeroTenantId, contacts);

            return RedirectToAction("Index", "ContactsInfo");
        }
    }
}