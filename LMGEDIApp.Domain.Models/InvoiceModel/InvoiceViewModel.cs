using LMGEDIApp.Domain.Models.InvoiceAdjustmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.InvoiceModel
{
    public class InvoiceViewModel
    {
        public IEnumerable<Invoice> InvoiceDetails { get; set; }
        public int InvoiceCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        
    }
}
