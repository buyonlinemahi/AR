using DLModel = LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;

namespace LMGEDI.Core.BL
{
    public interface IInvoiceNote
    {
        int AddInvoiceNoteRecord(DLModel.InvoiceNote modelInvoiceNote);
        int UpdateInvoiceNoteRecord(DLModel.InvoiceNote modelInvoiceNote);
        BLModel.Paged.InvoiceNoteDetail GetInvoiceNoteRecordByInvoiceID(int InvoiceID, int Skip, int Take);
        DLModel.InvoiceNote GetInvoiceNoteDetailsById(int InvoiceNoteId);
    }
}
