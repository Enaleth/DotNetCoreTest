using System.ComponentModel.DataAnnotations;

namespace DotNetCoreWebApp.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }
        [Required(ErrorMessage = "Please fill position name")]
        public string Name { get; set; }
    }
}