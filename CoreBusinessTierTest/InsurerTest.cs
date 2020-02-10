using LMGEDI.Core.BL.Implementation;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class InsurerTest
    {
        private IInsurerRepository _IInsurerRepository;
        private IInsurer _IInsurerImplBL;
        private LMGEDI.Core.Data.Model.Insurer DLModel = new LMGEDI.Core.Data.Model.Insurer();

        [TestInitialize]
        public void InsurerInitializer()
        {
            _IInsurerRepository = new InsurerRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _IInsurerImplBL = new InsurerImpl(_IInsurerRepository);
        }
        [TestMethod]
        public void AddInsurer()
        {
            DLModel.InsurerName = "Test123";
            DLModel.InsurerAddress = "InsurerAddress";
            DLModel.InsurerCity = "InsurerCity";
            DLModel.InsurerPhone = "(321)-321-1324";
            DLModel.InsurerStateID = 1;
            DLModel.InsurerZip = "90029-1234";
            int insurerId = _IInsurerImplBL.AddInsurerRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");

        }
        [TestMethod]
        public void UpdateInsurer()
        {
            DLModel.InsurerId = 1;
            DLModel.InsurerName = "Test";
            DLModel.InsurerAddress = "InsurerAddress";
            DLModel.InsurerCity = "InsurerCity";
            DLModel.InsurerPhone = "(321)-321-1324";
            DLModel.InsurerStateID = 1;
            DLModel.InsurerZip = "90029-1234";
            int patientid = _IInsurerImplBL.UpdateInsurerRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }

        [TestMethod]
        public void Get_AllInsurerByName()
        {
            var getAllInsurer = _IInsurerImplBL.GetAllInsurerByName("%", 0, 10);
            Assert.IsTrue(getAllInsurer != null, "Unable to find");
        }
    }
}
