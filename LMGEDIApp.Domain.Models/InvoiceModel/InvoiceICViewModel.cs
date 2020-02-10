using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.InvoiceModel
{
    public class InvoiceICViewModel
    {
        public IEnumerable<InvoiceICDetail> InvoiceICDetails { get; set; }
        public IEnumerable<InvoiceICDetail> InvoiceICWithoutDetails { get; set; }  
    }

    public class InvoiceICDetail
    {
        public int FileID { get; set; }
        public int InvoiceID { get; set; }
        public string FileFullName { get; set; }
        public string ClaimNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal InvoiceAmt { get; set; }
        public decimal? InvoiceICAmt { get; set; }
        public int? DepartmentId { get; set; }
        public bool IsChecked { get; set; }
    }
}
