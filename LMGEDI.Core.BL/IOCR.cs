using System.Collections.Generic;
using DLModel = LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;

namespace LMGEDI.Core.BL
{
    public interface IOCR
    {
        int AddOCRRecord(DLModel.OCR  modelOCR);
        BLModel.Paged.OCRDetail GetAllOCRRecords(string Filename , int Skip, int Take);
        IEnumerable<DLModel.OCRFileInvoices> GetOCRFilesInvoiceRecords(string SearchText, int skip, int take);
        int GetOCRFilesInvoiceRecordsCount(string SearchText);
        int DeleteOCRFileDetailsById(int OCRId, string Filename);
        IEnumerable<DLModel.OCRPaymentDetails> GetOCRPaymentFilesRecords(string SearchText, int skip, int take);
        int GetOCRPaymentFilesRecordsCount(string SearchText);
        DLModel.OCRPayment AddOCRPaymentRecords(DLModel.OCRPaymentSave oCRPaymentModel);
        int CheckOCRPaymentDetailsByInvoiceId(DLModel.OCRPaymentDetails oCRPaymentDetails);
        DLModel.OCR GetOCRDetailById(int OCRId);
        int UpdateOCRRecord(DLModel.OCR modelOCR);
    }
}
