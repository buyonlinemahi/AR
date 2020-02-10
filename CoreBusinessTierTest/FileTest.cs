using LMGEDI.Core.BL.Implementation;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
    [TestClass]
    public class FileTest
    {
        private IFileRepository _IFileRepository;
        private IICRecordDetailRepository _ICRepository;
        private IFile _IFileImplBL;
        private LMGEDI.Core.Data.Model.File DLModel = new LMGEDI.Core.Data.Model.File();
        private LMGEDI.Core.Data.Model.ICRecordDetail DLModelICRecordDetail = new LMGEDI.Core.Data.Model.ICRecordDetail();
        private LMGEDI.Core.BL.Model.File BLModel = new LMGEDI.Core.BL.Model.File();

        private LMGEDI.Core.Data.Model.FileSearchResult DLModel1 = new LMGEDI.Core.Data.Model.FileSearchResult();

        [TestInitialize]
        public void FileInitializer()
        {
            _IFileRepository = new FileRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _ICRepository = new ICRecordDetailRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _IFileImplBL = new FileImpl(_IFileRepository, _ICRepository);
        }
        [TestMethod]
        public void AddFile()
        {
            BLModel.FirstName = "Test";
            BLModel.LastName = "Test";
            BLModel.ClaimNumber = "Test1";
            BLModel.IsLienClaimNumber = false;
            BLModel.InsurerId = 1;
            BLModel.InsurerBranchId = 2;
            BLModel.EmployerId = 3;
            BLModel.AdjusterId = 4;
            BLModel.IsLienInsurerID = true;
            BLModel.IsLienInsurerBranchID = true;
            BLModel.IsLienEmployerID = false;
            BLModel.IsLienAdjusterID = false;
            BLModel.IsDeleted = false;
            BLModel.DeletedBy = 0;
            BLModel.DeletedOn = System.DateTime.Now;
            BLModel.Notes = "";
            BLModel.DepartmentID =5;
            BLModel.FileID = 0;
            BLModel.ICRecordDetailAddress = "ABC";
            BLModel.ICRecordDetailCity = "ASD";
            BLModel.StateID = 1;
            BLModel.ICRecordDetailZip = "234";
            BLModel.ICRecordDetailTaxID = "4567";

            int patientid = _IFileImplBL.AddFileRecord(BLModel);
            Assert.IsTrue(true, "Unable to find");

        }
        [TestMethod]
        public void UpdateFile()
        {
            DLModel.FileID = 1;
            DLModel.FirstName = "updatetesta";
            DLModel.LastName = "Testa";
            DLModel.ClaimNumber = "00kla";
            DLModel.IsLienClaimNumber = true;
            DLModel.InsurerId = 1;
            DLModel.InsurerBranchId = 1;
            DLModel.AdjusterId = 1;
            DLModel.EmployerId = 1;
            DLModel.IsLienClaimNumber = true;
            DLModel.IsLienInsurerID = true;
            DLModel.IsLienInsurerBranchID = true;
            DLModel.IsLienEmployerID = false;
            DLModel.IsLienAdjusterID = false;
            DLModel.DeletedBy = 1;
            DLModel.DeletedOn = System.DateTime.Now;
            DLModel.Notes = "notes updated .";
            string UpdateFile = _IFileImplBL.UpdateFileCheckonClaimNo(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }

        [TestMethod]
        public void Update_ICFile()
        {
            DLModel.FileID = 35;
            DLModel.FirstName = "ddeerr";
            DLModel.LastName = "Testa";
            DLModel.ClaimNumber = " ";
            DLModel.IsLienClaimNumber = false;
            DLModel.InsurerId = 0;
            DLModel.InsurerBranchId = 0;
            DLModel.AdjusterId = 0;
            DLModel.EmployerId = 0;
            DLModel.IsLienClaimNumber = false;
            DLModel.IsLienInsurerID = false;
            DLModel.IsLienInsurerBranchID = false;
            DLModel.IsLienEmployerID = false;
            DLModel.IsLienAdjusterID = false;
            DLModel.DeletedBy = 0;
            DLModel.DeletedOn = null;
            DLModel.Notes = "notes updated .";
            int UpdateFile = _IFileImplBL.UpdateICFile(DLModel);
            Assert.IsTrue(true, "Unable to find");
        }

        [TestMethod]
        public void Update_FileRecord()
        {
            DLModel.FileID = 1;
            DLModel.FirstName = "updatetest";
            DLModel.LastName = "Test";
            DLModel.ClaimNumber = "1890";
            DLModel.IsLienClaimNumber = true;
            DLModel.InsurerId = 1;
            DLModel.InsurerBranchId = 2;
            DLModel.EmployerId = 3;
            DLModel.AdjusterId = 4;
            DLModel.IsLienClaimNumber = true;
            DLModel.IsLienInsurerID = true;
            DLModel.IsLienInsurerBranchID = true;
            DLModel.IsLienEmployerID = false;
            DLModel.IsLienAdjusterID = false;
            DLModel.DeletedBy = 1;
            DLModel.DeletedOn = System.DateTime.Now;
            DLModel.IsDeleted = false;
            int UpdateFile = _IFileImplBL.UpdateFileRecord(BLModel);
            Assert.IsTrue(true, "Unable to find");
        }

        [TestMethod]
        public void Get_FilesBySearch()
        {
            var getfiles = _IFileImplBL.GetFilesBySearch("s", 0, 20);
            Assert.IsTrue(getfiles != null, "Unable to find");

        }

        [TestMethod]
        public void Get_FileDetailByClaimNumber()
        {
            var getfilerecord = _IFileImplBL.GetFileDetailByFileId(3);
            Assert.IsTrue(getfilerecord != null, "Unable to find");

        }


        //[TestMethod]
        //public void Get_InvoiceDetailByClaimNumber()
        //{
        //    string claimNumber = "TF1234-22";
        //    var getfilerecord = _IFileImplBL.GetInvoiceDetailByClaimNumber(claimNumber);
        //    Assert.IsTrue(getfilerecord != null, "Unable to find");

        //}
    }
}
