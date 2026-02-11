using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectNoy.Pages
{
    public class rapModel : PageModel
    
            {
    public IActionResult OnGet()
        {
            string login = HttpContext.Session.GetString("login");
            if (string.IsNullOrEmpty(login))
            {
                return RedirectToPage("/AccessDenied");
            }
            return Page();
        }
    }
}
