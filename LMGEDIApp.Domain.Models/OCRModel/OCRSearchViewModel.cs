using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.OCRModel
{
    public class OCRInvoiceSearchViewModel
    {
        public IEnumerable<OCRFileInvoices> OCRFileSearchResult { get; set; }
        public int OCRFileCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }

    public class OCRFileSearchViewModel
    {
        public IEnumerable<OCRModel> OCRSearchResult { get; set; }
        public int OCRCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }

    public class OCRFileAssignedViewModel
    {
        public IEnumerable<OCRPaymentDetails> OCRPaymentDetailsResult { get; set; }
        public int OCRFileAssignedCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }  
}
