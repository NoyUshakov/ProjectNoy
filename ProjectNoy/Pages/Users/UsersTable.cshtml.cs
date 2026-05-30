using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectNoy.Model;
using System.Data;

namespace ProjectNoy.Pages.Users
{
    public class UsersTableModel : PageModel
    {
        [BindProperty]
        public string filter { get; set; }

        [BindProperty]
        public string column { get; set; }

        [BindProperty]
        public string Order { get; set; }

        // ✨ תוקן: שונה מ-Id ל-UserId כדי להתאים לקוד המחיקה ול-SQL החדש
        [BindProperty]
        public int? UserId { get; set; }

        public DataTable dt { get; set; }

        public IActionResult OnGet()
        {
            // אם תרצה להחזיר את חסימת ה-Admin בעתיד, פשוט תוריד את ה-//
            // if (HttpContext.Session.GetString("Admin") != "True")
            // {
            //     return RedirectToPage("/AccessDenied");
            // }

            LoadTable();
            return Page();
        }

        // 🔍 מימוש מנגנון הסינון
        public void OnPostFilter()
        {
            Helper helper = new Helper();
            // השאילתה מחפשת את הטקסט ב-Username, בשם הפרטי, בשם המשפחה או באימייל
            string SQL = $"SELECT * FROM Users WHERE Username LIKE '%{filter}%' OR Firstname LIKE '%{filter}%' OR Lastname LIKE '%{filter}%' OR Email LIKE '%{filter}%'";
            dt = helper.RetrieveTable(SQL, "Users");
        }

        // ↕️ מימוש מנגנון המיון הדינמי
        public void OnPostSort()
        {
            Helper helper = new Helper();
            // המיון מתבצע לפי העמודה והסדר (עולה/יורד) שנבחרו בטופס
            string SQL = $"SELECT * FROM Users ORDER BY {column} {Order}";
            dt = helper.RetrieveTable(SQL, "Users");
        }

        // ❌ מימוש מנגנון המחיקה
        public IActionResult OnPostDelete()
        {
            if (UserId.HasValue)
            {
                Helper helper = new Helper();
                helper.Delete(UserId.Value, "Users");
            }

            // טעינה מחדש של הטבלה המעודכנת לאחר המחיקה
            LoadTable();
            return Page();
        }

        // פונקציית עזר פנימית כדי למנוע שכפול קוד בין ה-Get ל-Delete
        private void LoadTable()
        {
            Helper helper = new Helper();
            string SQL = "SELECT * FROM Users";
            dt = helper.RetrieveTable(SQL, "Users");
        }
    }
}