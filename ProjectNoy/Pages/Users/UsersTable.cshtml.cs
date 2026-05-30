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

        [BindProperty]
        public int? UserId { get; set; } // זהו המזהה למחיקה

        public DataTable dt { get; set; }

        public IActionResult OnGet()
        {
            if(HttpContext.Session.GetString("Admin") != "True")
            {
                return RedirectToPage("/AccessDenied");
            }
            LoadTable();
            return Page();
        }

        public void OnPostFilter()
        {
            Helper helper = new Helper();
            string SQL = $"SELECT * FROM Users WHERE Username LIKE '%{filter}%' OR FirstName LIKE '%{filter}%' OR LastName LIKE '%{filter}%' OR Email LIKE '%{filter}%'";
            dt = helper.RetrieveTable(SQL, "Users");
        }

        public void OnPostSort()
        {
            Helper helper = new Helper();
            string SQL = $"SELECT * FROM Users ORDER BY {column} {Order}";
            dt = helper.RetrieveTable(SQL, "Users");
        }

        public IActionResult OnPostDelete()
        {
            if (UserId.HasValue)
            {
                Helper helper = new Helper();
                helper.Delete(UserId.Value, "Users");
            }
            LoadTable();
            return Page();
        }

        private void LoadTable()
        {
            Helper helper = new Helper();
            string SQL = "SELECT * FROM Users";
            dt = helper.RetrieveTable(SQL, "Users");
        }
    }
}