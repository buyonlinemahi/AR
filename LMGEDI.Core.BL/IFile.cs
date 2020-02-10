using LMGEDI.Core.BL.Model.Paged;
using System.Collections.Generic;
using DLModel = LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;


namespace LMGEDI.Core.BL
{
    public interface IFile
    {
        int AddFileRecord(BLModel.File modelFile);
        int UpdateFileRecord(BLModel.File modelFile);
        FileDetail GetFilesBySearch(string search, int skip, int take);
        DLModel.FileDetail GetFileDetailByFileId(int FileId);
        IEnumerable<BLModel.TempExcelDataColumns> GetAllColumnNameFromTblUploadTemps();
        DLModel.ErrorLogFile ALLValidateDataFromLienAndAR(int Department, int PendingUploadId, int UserID);
        int DeleteFileAccToFielID(int FileID, int DeletedBy);
         string UpdateFileCheckonClaimNo(DLModel.File file);
         int UpdateICFile(DLModel.File file);
         DLModel.ErrorLogFile PullDataFromLienToAR(int Department, int UserID, string Invoicedt);
         IEnumerable<BLModel.TempExcelDataColumns> GetAllColumnNameFromTblUploadTempsContractor();

    }
}
