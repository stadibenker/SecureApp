using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecureApp.Models.Account;
using System.Security.Claims;

namespace SecureApp.Pages.Account
{
	public partial class LoginModel : PageModel
	{
		[BindProperty]
		public Credential Credential { get; set; } = new Credential();

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
				return Page();

			if (Credential.UserName == "admin" && Credential.Password == "password")
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, "admin"),
					new Claim(ClaimTypes.Email, "t@g.c")
				};

				var identity = new ClaimsIdentity(claims, "CookieAuth");
				ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

				await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

				return RedirectToPage("/Index");
			}

			return Page();
		}
	}
}
