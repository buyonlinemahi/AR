using LMGEDI.BL.Model.Base;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;

namespace LMGEDI.BL.Model.Paged
{
    public class UserDetail : BasePaged
    {
        public IEnumerable<User> UserDetails { get; set; }
    }
}
