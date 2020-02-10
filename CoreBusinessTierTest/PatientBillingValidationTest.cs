using LMGEDI.Core.BL.Implementation;
using LMGEDI.BL.Model;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using DLModel = LMGEDI.Core.Data.Model;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class PatientBillingValidationTest
    {
        private IPatientBillingValidationRepository _patientBillingValidationRepository;
        private IPatientBillingValidation _patientBillingValidationBL;
        private DLModel.PatientBillingValidation patientBillingValidationModel = new DLModel.PatientBillingValidation();

        [TestInitialize]
        public void PatientBillingValidationInitializer()
        {
            _patientBillingValidationRepository = new PatientBillingValidationRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _patientBillingValidationBL = new PatientBillingValidationImpl(_patientBillingValidationRepository);
        }
        [TestMethod]
        public void Get_GetAllFromPatientBillingValidation()
        {
            var getAllPatientBillings = _patientBillingValidationBL.GetAllFromPatientBillingValidation(0, 10);
            Assert.IsTrue(getAllPatientBillings != null, "Unable to find");
        }
        [TestMethod]
        public void Update_ValidateDataFromBillingValidation()
        {
            patientBillingValidationModel.BillingID = 1;
            patientBillingValidationModel.PatientFName = "AARVIG, MICHAEL";
            patientBillingValidationModel.PatientSSN = "573-56-5519";
            patientBillingValidationModel.BillingInsurance = "YORK SERVICES";
            int updationResult = _patientBillingValidationBL.UpdateValidateDataFromBillingValidation(patientBillingValidationModel);
            Assert.IsTrue(true, "Unable to find");
        }

    }
}
