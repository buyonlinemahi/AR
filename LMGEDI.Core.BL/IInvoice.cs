using DLModel = LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;
using System.Collections.Generic;

namespace LMGEDI.Core.BL
{
    public interface IInvoice
    {
    
        int AddInvoiceRecord(DLModel.Invoice modelInvoice);
        int UpdateInvoiceRecord(DLModel.Invoice modelInvoice);
        BLModel.Paged.InvoiceDetail GetInvoiceRecordByFileId(int FileId, int Skip, int Take);
        //InvoiceIC
        IEnumerable<DLModel.InvoiceICDetail> GetInvoiceICRecord(int FileId);
        int UpdateInvoiceICAmt(IEnumerable<DLModel.InvoiceICDetail> objInvoiceICDetail);

        DLModel.Invoice GetInvoiceDetailsById(int InvoiceId);
        // Adjustment....
        int AddInvoiceAdjustment(DLModel.InvoiceAdjustment modelAdJustment);
        void UpdateAssignedToInvoiceIDByInvoiceID(int InvoiceID, int SubInvoiceID);
        IEnumerable<DLModel.InvoiceICDetail> GetAssignedInvoicedByInvoiceID(int InvoiceID);
    }
}
