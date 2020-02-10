using LMGEDI.BL.Model;
using LMGEDI.BL.Model.Base;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;

namespace LMGEDI.Core.BL.Model.Paged
{
    public class CommissionPaymentDetail : BasePaged
    {
        public IEnumerable<CommissionPayment> CommissionPaymentDetails { get; set; }        
    }
}
