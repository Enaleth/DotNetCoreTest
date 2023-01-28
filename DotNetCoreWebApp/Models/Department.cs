using System.ComponentModel.DataAnnotations;

namespace DotNetCoreWebApp.Models
{
	public class Department
	{
		public int DepartmentId { get; set; }
		[Required (ErrorMessage = "Please fill department name")]
		public string DepartmentName { get; set; }
	}
}