using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ProjectNoy.Pages.Users
{
    public class LoginModel : PageModel
    {
        public string msg { get; set; } = "";
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (Request.Form["Username"] == "Gilad" && Request.Form["password"] == "1968" ||
                Request.Form["Username"] == "Yoav" && Request.Form["password"] == "1234")
            {
                HttpContext.Session.SetString("Login", Request.Form["Username"]);
                HttpContext.Session.SetString("Admin", "True");
                return RedirectToPage("/Index");
            }
            msg = "Wrong username or password.";
            return Page();
        }
    }
}