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
        public int? Id { get; set; }

        public DataTable dt { get; set; }

        public IActionResult OnGet()
        {
           // if (HttpContext.Session.GetString("Admin") != "True")
          //  {
         //       return RedirectToPage("/AccessDenied");
           // }

            Helper helper = new Helper();
            string SQL = "SELECT * FROM Users";
            dt = helper.RetrieveTable(SQL, "Users");
            return Page();
        }

        public void OnPostFilter()
        {
            // Implement filtering logic using 'filter'
        }

        public void OnPostSort()
        {
            // Implement sorting logic using 'column' and 'Order'
        }
        public IActionResult OnPostDelete()
        {
            if (Id.HasValue)
            {
                Helper helper = new Helper();
                helper.Delete(Id.Value, "Users");
                string SQL = "SELECT * FROM Users";
                dt = helper.RetrieveTable(SQL, "Users");
            }
            return Page();
        }
    }
}
