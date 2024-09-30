using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecureApp.Models.Account;

namespace SecureApp.Pages.Account
{
	public partial class LoginModel : PageModel
	{
		[BindProperty]
		public Credential Credential { get; set; } = new Credential();

		public void OnGet()
		{
		}

		public void OnPost() { }
	}
}
