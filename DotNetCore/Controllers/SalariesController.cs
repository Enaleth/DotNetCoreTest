using DotNetCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var db = new ApiDbContext();

            var list = db.Salaries.ToList();
            return Ok(list);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var db = new ApiDbContext();

            Salary salary = db.Salaries.Find(Id);

            if (salary == null)
            {
                return NotFound();
            }

            db.Salaries.Remove(salary);

            db.SaveChanges();

            return NoContent();
        }
    }
}
