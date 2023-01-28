using DotNetCoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Controllers
{
    public class PersonController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("https://localhost:44391/api/persons");
            
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();

                List<PersonAll> list = JsonConvert.DeserializeObject<List<PersonAll>>(jstring);
                
                return View(list);
            }

            return View(new List<PersonAll>());
        }
        public IActionResult Add()
        {
            return View();
        }
    }
}