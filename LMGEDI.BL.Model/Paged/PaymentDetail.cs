using LMGEDI.BL.Model.Base;
using System.Collections.Generic;

namespace LMGEDI.Core.BL.Model.Paged
{
    public class PaymentDetail : BasePaged
    {
        public IEnumerable<LMGEDI.Core.Data.Model.Payment> PaymentDetails { get; set; }
    }
}
