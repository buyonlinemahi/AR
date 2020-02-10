
namespace LMGEDI.Core.Data.Model
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
