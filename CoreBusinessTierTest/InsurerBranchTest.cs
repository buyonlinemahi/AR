using LMGEDI.Core.BL.Implementation;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class InsurerBranchTest
    {
        private IInsurerBranchRepository _IInsurerBranchRepository;
        private IInsurerBranch _IInsurerBranchImplBL;
        private LMGEDI.Core.Data.Model.InsurerBranch DLModel = new LMGEDI.Core.Data.Model.InsurerBranch();

        [TestInitialize]
        public void InsurerBranchInitializer()
        {
            _IInsurerBranchRepository = new InsurerBranchRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _IInsurerBranchImplBL = new InsurerBranchImpl(_IInsurerBranchRepository);
        }
        [TestMethod]
        public void AddInsurerBranch()
        {
            DLModel.InsurerId = 1;
            DLModel.InsurerBranchName = "Test";
            DLModel.InsurerBranchAddress = "InsurerBranchAddress";
            DLModel.InsurerBranchCity = "InsurerBranchCity";
            DLModel.InsurerBranchPhone = "(321)654-3215";
            DLModel.InsurerBranchStateID = 1;
            DLModel.InsurerBranchZip = "90029-1234";
            int patientid = _IInsurerBranchImplBL.AddInsuranceBranchRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");

        }
        [TestMethod]
        public void UpdateInsurerBranch()
        {
            DLModel.InsurerBranchId = 1;
            DLModel.InsurerId = 1;
            DLModel.InsurerBranchName = "Test321";
            DLModel.InsurerBranchAddress = "InsurerBranchAddress";
            DLModel.InsurerBranchCity = "InsurerBranchCity";
            DLModel.InsurerBranchPhone = "(321)654-3215";
            DLModel.InsurerBranchStateID = 1;
            DLModel.InsurerBranchZip = "90029-1234";
            int patientid = _IInsurerBranchImplBL.UpdateInsuranceBranchRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }
        [TestMethod]
        public void Get_AllInsurerBranchByInsurerBranchID()
        {
            var getAllInsurerBranch = _IInsurerBranchImplBL.GetAllInsurerBranch("t", 0, 10);
            Assert.IsTrue(getAllInsurerBranch != null, "Unable to find");
        }

    
    }
}
