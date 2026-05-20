using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectNoy.Model;
using System.Data;


namespace ProjectNoy.Pages
{
    public class loginModel : PageModel
    {
        public string msg { get; set; } = string.Empty;
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            string SQLStr = $"SELECT * FROM Users WHERE Username LIKE '{Username}' AND Password LIKE '{Password}'";
            Helper helper = new Helper();
            DataTable dt = helper.RetrieveTable(SQLStr, "Users");

            if (dt.Rows.Count > 0)
            {
                HttpContext.Session.SetString("Login", Username);
                HttpContext.Session.SetString("Admin", dt.Rows[0]["Admin"].ToString());
                return RedirectToPage("/Index");
            }
            msg = "Wrong username or password.";
            return Page();
        }
    }
}
