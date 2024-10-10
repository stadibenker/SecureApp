using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Data.Account;
using WebApp.Models;

namespace WebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
		private readonly SignInManager<User> _signInManager;

		[BindProperty]
        public CredentialVM Credential { get; set; } = new CredentialVM();

		public LoginModel(SignInManager<User> signInManager)
		{
			_signInManager = signInManager;
		}

		public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _signInManager.PasswordSignInAsync(Credential.Email, Credential.Password, Credential.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("Login", "You are locked out.");
                }
                else
                {
                    ModelState.AddModelError("Login", "Failed to login.");
                }

                return Page();
            }
        }
    }
}
