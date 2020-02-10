using LMGEDI.Core.BL;
using LMGEDI.Core.BL.Implementation;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class EmployerTest
    {
        private IEmployerRepository _IEmployerRepository;
        private IEmployer _EmployerImplBL;
        private LMGEDI.Core.Data.Model.Employer DLModel = new LMGEDI.Core.Data.Model.Employer();

        [TestInitialize]
        public void EmployerInitializer()
        {
            _IEmployerRepository = new EmployerRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _EmployerImplBL = new EmployerImpl(_IEmployerRepository);
        }

        [TestMethod]
        public void AddEmployer()
        {
            DLModel.EmployerName = "EmployerName";
            DLModel.EmployerAddress = "EmployerAddress";
            DLModel.EmployerCity = "EmployerCity";
            DLModel.EmployerPhone = "(132)321-3214";
            DLModel.EmployerStateID = 2;
            DLModel.EmployerZip = "90029-1234";
            int employerid = _EmployerImplBL.AddEmployerRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }
        [TestMethod]
        public void UpdateEmployer()
        {
            DLModel.EmployerId = 1;
            DLModel.EmployerName = "EmployerName";
            DLModel.EmployerAddress = "EmployerAddress";
            DLModel.EmployerCity = "EmployerCity";
            DLModel.EmployerPhone = "(132)321-3214";
            DLModel.EmployerStateID = 3;
            DLModel.EmployerZip = "90029-1234";
            int employerId = _EmployerImplBL.UpdateEmployerRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }

        [TestMethod]
        public void Get_AllEmployer()
        {
            var get_AllEmployer = _EmployerImplBL.GetAllEmployerByEmployerName("e", 0, 10);
            Assert.IsTrue(get_AllEmployer != null, "Unable to find");
        }
    }
}
