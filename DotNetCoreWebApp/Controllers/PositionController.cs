using DotNetCoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Controllers
{
    public class PositionController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("http://localhost:51336/api/positions");

            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();

                List<PositionAll> list = JsonConvert.DeserializeObject<List<PositionAll>>(jstring);

                return View(list);
            }

            return View(new List<PositionAll>());
        }

        public IActionResult Add()
        {
            Position position = new Position();
            return View(position);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Position position)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var jsonDepartment = JsonConvert.SerializeObject(position);
                StringContent content = new StringContent(jsonDepartment, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PostAsync("http://localhost:51336/api/positions", content);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("Error", "There is an error");
                return View(position);
            }

            return View(position);
        }

        public async Task<IActionResult> Update(int Id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("http://localhost:51336/api/positions/" + Id);

            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                Position position = JsonConvert.DeserializeObject<Position>(jstring);
                return View(position);
            }

            return RedirectToAction("Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Position position)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var jsonposition = JsonConvert.SerializeObject(position);
                StringContent content = new StringContent(jsonposition, Encoding.UTF8, "application/json");

                HttpResponseMessage message = await client.PutAsync("http://localhost:51336/api/positions", content);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View(position);
            }

            return View(position);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage message = await client.DeleteAsync("http://localhost:51336/api/positions/" + Id);
            if (message.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
