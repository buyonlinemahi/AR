using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class FileRepository : BaseRepository<File,LMGEDIDBContext>,IFileRepository
    {
        public FileRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory), contextFactory)
        {
     
        }

        public int GetAllFilesCount(string searchText)
        {
            SqlParameter _searchText = new SqlParameter("@SearchText", searchText);
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.FileRepositoryProcedure.GetFilesCount, _searchText).SingleOrDefault();
        }
        public IEnumerable<FileSearchResult> GetFilesBySearch(string searchText, int skip, int take)
        {
            SqlParameter _searchText = new SqlParameter("@SearchText", searchText);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return Context.Database.SqlQuery<FileSearchResult>(Constant.StoredProcedureConst.FileRepositoryProcedure.GetFilesBySearch, _searchText, _skip, _take);
        }
        public FileDetail GetFileDetailByFileId(int FileId)
        {
            SqlParameter _fileId = new SqlParameter("@FileId", FileId);
            return Context.Database.SqlQuery<FileDetail>(Constant.StoredProcedureConst.FileRepositoryProcedure.GetFileDetailByFileId, _fileId).SingleOrDefault();
        }

        public IEnumerable<TempExcelDataColumns> GetAllColumnNameFromTblUploadTemps()
        {
            return Context.Database.SqlQuery<TempExcelDataColumns>(Constant.StoredProcedureConst.FileRepositoryProcedure.GetAllColumnNameFromTblUploadTemps);
        }

        public ErrorLogFile ALLValidateDataFromLienAndAR(int Department, int PendingUploadId, int UserID)
        {
            SqlParameter _department = new SqlParameter("@Department", Department);
            SqlParameter _PendingUploadId = new SqlParameter("@PendingUploadId", PendingUploadId);
            SqlParameter _UserID = new SqlParameter("@UserID", UserID);
            string sqlquery = "";
            if (Department == 1)
            {
                sqlquery = Constant.StoredProcedureConst.FileRepositoryProcedure.MoveTempUploadDataDepLien;
            }
            else if (Department == 2 || Department ==3 || Department == 4)
            {
                sqlquery = Constant.StoredProcedureConst.FileRepositoryProcedure.MoveTempUploadDataDepAR;
            }
            else if (Department == 5)
            {
                sqlquery = Constant.StoredProcedureConst.FileRepositoryProcedure.MoveTempUploadDataDepContractor;
            }
            Context.Database.CommandTimeout = 36000;
            return Context.Database.SqlQuery<ErrorLogFile>(sqlquery, _department, _PendingUploadId, _UserID).SingleOrDefault();
        }

        public int DeleteFileAccToFielID(int FileID,int DeletedBy)
        {
            SqlParameter _FileID = new SqlParameter("@FileID", FileID);
            SqlParameter _DeletedBy = new SqlParameter("@DeletedBy", DeletedBy);
            return Context.Database.ExecuteSqlCommand(Constant.StoredProcedureConst.FileRepositoryProcedure.DeleteFileAccToFielID, _FileID, _DeletedBy);
        }

        public int CheckClaimNumberExist(string claimNumber)
        {
            SqlParameter _claimNumber = new SqlParameter("@ClaimNumber", claimNumber);
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.FileRepositoryProcedure.CheckClaimNumberExist, _claimNumber).SingleOrDefault();
        }

        public string  UpdateFileCheckonClaimNo(File file)
        {
            SqlParameter _FileID = new SqlParameter("@FileID", file.FileID);
            SqlParameter _FirstName = new SqlParameter("@FirstName", file.FirstName);
            SqlParameter _LastName = new SqlParameter("@LastName", file.LastName);
            SqlParameter _claimNumber = new SqlParameter("@ClaimNumber", file.ClaimNumber);
            SqlParameter _InsurerId = new SqlParameter("@InsurerId", file.InsurerId);
            SqlParameter _InsurerBranchId = new SqlParameter("@InsurerBranchId", file.InsurerBranchId);
            SqlParameter _AdjusterId = new SqlParameter("@AdjusterId", file.AdjusterId);
            SqlParameter _EmployerId = new SqlParameter("@EmployerId", file.EmployerId);
            SqlParameter _IsLienClaimNumber = new SqlParameter("@IsLienClaimNumber", file.IsLienClaimNumber);
            SqlParameter _IsLienInsurerID = new SqlParameter("@IsLienInsurerID", file.IsLienInsurerID);
            SqlParameter _IsLienInsurerBranchID = new SqlParameter("@IsLienInsurerBranchID", file.IsLienInsurerBranchID);
            SqlParameter _IsLienEmployerID = new SqlParameter("@IsLienEmployerID", file.IsLienEmployerID);
            SqlParameter _IsLienAdjusterID = new SqlParameter("@IsLienAdjusterID", file.IsLienAdjusterID);
          //  SqlParameter _IsDeleted = new SqlParameter("@IsDeleted", file.IsDeleted);
            SqlParameter _DeletedBy = new SqlParameter("@DeletedBy", file.DeletedBy);
            SqlParameter _DeletedOn = new SqlParameter("@DeletedOn", file.DeletedOn);
            SqlParameter _Notes = new SqlParameter("@Notes", file.Notes == null ? string.Empty : file.Notes);
            return Context.Database.SqlQuery<string>(Constant.StoredProcedureConst.FileRepositoryProcedure.UpdateFileCheckonClaimNo, _FileID, _FirstName, _LastName, _claimNumber,
                                                     _InsurerId, _InsurerBranchId, _AdjusterId, _EmployerId, _IsLienClaimNumber, _IsLienInsurerID, _IsLienInsurerBranchID, _IsLienEmployerID,
                                                     _IsLienAdjusterID, _DeletedBy, _DeletedOn,_Notes).SingleOrDefault();
        }

        public int UpdateICFile(File file)
        {
            SqlParameter _FileID = new SqlParameter("@FileID", file.FileID);
            SqlParameter _FirstName = new SqlParameter("@FirstName", file.FirstName);
            SqlParameter _LastName = new SqlParameter("@LastName", file.LastName);
            return Context.Database.ExecuteSqlCommand(Constant.StoredProcedureConst.FileRepositoryProcedure.UpdateICFile, _FileID, _FirstName, _LastName);
        }
        public ErrorLogFile PullDataFromLienToAR(int Department, int UserID, string Invoicedt)
        {
            SqlParameter _department = new SqlParameter("@Department", Department);
            SqlParameter _UserID = new SqlParameter("@UserID", UserID);
            SqlParameter _invoiceDt = new SqlParameter("@Invoicedt", Invoicedt);
            SqlParameter _ForJob = new SqlParameter("@ForJob", System.Convert.ToBoolean(0));  // This parameter is for sql job from button it will send Zero but while sql job it will send 1
            Context.Database.CommandTimeout = 36000;
            return Context.Database.SqlQuery<ErrorLogFile>(Constant.StoredProcedureConst.FileRepositoryProcedure.MoveFrLienToAR, _department, _UserID, _invoiceDt, _ForJob).SingleOrDefault();
        }


        public IEnumerable<TempExcelDataColumns> GetAllColumnNameFromTblUploadTempsContractor()
        {
            return Context.Database.SqlQuery<TempExcelDataColumns>(Constant.StoredProcedureConst.FileRepositoryProcedure.GetAllColumnNameFromTblUploadTempsContractor);
        }

    }
}
