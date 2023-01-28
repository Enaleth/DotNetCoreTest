using DotNetCoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Controllers
{
    public class PersonController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("http://localhost:51336/api/persons");

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
            Person person = new Person();
            return View(person);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();

                var jsonPerson = JsonConvert.SerializeObject(person);
                StringContent content = new StringContent(jsonPerson, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PostAsync("http://localhost:51336/api/persons", content);

                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "There is an API error");
                return View(person);
            }

            return View(person);
        }
    }
}