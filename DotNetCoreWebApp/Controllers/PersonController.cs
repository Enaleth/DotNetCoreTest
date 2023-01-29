using DotNetCoreWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<IActionResult> Update(int Id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("http://localhost:51336/api/persons/" + Id);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                Person person = JsonConvert.DeserializeObject<Person>(jstring);
                return View(person);
            }
            return RedirectToAction("Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Person person)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var jsonperson = JsonConvert.SerializeObject(person);
                StringContent content = new StringContent(jsonperson, Encoding.UTF8, "application/json");

                HttpResponseMessage message = await client.PutAsync("http://localhost:51336/api/persons", content);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View(person);
            }
            return View(person);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.DeleteAsync("http://localhost:51336/api/persons/" + Id);
            if (message.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(/*[FromForm]*/IFormFile file)
        {
            HttpClient client = new HttpClient();
            using (Stream stream = file.OpenReadStream())
            {

                MemoryStream memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                var bytes = memoryStream.ToArray();

                ByteArrayContent content = new ByteArrayContent(bytes);
                MultipartFormDataContent data = new MultipartFormDataContent();
                data.Add(content, "File", file.FileName);
                HttpResponseMessage message = await client.PostAsync("http://localhost:51336/api/persons/uploadfile", data);

                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View();
            }
        }
    }
}