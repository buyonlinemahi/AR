using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.InvoiceNoteModel
{
    public class InvoiceNote
    {
        public int InvoiceNotesID { get; set; }
        public int InvoiceID { get; set; }
        public string InvoiceNotes { get; set; }
        public int UserID { get; set; }
    }
    public class InvoiceNoteDetail
    {
        public int InvoiceNotesID { get; set; }
        public int InvoiceID { get; set; }
        public string InvoiceNotes { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}
