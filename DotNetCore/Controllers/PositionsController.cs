using DotNetCore.Model;
using DotNetCoreBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var db = new ApiDbContext();

            var list = db.Positions
                .Include(x => x.Department)
                .Select(x => new PositionAll()
                    {
                        PositionId = x.PositionId,
                        Name = x.Name,
                        DepartmentName = x.Department.DepartmentName
                    }).ToList();

            return Ok(list);
        }

        [HttpGet("{Id}")]
        public IActionResult Get (int Id)
        {
            var db = new ApiDbContext();

            Position position = db.Positions.FirstOrDefault(x => x.PositionId == Id);

            if (position == null)
            {
                return NotFound();
            }

            return Ok(position);
        }

        [HttpPost]
        public IActionResult AddPosition (Position position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var db = new ApiDbContext();

            db.Positions.Add(position);

            db.SaveChanges();

            return Created("", position);
        }

        [HttpPut]
        public IActionResult UpdatePosition(Position position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var db = new ApiDbContext();

            Position updatePosition = db.Positions.Find(position.PositionId);

            updatePosition.Name = position.Name;
            updatePosition.DepartmentId = position.DepartmentId;

            db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public IActionResult DeletePosition(int id)
        {
            var db = new ApiDbContext();

            Position position = db.Positions.Find(id);

            if (position == null)
            {
                return NotFound();
            }

            db.Positions.Remove(position);

            db.SaveChanges();

            return NoContent();
        }
    }
}