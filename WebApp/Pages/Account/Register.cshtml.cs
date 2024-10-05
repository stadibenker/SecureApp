using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
		[BindProperty]
		public RegisterVM RegisterVM { get; set; } = new RegisterVM();

		public void OnGet()
        {
        }
    }
}
