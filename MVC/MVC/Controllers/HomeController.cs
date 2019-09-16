using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            string message = JsonConvert.SerializeObject(model, Formatting.None);
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(message, Encoding.UTF8, "application/json");
            var loginRequest = await httpClient.PostAsync(new Uri("https://localhost:44310/api/auth/login"), content);

            var jsonResponse = await loginRequest.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(jsonResponse))
            {
                var result = JsonConvert.DeserializeObject<Tokens>(jsonResponse);

                Response.Cookies.Append(
                    "AuthToken",
                    result.Token,
                    new CookieOptions
                    {
                        Expires = result.Expiration,
                        IsEssential = true,
                        
                    }
                );
                return RedirectToAction("Values");
            }
            return View();
        }

        public async Task<IActionResult> Values()
        {
            ValuesModel model = new ValuesModel();
            using (HttpClient client = new HttpClient())
            {
                var authToken = Request.Cookies["AuthToken"];
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + authToken);
                var data = await client.GetAsync(new Uri("https://localhost:44310/api/values"));
                var jsonResponse = await data.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(jsonResponse))
                {
                    IEnumerable<string> response = JsonConvert.DeserializeObject<IEnumerable<string>>(jsonResponse);
                    foreach(var item in response)
                    {
                        model.Values.Add(item);
                    }
                }                    
            }           
            return View(model);
        }
    }
}
