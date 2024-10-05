using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
	public class RegisterVM
	{
		[Required]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
