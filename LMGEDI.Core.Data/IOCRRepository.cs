using System.Collections.Generic;
using Core.Base.Data;
using LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.Data
{
    public interface IOCRRepository : IBaseRepository<OCR>
    {
        IEnumerable<OCRFileInvoices> GetOCRFilesInvoiceRecords(string SearchText, int skip, int take);
        int GetOCRFilesInvoiceRecordsCount(string SearchText);
        IEnumerable<OCRPaymentDetails> GetOCRPaymentFilesRecords(string SearchText, int skip, int take);
        int GetOCRPaymentFilesRecordsCount(string SearchText);
        OCRPayment AddOCRPaymentRecords(OCRPaymentSave oCRPaymentModel);
    }
}
