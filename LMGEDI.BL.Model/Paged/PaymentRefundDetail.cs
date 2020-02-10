using LMGEDI.BL.Model.Base;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;


namespace LMGEDI.Core.BL.Model.Paged
{
    public class PaymentRefundDetail : BasePaged
    {
        public IEnumerable<PaymentRefund> PaymentRefundDetails { get; set; }
    }
}
