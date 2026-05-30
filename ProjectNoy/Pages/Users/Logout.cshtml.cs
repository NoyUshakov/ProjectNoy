using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectNoy.Pages.Users
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // 1. מנקים את כל ה-Session לחלוטין (מוחק גם את Login וגם את Admin)
            HttpContext.Session.Clear();

            // 2. מעבירים את המשתמש באופן מיידי חזרה לדף הבית
            return RedirectToPage("/Index");
        }
    }
}