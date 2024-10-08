using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Data.Account;

namespace WebApp.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
		private readonly UserManager<User> _userManager;

		[BindProperty]
		public string Message { get; set; } = string.Empty;

		public ConfirmEmailModel(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IActionResult> OnGetAsync (string userId, string token)
        {
			var user = await _userManager.FindByIdAsync(userId);

			if (user == null) {
				Message = "Failed to validate email.";
				return Page();
			}

			var result = await _userManager.ConfirmEmailAsync(user, token);
			if (result.Succeeded)
			{
				Message = "The email address has been confirmed.";
			}

			return Page();
		}
	}
}
