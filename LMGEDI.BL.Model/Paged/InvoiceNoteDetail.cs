using LMGEDI.BL.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDI.Core.BL.Model.Paged
{
    public class InvoiceNoteDetail : BasePaged
    {
        public IEnumerable<LMGEDI.Core.Data.Model.InvoiceNoteDetail> InvoiceNoteDetails { get; set; }
    }
}
