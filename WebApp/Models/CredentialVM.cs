using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
	public class CredentialVM
	{
		[Required]
		public string Email { get; set; }
		
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Remember Me")]
		public bool RememberMe { get; set; }
	}
}
