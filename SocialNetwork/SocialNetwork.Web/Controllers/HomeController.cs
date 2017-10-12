using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using SocialNetwork.Web.Models;

namespace SocialNetwork.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Shouts()
        {
            await RefreshTokens();

            var token = await HttpContext.Authentication.GetTokenAsync("access_token");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var shoutsResponse = await (await client.GetAsync($"http://localhost:33917/api/shouts")).Content.ReadAsStringAsync();

                var shouts = JsonConvert.DeserializeObject<Shout[]>(shoutsResponse);
                
                return View(shouts);
            }
        }

        public IActionResult Login()
        { 
            return View();
        }


        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        private async Task RefreshTokens()
        {
            var authServerInfo = await DiscoveryClient.GetAsync("http://localhost:5000");
            var client = new TokenClient(authServerInfo.TokenEndpoint, "socialnetwork_code", "secret");

            var refreshToken = await HttpContext.Authentication.GetTokenAsync("refresh_token");
            var idToken = await HttpContext.Authentication.GetTokenAsync("id_token");
            var tokenResponse = await client.RequestRefreshTokenAsync(refreshToken);

            var tokens = new List<AuthenticationToken>
            {
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.IdToken,
                    Value = idToken
                },
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.RefreshToken,
                    Value = tokenResponse.RefreshToken
                },
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.AccessToken,
                    Value = tokenResponse.AccessToken
                },
                new AuthenticationToken
                {
                    Name = "expires_at",
                    Value = (DateTime.UtcNow + TimeSpan.FromSeconds(tokenResponse.ExpiresIn)).ToString("o", CultureInfo.InvariantCulture)
                },

            };

            var authInfo = await HttpContext.Authentication.GetAuthenticateInfoAsync("Cookies");
            authInfo.Properties.StoreTokens(tokens);

            await HttpContext.Authentication.SignInAsync("Cookies", authInfo.Principal, authInfo.Properties);
        }
    }
}
