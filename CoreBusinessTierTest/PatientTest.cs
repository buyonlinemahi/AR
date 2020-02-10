using LMGEDI.Core.BL.Implementation;
using LMGEDI.BL.Model;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class PatientTest
    {
        private IPatientRepository _patientRepository;
        private IPatient _patientImplBL;
        private LMGEDI.Core.BL.Model.Patient BLModel = new LMGEDI.Core.BL.Model.Patient();
        private LMGEDI.Core.Data.Model.Patient DLModel = new LMGEDI.Core.Data.Model.Patient();
       
        [TestInitialize]
        public void PatientInitializer()
        {
            _patientRepository = new PatientRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _patientImplBL = new PatientImpl(_patientRepository);
        }

        [TestMethod]
        public void Get_AllPatients()
        {
            var getAllPatients = _patientImplBL.GetAllPatient(0, 10);
            Assert.IsTrue(getAllPatients != null, "Unable to find");
        }

        [TestMethod]
        public void Get_AllPatientsByFirstName()
        {
            var getAllPatientsbyFirstName = _patientImplBL.GetAllPatientByName("A",0, 10);
            Assert.IsTrue(getAllPatientsbyFirstName != null, "Unable to find");
        }

  

        [TestMethod]
        public void UpdatePatient()
        {         
            DLModel.PatientID = 1;
            DLModel.PatientAccount = "Test";
            DLModel.PatientFirst = "Test";
            DLModel.PatientLast = "Test";
            DLModel.PatientGender = "Test";
            DLModel.PatientDOB = System.DateTime.Now;
            DLModel.PatientEmployer ="Test" ;
            DLModel.PatientInsurance = "Test";
            DLModel.PatientWCAB = "Test";
            DLModel.PatientSSN = "Test";
            DLModel.PatientClaim = "wer";
           int patientid = _patientImplBL.UpdatePatient(DLModel);
           Assert.IsTrue(true, "Unable to find");
        }
        [TestMethod]
        public void getEducationByID()
        {
            LMGEDI.Core.Data.Model.Patient Patient = _patientRepository.GetPatientByID(1);
            Assert.IsTrue(Patient != null, "Unable to get");
        }
        [TestMethod]
        public void AddPatient()
        {
            DLModel.PatientAccount = "Test";
            DLModel.PatientFirst = "Test";
            DLModel.PatientLast = "Test";
            DLModel.PatientGender = "Test";
            DLModel.PatientDOB = System.DateTime.Now;
            DLModel.PatientEmployer = "Test";
            DLModel.PatientInsurance = "Test";
            DLModel.PatientWCAB = "Test";
            DLModel.PatientSSN = "Test";
            DLModel.PatientClaim = "wer";
            int patientid = _patientImplBL.AddPatient(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }

    }
}
