
namespace LMGEDIApp.Infrastructure.ApplicationServices.Contracts
{
    public interface IStorageServices
    {
        bool CreateExcelUploadFolder(string storagePath, string ExcelFolder);
        bool CreatePaymentCheckFolder(int FileID, int InvoiceID, int PaymentID, string storagePath);
        bool CreateOCRUploadFolder(string storagePath, string OCRUpload);
        bool CreatePaymentRefundCheckFolder(int FileID, int InvoiceID, int PaymentRefundID, string storagePath);
        string CreateInvoiceICFolder(int InvoiceID, string storagePath);
    }
}
