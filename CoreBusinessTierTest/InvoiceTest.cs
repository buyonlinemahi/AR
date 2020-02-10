using LMGEDI.Core.BL.Implementation;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class InvoiceTest
    {
        private IInvoiceRepository _IInvoiceRepository;
        private IInvoice _IInvoiceImplBL;
        private LMGEDI.Core.Data.Model.Invoice DLModel = new LMGEDI.Core.Data.Model.Invoice();
        private LMGEDI.Core.Data.Model.InvoiceAdjustment _InvAdjModel = new LMGEDI.Core.Data.Model.InvoiceAdjustment();
        private IInvoiceAdjustmentRepository _IInvoiceAdjustmentRepository;
        [TestInitialize]
        public void InvoiceInitializer()
        {
            _IInvoiceAdjustmentRepository = new InvoiceAdjustmentRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _IInvoiceRepository = new InvoiceRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _IInvoiceImplBL = new InvoiceImpl(_IInvoiceRepository,_IInvoiceAdjustmentRepository);
        }
        [TestMethod]
        public void AddInvoice()
        {

            DLModel.InvoiceNumber = "24324235675476";
            DLModel.FileID = 1;
            DLModel.InvoiceAmt = 10;
            DLModel.InvoiceDate = System.DateTime.Now;
            DLModel.InvoiceSent = System.DateTime.Now;
            DLModel.BillingWeek = System.DateTime.Now;
            DLModel.DepartmentId = 1;
           // DLModel.InvoiceBalance = null;
            int patientid = _IInvoiceImplBL.AddInvoiceRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }
        [TestMethod]
        public void UpdateInvoice()
        {
            //DLModel.InvoiceID = 38;
            //DLModel.FileID = 1;
            //DLModel.InvoiceNumber = "2432423-1";
            //DLModel.InvoiceAmt = 1001;
            //DLModel.InvoiceDate = System.DateTime.Now;
            //DLModel.InvoiceSent = System.DateTime.Now;
            //DLModel.BillingWeek = System.DateTime.Now;
            //DLModel.DepartmentId = 1;
            //DLModel.InvoiceBalanceAmt = 373;
            //DLModel = _IInvoiceImplBL.GetInvoiceDetailsById(38);
            //DLModel.InvoiceBalanceAmt = 370;
            //    int patientid = _IInvoiceImplBL.UpdateInvoiceRecord(DLModel);

            DLModel.InvoiceID = 143;
            DLModel.InvoiceBalanceAmt = 370;
            _IInvoiceRepository.Update(DLModel,hp=>hp.InvoiceBalanceAmt);



            Assert.IsTrue(true, "Unable to find");
        }

        [TestMethod]
        public void Get_InvoiceRecordByFileId()
        {
            var getAllInvoices = _IInvoiceImplBL.GetInvoiceRecordByFileId(23, 0, 10);
            Assert.IsTrue(getAllInvoices != null, "Unable to find");
        }


        [TestMethod]
        public void Get_InvoiceDetailsById()
        {
            var getAllInvoices = _IInvoiceImplBL.GetInvoiceDetailsById(19);
            Assert.IsTrue(getAllInvoices != null, "Unable to find");
        }
        [TestMethod]
        public void AddInvoiceAdjustment()
        {
            _InvAdjModel.InvoiceID = 224;
            _InvAdjModel.AdjustmentAmt = 301;
            _InvAdjModel.AdjustmentNotes = "sdadsa dsadsa dsa dsa  dsad sa  dsadadadad";
            _InvAdjModel.CreatedBy = 1;
            _InvAdjModel.CreatedOn = System.DateTime.Now;
            // DLModel.InvoiceBalance = null;
            int patientid = _IInvoiceImplBL.AddInvoiceAdjustment(_InvAdjModel);
            Assert.IsTrue(true, "Unable to find");
        }

        [TestMethod]
        public void Get_InvoiceICDetailsById()
        {
            var getAllInvoices = _IInvoiceImplBL.GetInvoiceICRecord(2);
            Assert.IsTrue(getAllInvoices != null, "Unable to find");
        }
    }
}

