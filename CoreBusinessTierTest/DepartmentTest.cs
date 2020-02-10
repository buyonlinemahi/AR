using LMGEDI.Core.BL;
using LMGEDI.Core.BL.Implementation;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class DepartmentTest
    {
        private IDepartmentRepository _IDepartmentRepository;
        private IDepartment _DepartmentImplBL;
        private LMGEDI.Core.Data.Model.Department DLModel = new LMGEDI.Core.Data.Model.Department();

        [TestInitialize]
        public void DepartmentInitializer()
        {
            _IDepartmentRepository = new DepartmentRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _DepartmentImplBL = new DepartmentImpl(_IDepartmentRepository);
        }

        [TestMethod]
        public void Get_Department()
        {
            var getDepartment = _DepartmentImplBL.GetDepartment();
            Assert.IsTrue(getDepartment != null, "Unable to find");
        }
    }
}
