using DotNetCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            var list = db.Positions.ToList();
            return Ok(list);
        }

        [HttpGet("{Id}")]
        public IActionResult Get (int id)
        {
            var db = new ApiDbContext();

            Position position = db.Positions.Find(id);

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