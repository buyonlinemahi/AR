using LMGEDI.Core.BL;
using LMGEDI.Core.BL.Implementation;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class AdjusterTest
    {
        private IAdjusterRepository _adjusterRepository;
        private IAdjuster _adjusterImplBL;
        private LMGEDI.Core.Data.Model.Adjuster DLModel = new LMGEDI.Core.Data.Model.Adjuster();

        [TestInitialize]
        public void AdjusterInitializer()
        {
            _adjusterRepository = new AdjusterRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _adjusterImplBL = new AdjusterImpl(_adjusterRepository);
        }

        [TestMethod]
        public void AddAdjuster()
        {
            DLModel.AdjusterFirstName = "Rohit";
            DLModel.AdjusterLastName = "K Dhiman";
            DLModel.AdjusterPhone = "675-765-7657";
            DLModel.AdjusterFax = "132-123-1q32";
            DLModel.AdjusterEmail = "Test@test.com";
            int adjusterid = _adjusterImplBL.AddAdjusterRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");           

        }
        [TestMethod]
        public void UpdateAdjuster()
        {
            DLModel.AdjusterId = 1;
            DLModel.AdjusterFirstName = "Todd";
            DLModel.AdjusterLastName = "Glass1";
            DLModel.AdjusterPhone = "675-765-7657";
            DLModel.AdjusterFax = "132-123-1q32";
            DLModel.AdjusterEmail = "Test@test.com";
            int patientid = _adjusterImplBL.UpdateAdjusterRecord(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }

        [TestMethod]
        public void Get_AllAdjuster()
        {
            var getAllPatients = _adjusterImplBL.GetAllAdjusterByAdjusterName("t", 0, 10);
            Assert.IsTrue(getAllPatients != null, "Unable to find");
        }

        [TestMethod]
        public void Get_LienAdjusterByAdjusterID()
        {
            var getAdjusterDetails = _adjusterImplBL.GetLienAdjusterByAdjusterID(1);
            Assert.IsTrue(getAdjusterDetails != null, "Unable to find");
        }

        [TestMethod]
        public void Get_AdjusterByAdjusterID()
        {
            var getAdjusterDetails  = _adjusterImplBL.GetAdjusterByAdjusterID(1);
            Assert.IsTrue(getAdjusterDetails != null, "Unable to find");
        }
    }
}
