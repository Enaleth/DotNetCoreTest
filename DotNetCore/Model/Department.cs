using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetCore.Model
{
    public class Department
    {
        public Department()
        {
            Positions = new HashSet<Position>();
        }
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
    }
}