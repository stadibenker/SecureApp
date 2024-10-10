using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebApp.Data.Account;
using WebApp.Models;

namespace WebApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
		private readonly UserManager<User> _userManager;

		[BindProperty]
		public RegisterVM RegisterVM { get; set; } = new RegisterVM();

		public RegisterModel(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = new User
			{
                Email = RegisterVM.Email,
                UserName = RegisterVM.Email
            };

            var claims = new List<Claim>
            {
                new Claim("Department", RegisterVM.Department),
                new Claim("Position", RegisterVM.Position)
            };

            var result = await _userManager.CreateAsync(user, RegisterVM.Password);

            if (result.Succeeded) 
            {
                await _userManager.AddClaimsAsync(user, claims);
                
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                return RedirectToPage("/Account/ConfirmEmail", new { userId = user.Id, token });
            }
            else
            {
				foreach (var error in result.Errors)
				{
                    ModelState.AddModelError("Register", error.Description);
				}

                return Page();
			}
        }
    }
}
