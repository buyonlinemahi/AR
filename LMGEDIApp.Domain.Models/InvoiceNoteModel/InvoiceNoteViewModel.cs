using System.Collections.Generic;

namespace LMGEDIApp.Domain.Models.InvoiceNoteModel
{
    public class InvoiceNoteViewModel
    {
        public IEnumerable<InvoiceNoteDetail> InvoiceNoteDetails { get; set; }
        public int TotalCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
