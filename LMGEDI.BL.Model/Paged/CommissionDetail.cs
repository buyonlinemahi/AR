using LMGEDI.BL.Model;
using LMGEDI.BL.Model.Base;
using System.Collections.Generic;

namespace LMGEDI.Core.BL.Model.Paged
{
    public class CommissionDetail : BasePaged
    {
        public IEnumerable<CommissionSearch> CommissionDetails { get; set; }        
    }
}
