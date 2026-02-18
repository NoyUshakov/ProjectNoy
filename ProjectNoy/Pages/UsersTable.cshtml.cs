using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ProjectNoy.Pages
{
    public class UsersTableModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Admin") != "True")
            {
                return RedirectToPage("/AccessDenied");
            }
            return Page();
        }
    }
    }
                                                
