using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
	public class Person
	{	
		public Guid ID2 { get; set; }

		public string Name { get; set; }

		public DateTime DateOfBirth { get; set; }
	}
}