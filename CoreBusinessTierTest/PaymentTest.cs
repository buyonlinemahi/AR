using LMGEDI.Core.BL.Implementation;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class PaymentTest
    {
        private IPaymentRepository _IIPaymentRepository;
        private IPayment _IPaymentImplBL;
        private LMGEDI.Core.Data.Model.Payment DLModel = new LMGEDI.Core.Data.Model.Payment();

        private IInvoiceRepository _IInvoiceRepository;
        private IInvoice _IInvoiceImplBL;
        private LMGEDI.Core.Data.Model.Invoice DLModelIns = new LMGEDI.Core.Data.Model.Invoice();

        private LMGEDI.Core.Data.Model.InvoiceAdjustment _InvAdjModel = new LMGEDI.Core.Data.Model.InvoiceAdjustment();
        private IInvoiceAdjustmentRepository _IInvoiceAdjustmentRepository;

        [TestInitialize]
        public void InvoiceInitializer()
        {
            _IInvoiceAdjustmentRepository = new InvoiceAdjustmentRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _IInvoiceRepository = new InvoiceRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _IInvoiceImplBL = new InvoiceImpl(_IInvoiceRepository, _IInvoiceAdjustmentRepository);

            _IIPaymentRepository = new PaymentRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _IPaymentImplBL = new PaymentImpl(_IIPaymentRepository, _IInvoiceRepository, _IInvoiceAdjustmentRepository);
            
        }
        [TestMethod]
        public void Add_PaymentRecord()
        {
            DLModel.InvoiceId = 224;
            DLModel.PaymentAmount = 549;
            DLModel.PaymentReceived = System.DateTime.Now;
            DLModel.CheckNumber = "856974";
            DLModel.CheckUploadName = "check.pdf";
            int paymentId = _IPaymentImplBL.AddPaymentRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }
        [TestMethod]
        public void Update_PaymentRecord()
        {
            DLModel.PaymentId = 743;
            DLModel.InvoiceId = 224;
            DLModel.PaymentAmount = 200;
            DLModel.PaymentReceived = System.DateTime.Now;
            DLModel.CheckNumber = "856974";
            DLModel.CheckUploadName = "check21.pdf";
            int paymentId = _IPaymentImplBL.UpdatePaymentRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }
        [TestMethod]
        public void Get_PaymentRecordByInvoiceId()
        {
            var getAllPayments = _IPaymentImplBL.GetPaymentRecordByInvoiceId(2, 0, 10);
            Assert.IsTrue(getAllPayments != null, "Unable to find");
        }
    }
}
