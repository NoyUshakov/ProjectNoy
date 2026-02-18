using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectNoy.Pages
{
    public class logoutModel : PageModel
    {
            public IActionResult OnGet()
        {
            // Remove a specific session key
            //HttpContext.Session.Remove("Logout");
            // Clear the entire session
            HttpContext.Session.Clear();

            return RedirectToPage("/Index");
        }
    }
    }

