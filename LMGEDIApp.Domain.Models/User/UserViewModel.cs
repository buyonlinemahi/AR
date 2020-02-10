using System.Collections.Generic;

namespace LMGEDIApp.Domain.Models.User
{
    public class UserViewModel
    {
        public UserSearch userSearch { get; set; }
        public IEnumerable<UserSearchResult> UserSearchResult { get; set; }
        public int UserCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
