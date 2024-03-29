﻿using System.ComponentModel.DataAnnotations;

namespace DotNetCoreWebApp.Models
{
	public class Department
	{
		[Key]
		public int DepartmentId { get; set; }
		[Required (ErrorMessage = "Please fill department name")]
		public string DepartmentName { get; set; }
	}
}