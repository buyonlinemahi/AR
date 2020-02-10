using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDI.Core.Data.Model
{
    public class InvoiceAdjustment
    {
        public int AdjustmentID { get; set; }
        public int InvoiceID { get; set; }
        public decimal AdjustmentAmt { get; set; }
        public string AdjustmentNotes { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
