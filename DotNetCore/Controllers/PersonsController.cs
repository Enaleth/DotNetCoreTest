﻿using DotNetCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.IO;
using Microsoft.AspNetCore.Cors;

namespace DotNetCore.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var db = new ApiDbContext();

            var list = db.Persons
                .Include(x => x.Salary)
                .Include(x => x.Position)
                .ThenInclude(x => x.Department)
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

        [HttpDelete("{Id}")]
        public IActionResult DeletePerson(int id)
        {
            var db = new ApiDbContext();

            Person person = db.Persons.Find(id);

            if (person == null)
            {
                return NotFound();
            }
            db.Persons.Remove(person);

            db.SaveChanges();

            return NoContent();
        }

        [HttpPost("uploadfile")]
        public async Task<IActionResult> UploadFile([FromForm]IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/" + fileName);
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                return Created("", null);
            }
        }
    }
}