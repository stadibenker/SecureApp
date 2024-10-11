using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebApp.Data.Account;
using WebApp.Models;

namespace WebApp.Pages.Account
{
    public class UserProfileModel : PageModel
    {
		private readonly UserManager<User> _userManager;

		[BindProperty]
		public UserProfileVM UserProfileVM { get; set; }

		[BindProperty]
		public string? Message { get; set; }

		public UserProfileModel(UserManager<User> userManager)
		{
			_userManager = userManager;
			UserProfileVM = new UserProfileVM();
		}

		public async Task<IActionResult> OnGetAsync()
        {
			var (user, departmentClaim, positionClaim) = await GetUserInfoAsync();

			if (user != null)
			{
				UserProfileVM.Email = User?.Identity?.Name ?? string.Empty;
				UserProfileVM.Department = departmentClaim?.Value ?? string.Empty;
				UserProfileVM.Position = positionClaim?.Value ?? string.Empty;
			}

			return Page();
        }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var (user, departmentClaim, positionClaim) = await GetUserInfoAsync();

			if (user != null && departmentClaim != null) 
			{
				if (departmentClaim != null)
				{
					await _userManager.ReplaceClaimAsync(user, departmentClaim, new Claim(departmentClaim.Type, UserProfileVM.Department));
				}

				if (positionClaim != null)
				{
					await _userManager.ReplaceClaimAsync(user, positionClaim, new Claim(positionClaim.Type, UserProfileVM.Position));
				}
			}

			Message = "User have been successfully updated";

			return Page();
		}

		private async Task<(User? user, Claim? departmentClaim, Claim? position)> GetUserInfoAsync()
		{
			var user = await _userManager.FindByNameAsync(User?.Identity?.Name ?? string.Empty);

			if (user != null)
			{
				var claims = await _userManager.GetClaimsAsync(user);
				var departmentClaim = claims.FirstOrDefault(x => x.Type == "Department");
				var positionClaim = claims.FirstOrDefault(x => x.Type == "Position");

				UserProfileVM.Email = User?.Identity?.Name ?? string.Empty;
				UserProfileVM.Department = departmentClaim?.Value ?? string.Empty;
				UserProfileVM.Position = positionClaim?.Value ?? string.Empty;

				return (user, departmentClaim, positionClaim);
			}

			return (null, null, null);
		}
    }
}
