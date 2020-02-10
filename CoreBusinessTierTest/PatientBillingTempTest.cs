using LMGEDI.Core.BL.Implementation;
using LMGEDI.BL.Model;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class PatientBillingTempTest
    {
        private IPatientBillingTempRepository _patientBillingTempRepository;
        private IPatientBillingTemp _patientBillingBL;
        private LMGEDI.Core.BL.Model.TempExcelDataColumns ExcelDataColumnModel = new LMGEDI.Core.BL.Model.TempExcelDataColumns();

        [TestInitialize]
        public void TempExcelInitializer()
        {
            _patientBillingTempRepository = new PatientBillingTempRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _patientBillingBL = new PatientBillingTempImpl(_patientBillingTempRepository);
        }

        [TestMethod]
        public void Get_AllCoulmnsFromTable()
        {
            var getAllPatients = _patientBillingBL.GetAllColumnNameFromTempPatientBilling();
            Assert.IsTrue(getAllPatients != null, "Unable to find");
        }

        [TestMethod]
        public void _DuplicatePatientBillingTempDataReplace()
        {
            var duplicateAllData = _patientBillingBL.DuplicatePatientBillingTempDataReplace();
            Assert.IsTrue(duplicateAllData != 0, "Unable to find");
        }
    }
}
