using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecureApp.Pages
{
    [Authorize(Policy = "HrOnly")]
    public class HumanResourceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
