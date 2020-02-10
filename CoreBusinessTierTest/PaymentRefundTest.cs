using LMGEDI.Core.BL.Implementation;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class PaymentRefundTest
    {
        private IPaymentRefundRepository _IIPaymentRefundRepository;
        private IPaymentRefund _IPaymentRefundImplBL;
        private LMGEDI.Core.Data.Model.PaymentRefund DLModel = new LMGEDI.Core.Data.Model.PaymentRefund();

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

            _IIPaymentRefundRepository = new PaymentRefundRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _IPaymentRefundImplBL = new PaymentRefundImpl(_IIPaymentRefundRepository, _IInvoiceRepository, _IInvoiceAdjustmentRepository);

        }
        [TestMethod]
        public void Add_PaymentRefundRecord()
        {
            DLModel.InvoiceId = 92;
            DLModel.RefundAmount = 150;
            DLModel.RefundReceived = System.DateTime.Now;
            DLModel.CheckNumber = "8563974";
            DLModel.CheckUploadName = "check.pdf";
            int paymentId = _IPaymentRefundImplBL.AddPaymentRefundRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }
        [TestMethod]
        public void Update_PaymentRefundRecord()
        {
            DLModel.PaymentRefundID =1;
            DLModel.InvoiceId = 92;
            DLModel.RefundAmount = 200;
            DLModel.RefundReceived = System.DateTime.Now;
            DLModel.CheckNumber = "8000974";
            DLModel.CheckUploadName = "check21.pdf";
            int paymentId = _IPaymentRefundImplBL.UpdatePaymentRefundRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }
        [TestMethod]
        public void Get_PaymentRefundRecordByInvoiceId()
        {
            var getAllPayments = _IPaymentRefundImplBL.GetPaymentRefundRecordByInvoiceId(2, 0, 10);
            Assert.IsTrue(getAllPayments != null, "Unable to find");
        }

        [TestMethod]
        public void Get_GetNetworkingDays()
        {
            var getAllPayments = _IPaymentRefundImplBL.GetNetworkingDays("01/01/2016", 2);
            Assert.IsTrue(getAllPayments != null, "Unable to find");
        }
    }
}
