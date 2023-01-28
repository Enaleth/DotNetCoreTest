using System.ComponentModel.DataAnnotations;

namespace DotNetCoreWebApp.Models
{
	public class Salary
	{
		[Key]
		public int SalaryId { get; set; }
		public int Amount { get; set; }
	}
}