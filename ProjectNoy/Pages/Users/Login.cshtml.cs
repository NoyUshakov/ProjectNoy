using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // לצורך שימוש ב-Session
using System.Data;
using ProjectNoy.Model; // ה-using שמחבר ל-Helper ול-User

namespace ProjectNoy.Pages.Users
{
    public class LoginModel : PageModel
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

            // בתוך ה-OnPost של ה-Login
            if (dt != null && dt.Rows.Count > 0)
            {
                HttpContext.Session.SetString("Username", dt.Rows[0]["Username"].ToString());
                // חשוב: אנחנו נשמור את הערך כ-string קטן ("true" או "false")
                bool isAdmin = Convert.ToBoolean(dt.Rows[0]["Admin"]);
                HttpContext.Session.SetString("Admin", isAdmin.ToString());

                return RedirectToPage("/Index");
            }
            msg = "Wrong username or password.";
            return Page();
        }
    }
}