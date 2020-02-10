using LMGEDIApp.Infrastructure.ApplicationServices.Contracts;
using System.IO;


namespace LMGEDIApp.Infrastructure.Services
{
    public class StorageServices : IStorageServices
    {
        public bool CreateExcelUploadFolder(string storagePath, string ExcelFolder)
        {
            string path = Path.Combine(storagePath, ExcelFolder);
            return CreateDirectory(path);

        }
        public bool CreatePaymentCheckFolder(int FileID, int InvoiceID, int PaymentID, string storagePath)
        {
            string path = Path.Combine(storagePath, FileID.ToString(), InvoiceID.ToString(), PaymentID.ToString());
            return CreateDirectory(path);
        }
        public bool CreatePaymentRefundCheckFolder(int FileID, int InvoiceID, int PaymentRefundID, string storagePath)
        {
            string path = Path.Combine(storagePath, FileID.ToString(), InvoiceID.ToString(), PaymentRefundID.ToString());
            return CreateDirectory(path);
        }
        public string CreateInvoiceICFolder(int InvoiceID, string storagePath)
        {
            string path = Path.Combine(storagePath, "Invoices", InvoiceID.ToString(), "IC");
            CreateDirectory(path);
            return path;
        }
        public bool CreateOCRUploadFolder(string storagePath, string OCRUpload)
        {
            string path = Path.Combine(storagePath, OCRUpload);
            return CreateDirectory(path);

        }
        private static bool CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return true;
            }
            return false;
        }
    }
}
