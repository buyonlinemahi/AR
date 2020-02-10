using LMGEDI.Core.BL;
using LMGEDI.Core.BL.Implementation;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class CommissionTest
    {
        private ICommissionRepository _commissionRepository;
        private ICommissionPaymentRepository _commissionPaymentRepository;
        private ICommission _commissionImplBL;

        private LMGEDI.Core.Data.Model.Commission DLModel = new LMGEDI.Core.Data.Model.Commission();
        private LMGEDI.Core.Data.Model.CommissionPayment DLModel2 = new LMGEDI.Core.Data.Model.CommissionPayment();

        [TestInitialize]
        public void CommissionInitializer()
        {
            _commissionRepository = new CommissionRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _commissionPaymentRepository = new CommissionPaymentRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _commissionImplBL = new CommissionImpl(_commissionRepository, _commissionPaymentRepository);
        }


        [TestMethod]
        public void Get_CommissionRecordByLienClientID()
        {
            var getCommissionDetails = _commissionImplBL.GetCommissionRecordByLienClientID(1, 0, 10);
            Assert.IsTrue(getCommissionDetails != null, "Unable to find");
        }

        [TestMethod]
        public void Get_AllLienClientBilling()
        {
            var _res = _commissionImplBL.GetAllLienClientBilling();
            Assert.IsTrue(_res != null, "Unable to find");
        }

        [TestMethod]
        public void Get_AllCommissionPaymentByCommissionID()
        {
            var _res = _commissionImplBL.GetAllCommissionPaymentByCommissionID(1, 1, 0, 10);
            Assert.IsTrue(_res != null, "Unable to find");
        }

        [TestMethod]
        public void Add_CommissionPaymentRecord()
        {
            DLModel2.CommissionID = 4;
            DLModel2.CPCheckNumber = "132213";
            DLModel2.CPCheckSentOn = System.DateTime.Now;
            DLModel2.CPPaymentPaidAmount = 25;
            int patientid = _commissionImplBL.AddCommissionPaymentRecord(DLModel2);
            Assert.IsTrue(true, "Unable to find");
        }

        [TestMethod]
        public void Update_CommissionPaymentRecord()
        {
            DLModel2.CommissionID = 1;
            DLModel2.CommissionPaymentID = 3;
            DLModel2.CPCheckNumber = "132213";
            DLModel2.CPCheckSentOn = System.DateTime.Now;
            DLModel2.CPPaymentPaidAmount = 25;
            int patientid = _commissionImplBL.UpdateCommissionPaymentRecord(DLModel2);
            Assert.IsTrue(true, "Unable to find");
        }

        [TestMethod]
        public void Update_CommissionRecord()
        {
            DLModel.CommissionID = 1;
            DLModel.IsPaymentRecevied = true;
            int patientid = _commissionImplBL.UpdateCommissionRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }

        [TestMethod]
        public void Get_CommissionDashboardRecord()
        {
            var getCommissionDetails = _commissionImplBL.GetCommissionDashboardRecord(0, 10);
            Assert.IsTrue(getCommissionDetails != null, "Unable to find");
        }
    }
}
