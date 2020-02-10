using LMGEDI.Core.BL.Implementation;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class OCRTest
    {
        private IOCRRepository _IOCRRepository;

        
        private IOCR _IOCRImplBL;

        private IPaymentRepository _IPaymentRepository;
        private IInvoiceRepository _IInvoiceRepository;


        private LMGEDI.Core.Data.Model.OCR DLModel = new LMGEDI.Core.Data.Model.OCR();
        private LMGEDI.Core.Data.Model.OCRPaymentSave DLModel2 = new LMGEDI.Core.Data.Model.OCRPaymentSave();
        private LMGEDI.Core.Data.Model.OCRPaymentDetails DLModel3 = new LMGEDI.Core.Data.Model.OCRPaymentDetails();

        [TestInitialize]
        public void InvoiceInitializer()
        {
            _IOCRRepository = new OCRRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());

            _IPaymentRepository = new PaymentRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());

            _IInvoiceRepository = new InvoiceRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());


            _IOCRImplBL = new OCRImpl(_IOCRRepository, _IPaymentRepository, _IInvoiceRepository);
        }
        [TestMethod]
        public void Add_OCRRecord()
        {
            DLModel.OcrId = 1;
            DLModel.OcrFileName = "OCR1.pdf";            
            DLModel.CreatedBy = 1;
            DLModel.CreatedOn = System.DateTime.Now;
            DLModel.IsDeleted = null;
            DLModel.DeletedBy = null;
            DLModel.DeletedOn = null;
            int OcrId = _IOCRImplBL.AddOCRRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }
        [TestMethod]
        public void Get_AllOCRRecords()
        {
            var getAllOCRRecords = _IOCRImplBL.GetAllOCRRecords("%", 0, 10);
            Assert.IsTrue(getAllOCRRecords != null, "Unable to find");
        }

        [TestMethod]
        public void Get_OCRFilesInvoiceRecords()
        {
            var getAllOCRRecords = _IOCRImplBL.GetOCRFilesInvoiceRecords("%", 0, 10);
            Assert.IsTrue(getAllOCRRecords != null, "Unable to find");
        }
        [TestMethod]
        public void Get_OCRFilesInvoiceRecordsCount(string SearchText)
        {
            int Count = _IOCRImplBL.GetOCRFilesInvoiceRecordsCount("%");
            Assert.IsTrue(Count < 0, "Unable to find");
        }
        [TestMethod]
        public void Get_OCRPaymentFilesRecords()
        {
            var getAllOCRRecords = _IOCRImplBL.GetOCRPaymentFilesRecords("%", 0, 10);
            Assert.IsTrue(getAllOCRRecords != null, "Unable to find");
        }
        [TestMethod]
        public void Get_OCRPaymentFilesRecordsCount(string SearchText)
        {
            int Count = _IOCRImplBL.GetOCRPaymentFilesRecordsCount("%");
            Assert.IsTrue(Count < 0, "Unable to find");
        }
        [TestMethod]
        public void Add_OCRPaymentRecords()
        {
            DLModel2.CheckNumber = "1R2O3H4I5T";
            DLModel2.CheckUploadName = "CHECK132.pdf";
            DLModel2.CreatedBy = 2;
            DLModel2.FileID = 5;
            DLModel2.InvoiceID = 1;
            DLModel2.OCRId = 1;
            DLModel2.PaymentAmount = decimal.Parse("13");
            DLModel2.PaymentId = 1;
            DLModel2.PaymentReceived = System.DateTime.Now;
            DLModel2.CheckDate = System.DateTime.Now;
            var OCRPayment = _IOCRImplBL.AddOCRPaymentRecords(DLModel2);
            Assert.IsTrue(OCRPayment != null, "Unable to find");
        }
        [TestMethod]
        public void Check_OCRPaymentDetailsByInvoiceId()
        {
            DLModel3.InvoiceID = 2;
            DLModel3.PaymentAmount = decimal.Parse("1");
            int Count = _IOCRImplBL.CheckOCRPaymentDetailsByInvoiceId(DLModel3);
            Assert.IsTrue(Count < 0, "Unable to find");
        }
        
        [TestMethod]
        public void Get_OCRDetailById()
        {
            var getAllOCRRecords = _IOCRImplBL.GetOCRDetailById(1);
            Assert.IsTrue(getAllOCRRecords != null, "Unable to find");
        }

        
    }
}
