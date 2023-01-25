using DotNetCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var db = new ApiDbContext();

            db.Persons.Add(person);

            db.SaveChanges();

            return Created("", person);
        }

        [HttpPut]
        public IActionResult UpdatePerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var db = new ApiDbContext();

            Person updatePerson = db.Persons.Find(person.Id);

            updatePerson.Adress = person.Adress;
            updatePerson.Age = person.Age;
            updatePerson.Email = person.Email;
            updatePerson.Name = person.Name;
            updatePerson.Password = person.Password;
            updatePerson.PositionId = person.PositionId;
            updatePerson.SalaryId = person.SalaryId;
            updatePerson.Surname = person.Surname;

            db.SaveChanges();

            return NoContent();
        }

        public IActionResult Upload()
        {
            return View();
        }
        //[HttpPost]
    }
}