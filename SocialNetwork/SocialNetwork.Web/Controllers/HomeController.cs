using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
            var claim = User.Claims;
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
    }
}
