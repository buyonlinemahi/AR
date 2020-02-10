using LMGEDI.Core.BL.Implementation;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LMGEDI.Core.Data.Model;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class LienTempTableTest
    {
        private ILienTempTableRepository _ILienTempTableRepository;
        private ILienTempTable _ILienTempBL;
        private LMGEDI.Core.Data.Model.LienTempTable DLModel = new LMGEDI.Core.Data.Model.LienTempTable();

        [TestInitialize]
        public void FileInitializer()
        {
            _ILienTempTableRepository = new LienTempTableRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _ILienTempBL = new LienTempTableImpl(_ILienTempTableRepository);
        }

         [TestMethod]
        public void UpdateLienTempTable()
        {
            List<LMGEDI.Core.Data.Model.LienTempTable> s = new List<LMGEDI.Core.Data.Model.LienTempTable>();
            LienTempTable b1 = new LienTempTable
            {
                UploadId = 31,
                Claim = "45286"
            };
            LienTempTable b2 = new LienTempTable
            {
                UploadId = 32,
                Claim = "4563"
            };
            LienTempTable b3 = new LienTempTable
            {
                UploadId = 33,
                Claim = "458"
            };
            LienTempTable b4 = new LienTempTable
            {
                UploadId = 34,
                Claim = "456"
            };
            LienTempTable b5 = new LienTempTable
            {
                UploadId = 35,
                Claim = "125"
            };
            s.Add(b1);
            s.Add(b2);
            s.Add(b3);
            s.Add(b4);
            s.Add(b5);


           var result = _ILienTempBL.UpdateLienTempClaimNumber(s);
            Assert.IsTrue(true, "Unable to find");
        }

         [TestMethod]
         public void Get_AllLienTempData()
         {
             var getAllOCRRecords = _ILienTempBL.GetAllLienTempData(1, 10);
             Assert.IsTrue(getAllOCRRecords != null, "Unable to find");
         }
        
    }
}
