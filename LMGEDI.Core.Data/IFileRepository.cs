using Core.Base.Data;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;

namespace LMGEDI.Core.Data
{
    public interface IFileRepository : IBaseRepository<File>
    {
        int GetAllFilesCount(string searchText);
        IEnumerable<FileSearchResult> GetFilesBySearch(string searchText, int skip, int take);
        FileDetail GetFileDetailByFileId(int FileId);
        IEnumerable<TempExcelDataColumns> GetAllColumnNameFromTblUploadTemps();
        ErrorLogFile ALLValidateDataFromLienAndAR(int Department, int PendingUploadId, int UserID);
        int DeleteFileAccToFielID(int FileID, int DeletedBy);     
        int CheckClaimNumberExist(string ClaimNumber);     
        string UpdateFileCheckonClaimNo(File file);
        int UpdateICFile(File file);
        ErrorLogFile PullDataFromLienToAR(int Department, int UserID, string Invoicedt);
        IEnumerable<TempExcelDataColumns> GetAllColumnNameFromTblUploadTempsContractor();
        
    }
}
