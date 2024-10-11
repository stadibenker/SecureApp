using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
	public class UserProfileVM
	{
		[Required]
		public string Email { get; set; }

		[Required]
		public string Department { get; set; }

		[Required]
		public string Position { get; set; }
	}
}
