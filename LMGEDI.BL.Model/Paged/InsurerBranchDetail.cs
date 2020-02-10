using LMGEDI.BL.Model.Base;
using System.Collections.Generic;

namespace LMGEDI.Core.BL.Model.Paged
{
    public class InsurerBranchDetail : BasePaged
    {
        public IEnumerable<InsurerBranchSearch> InsurerBranchDetails { get; set; }
    }
}
