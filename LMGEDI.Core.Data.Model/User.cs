using System;

namespace LMGEDI.Core.Data.Model
{
  public   class User
    {
        public int UserID { get; set; }
        public string  Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public  DateTime?  LastLoginDate { get; set; }
        public bool IsActive { get; set; }

    }
}
