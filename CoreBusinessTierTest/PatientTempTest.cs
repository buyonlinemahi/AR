using LMGEDI.Core.BL.Implementation;
using LMGEDI.BL.Model;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class PatientTempTest
    {
        private IPatientTempRepository _tempExcelDataRepository;
        private IPatientTemp _tempExcelDataBL;
        private LMGEDI.Core.BL.Model.TempExcelDataColumns ExcelDataColumnModel = new LMGEDI.Core.BL.Model.TempExcelDataColumns();

        [TestInitialize]
        public void TempExcelInitializer()
        {
            _tempExcelDataRepository = new PatientTempRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _tempExcelDataBL = new PatientTempImpl(_tempExcelDataRepository);
        }

        [TestMethod]
        public void Get_AllCoulmnsFromTable()
        {
            var getAllPatients = _tempExcelDataBL.GetAllColumnNameFromTempPatient();
            Assert.IsTrue(getAllPatients != null, "Unable to find");
        }

        [TestMethod]
        public void _DuplicatePatientTempDataReplace()
        {
            var duplicateAllData = _tempExcelDataBL.DuplicatePatientTempDataReplace();
            Assert.IsTrue(duplicateAllData !=0 , "Unable to find");
        }
    }
}
