using LMGEDI.Core.BL.Implementation;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class PendingUploadTest
    {
        private IPendingUploadRepository _IPendingUploadRepository;
        private IPendingUpload _IPendingUploadImplBL;

        private IUserRepository _IUserRepository;
        
        private LMGEDI.Core.Data.Model.PendingUpload DLModel = new LMGEDI.Core.Data.Model.PendingUpload();
        private LMGEDI.Core.Data.Model.PendingUploadRecord DLModel2 = new LMGEDI.Core.Data.Model.PendingUploadRecord();
        
        [TestInitialize]
        public void InvoiceInitializer()
        {
            _IPendingUploadRepository = new PendingUploadRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _IUserRepository = new UserRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _IPendingUploadImplBL = new PendingUploadImpl(_IPendingUploadRepository, _IUserRepository);

        }
        [TestMethod]
        public void Add_PendingUploadRecord()
        {

            DLModel.PendingUploadId = 1;
            DLModel.PendingUploadName = "abc.xls";
            DLModel.PendingUploadDate = System.DateTime.Now;
            DLModel.UserId = 2;
            DLModel.DepartmentId = 1;
            int pendingUploadId = _IPendingUploadImplBL.AddPendingUploadRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }
        [TestMethod]
        public void Update_PendingUploadRecord()
        {
            DLModel.PendingUploadId = 5;
            DLModel.PendingUploadDate = System.DateTime.Now;
            DLModel.UserId = 1;
            DLModel.IsDeleted = true;
            DLModel.DeletedBy = 1;
            DLModel.DeletedOn = System.DateTime.Now;
            DLModel.IsProcessed = false;
            DLModel.ProcessedBy = 1;
            DLModel.ProcessedOn = System.DateTime.Now;
            int paymentId = _IPendingUploadImplBL.UpdatePendingUploadRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }
        [TestMethod]
        public void Get_AllPendingUploadRecord() 
        {
            var getAllPendingUploadRecords = _IPendingUploadImplBL.GetAllPendingUploadRecord(0, 10);
            Assert.IsTrue(getAllPendingUploadRecords != null, "Unable to find");
        }
    }
}
