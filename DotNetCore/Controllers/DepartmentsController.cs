using DotNetCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        [HttpGet] 
        public IActionResult Get()
        {
            var db = new ApiDbContext();

            var list = db.Departments.ToList();
            return Ok(list);
        }
    }
}