﻿
namespace LMGEDIApp.Domain.Models.User
{
    public class UserSearchResult
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
    }
}
