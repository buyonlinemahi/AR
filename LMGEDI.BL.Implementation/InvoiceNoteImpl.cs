using System.Linq;
using LMGEDI.Core.Data;
using Omu.ValueInjecter;
using DLModel = LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;

namespace LMGEDI.Core.BL.Implementation
{
    public class InvoiceNoteImpl : IInvoiceNote
    {
        private readonly IInvoiceNoteRepository _invoiceNoteRepostory;
        public InvoiceNoteImpl(IInvoiceNoteRepository invoiceNoteRepostory)
        {
            _invoiceNoteRepostory = invoiceNoteRepostory;
        }
        public int AddInvoiceNoteRecord(DLModel.InvoiceNote modelInvoiceNote)
        {
            return _invoiceNoteRepostory.Add((DLModel.InvoiceNote)new DLModel.InvoiceNote().InjectFrom(modelInvoiceNote)).InvoiceNotesID;
        }

        public int UpdateInvoiceNoteRecord(DLModel.InvoiceNote modelInvoiceNote)
        {
            return _invoiceNoteRepostory.Update((DLModel.InvoiceNote)new DLModel.InvoiceNote().InjectFrom(modelInvoiceNote));
        }
        public BLModel.Paged.InvoiceNoteDetail GetInvoiceNoteRecordByInvoiceID(int InvoiceID, int Skip, int Take)
        {
            return new BLModel.Paged.InvoiceNoteDetail
            {
                InvoiceNoteDetails = _invoiceNoteRepostory.GetAllInvoiceNotes(InvoiceID, Skip, Take).ToList(),
                TotalCount = _invoiceNoteRepostory.GetAllInvoiceNotesByInvoiceID(invoiceNote => invoiceNote.InvoiceID.Equals(InvoiceID))
            };            
        }
        public DLModel.InvoiceNote GetInvoiceNoteDetailsById(int InvoiceNoteId)
        {
            return _invoiceNoteRepostory.GetById(InvoiceNoteId);
        }
    }
}
