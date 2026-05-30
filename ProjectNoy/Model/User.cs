using System;

namespace ProjectNoy.Model
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; } // F גדולה ו-N גדולה
        public string LastName { get; set; }  // L גדולה ו-N גדולה
        public string Email { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Birthday { get; set; } // נשאיר כ-string כדי להתאים ל-dr["Birthday"].ToString() שלך
        public bool Admin { get; set; }
    }
}