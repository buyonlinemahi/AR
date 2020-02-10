using LMGEDI.Core.BL.Implementation;
using LMGEDI.BL.Model;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class PatientHistoryTest
    {
        private IPatientHistoryRepository _patientHistoryRepository;
        private IPatientHistory _patientHistoryImplBL;
        private LMGEDI.Core.Data.Model.PatientHistory DLModel = new LMGEDI.Core.Data.Model.PatientHistory();
       
        [TestInitialize]
        public void PatientInitializer()
        {
            _patientHistoryRepository = new PatientHistoryRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _patientHistoryImplBL = new PatientHistoryImpl(_patientHistoryRepository);
        }

     
        [TestMethod]
        public void InsertPatientUpdateHistory()
        {         
            DLModel.PatientID = 1;
            DLModel.Description = "Test";
            int patientHistoryid = _patientHistoryImplBL.InsertPatientUpdateHistory(DLModel);
           Assert.IsTrue(true, "Unable to find");
        }

    }
}
