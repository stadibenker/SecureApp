using System.ComponentModel.DataAnnotations;

namespace SecureApp.Models.Account
{
    public class Credential
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;

		[Required]
        [DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;

		[Display(Name = "Remember me")]
		public bool RememberMe { get; set; }
	}
}
