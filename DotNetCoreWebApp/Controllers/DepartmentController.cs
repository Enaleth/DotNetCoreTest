using DotNetCoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Controllers
{
    public class DepartmentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Department> list = new List<Department>();
            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:51336/api/departments");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jstring = await responseMessage.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<Department>>(jstring);
                return View(list);
            }

            return View(list);
        }
        public IActionResult Add()
        {
            Department department = new Department();
            return View(department);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Department department)
        {
            if(ModelState.IsValid)
            {
                HttpClient client= new HttpClient();
                var jsonDepartment = JsonConvert.SerializeObject(department);
                StringContent content = new StringContent(jsonDepartment, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PostAsync("http://localhost:51336/api/departments", content);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("Error", "There is an error");
                return View(department);
            }

            return View(department);
        }
    }
}
