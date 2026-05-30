using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // לצורך שימוש ב-Session
using System.Data;
using ProjectNoy.Model; // ה-using שמחבר ל-Helper ול-User

namespace ProjectNoy.Pages.Users
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // 1. יצירת מופע של מחלקת העזר שלך
            Helper helper = new Helper();

            // 2. בניית השאילתה לבדיקת פרטי המשתמש
            string sql = $"SELECT * FROM Users WHERE Username = '{Username}' AND Password = '{Password}'";

            // 3. קריאה לפונקציה המדויקת מה-Helper שלך (RetrieveTable)
            // שלחנו את השאילתה ואת שם הטבלה "Users" כפי שהפונקציה דורשת
            DataTable dt = helper.RetrieveTable(sql, "Users");

            // 4. בדיקה האם חזר משתמש תואם מהחלון
            if (dt != null && dt.Rows.Count > 0)
            {
                // המשתמש נמצא! נשמור את הפרטים שלו ב-Session כדי שהאתר יזהה אותו
                HttpContext.Session.SetString("Username", dt.Rows[0]["Username"].ToString());

                // שמירת סטטוס הניהול (Admin) שלו
                HttpContext.Session.SetString("IsAdmin", dt.Rows[0]["Admin"].ToString());

                // העברה חלקה לדף הבית של האתר
                return RedirectToPage("/Index");
            }
            else
            {
                // אם הטבלה ריקה - הפרטים שגויים
                ErrorMessage = "שם משתמש או סיסמה שגויים. נסה שנית.";
                return Page();
            }
        }
    }
}