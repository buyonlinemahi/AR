using LMGEDI.BL.Model.Base;
using System.Collections.Generic;

namespace LMGEDI.Core.BL.Model.Paged
{
    public class InvoiceDetail : BasePaged
    {
        public IEnumerable<LMGEDI.Core.Data.Model.InvoiceDetail> InvoiceDetails { get; set; }
    }
}
