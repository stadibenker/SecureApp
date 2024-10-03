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

			if (ValidateCredentials(Credential))
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, "admin"),
					new Claim(ClaimTypes.Email, "t@g.c"),
					new Claim("Admin", "true"),
					new Claim("Department", "HR"),
					new Claim("StartedDate", "2024-06-01"),
				};

				var identity = new ClaimsIdentity(claims, "CookieAuth");
				ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

				var authProperties = new AuthenticationProperties
				{
					IsPersistent = Credential.RememberMe
				};

				await HttpContext.SignInAsync("CookieAuth", claimsPrincipal, authProperties);

				return RedirectToPage("/Index");
			}

			return Page();
		}

		private bool ValidateCredentials(Credential credential)
		{
			return credential.UserName == "admin" && credential.Password == "password";
		}
	}
}
