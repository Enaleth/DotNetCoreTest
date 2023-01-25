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
        [HttpGet("{Id}")]
        public IActionResult Get(int id)
        {
            var db = new ApiDbContext();

            Department department= db.Departments.Find(id);

            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var db = new ApiDbContext();

            db.Departments.Add(department);

            db.SaveChanges();

            return Created("", department);
        }

        [HttpPut]
        public IActionResult UpdateDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var db = new ApiDbContext();

            Department updateDepartment = db.Departments.Find(department.DepartmentId);
            updateDepartment.DepartmentName = department.DepartmentName;

            db.SaveChanges();

            return NoContent();
        }
    }
}