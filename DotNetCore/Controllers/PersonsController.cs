using DotNetCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var db = new ApiDbContext();

            var list = db.Persons.Include(x => x.Salary).Include(x => x.Position).ThenInclude(x => x.Department)
                    .Select(x => new PersonAll()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        PositionName = x.Position.Name,
                        Salary = x.Salary.Amount,
                        DepartmentName = x.Position.Department.DepartmentName
                    }).ToList();
            return Ok(list);
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var db = new ApiDbContext();

            Person person = db.Persons.FirstOrDefault(x => x.Id == Id);
            
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }
    }
}