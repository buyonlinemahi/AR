using LMGEDI.Core.Data;
using DLModel = LMGEDI.Core.Data.Model;
using System.Linq;
using BLModel = LMGEDI.Core.BL.Model;
using Omu.ValueInjecter;
using System.Collections.Generic;
using LMGEDI.Core.BL.Model.Paged;

namespace LMGEDI.Core.BL.Implementation
{
    public class FileImpl : IFile
    {
        private readonly IFileRepository _fileRepository;
        private readonly IICRecordDetailRepository _ICRepository;

        public FileImpl(IFileRepository fileRepository, IICRecordDetailRepository ICRepository)
        {
            _fileRepository = fileRepository;
            _ICRepository = ICRepository;
        }

       
       

        public int AddFileRecord(BLModel.File modelFile)
        {
            if (_fileRepository.CheckClaimNumberExist(modelFile.ClaimNumber) == 0 || modelFile.DepartmentID == 5)
            {
                DLModel.ICRecordDetail objICRecordDetail = new DLModel.ICRecordDetail();
                DLModel.File objFile = new DLModel.File();

                objFile.FirstName = modelFile.FirstName;
                objFile.LastName = modelFile.LastName;
                objFile.ClaimNumber = modelFile.ClaimNumber;
                objFile.InsurerId = modelFile.InsurerId;
                objFile.InsurerBranchId = modelFile.InsurerBranchId;
                objFile.EmployerId = modelFile.EmployerId;
                objFile.AdjusterId = modelFile.AdjusterId;
                objFile.IsLienClaimNumber = modelFile.IsLienClaimNumber;
                objFile.IsLienInsurerID = modelFile.IsLienInsurerID;
                objFile.IsLienInsurerBranchID = modelFile.IsLienInsurerBranchID;
                objFile.IsLienEmployerID = modelFile.IsLienEmployerID;
                objFile.IsLienAdjusterID = modelFile.IsLienAdjusterID;
                objFile.IsDeleted = modelFile.IsDeleted;
                objFile.DeletedBy = modelFile.DeletedBy;
                objFile.DeletedOn = modelFile.DeletedOn;
                objFile.Notes = modelFile.Notes;
                objFile.DepartmentID = modelFile.DepartmentID;
                
                var _fileID = _fileRepository.Add((DLModel.File)new DLModel.File().InjectFrom(modelFile)).FileID;
                if (modelFile.DepartmentID == 5)
                {
                    objICRecordDetail.FileID = _fileID;
                    objICRecordDetail.ICRecordDetailAddress = modelFile.ICRecordDetailAddress;
                    objICRecordDetail.ICRecordDetailCity = modelFile.ICRecordDetailCity;
                    objICRecordDetail.StateID = modelFile.StateID;
                    objICRecordDetail.ICRecordDetailZip = modelFile.ICRecordDetailZip;
                    objICRecordDetail.ICRecordDetailTaxID = modelFile.ICRecordDetailTaxID;
                    var _ICRecordDetailID = _ICRepository.Add((DLModel.ICRecordDetail)new DLModel.ICRecordDetail().InjectFrom(objICRecordDetail)).ICRecordDetailID;
                }
                return _fileID;
            }
            else
            {
                //  Its means claim number already exists ....
                return 0;
            }
        }

        public int UpdateFileRecord(BLModel.File modelFile)
        {
            int ClaimNumberExists = 0;
            ClaimNumberExists = _fileRepository.GetAll(file => file.ClaimNumber == modelFile.ClaimNumber && file.FileID != modelFile.FileID).Count();
            if (ClaimNumberExists == 0 || modelFile.DepartmentID == 5)
            {
                DLModel.ICRecordDetail objICRecordDetail = new DLModel.ICRecordDetail();
                DLModel.File objFile = new DLModel.File();

                objFile.FileID = modelFile.FileID;
                objFile.FirstName = modelFile.FirstName;
                objFile.LastName = modelFile.LastName;
                objFile.ClaimNumber = modelFile.ClaimNumber;
                objFile.InsurerId = modelFile.InsurerId;
                objFile.InsurerBranchId = modelFile.InsurerBranchId;
                objFile.EmployerId = modelFile.EmployerId;
                objFile.AdjusterId = modelFile.AdjusterId;
                objFile.IsLienClaimNumber = modelFile.IsLienClaimNumber;
                objFile.IsLienInsurerID = modelFile.IsLienInsurerID;
                objFile.IsLienInsurerBranchID = modelFile.IsLienInsurerBranchID;
                objFile.IsLienEmployerID = modelFile.IsLienEmployerID;
                objFile.IsLienAdjusterID = modelFile.IsLienAdjusterID;
                objFile.IsDeleted = modelFile.IsDeleted;
                objFile.DeletedBy = modelFile.DeletedBy;
                objFile.DeletedOn = modelFile.DeletedOn;
                objFile.Notes = modelFile.Notes;
                objFile.DepartmentID = modelFile.DepartmentID;

                _fileRepository.Update((DLModel.File)new DLModel.File().InjectFrom(modelFile));
                if (modelFile.DepartmentID == 5)
                {
                    objICRecordDetail.FileID = modelFile.FileID;
                    objICRecordDetail.ICRecordDetailID = modelFile.ICRecordDetailID;
                    objICRecordDetail.ICRecordDetailAddress = modelFile.ICRecordDetailAddress;
                    objICRecordDetail.ICRecordDetailCity = modelFile.ICRecordDetailCity;
                    objICRecordDetail.StateID = modelFile.StateID;
                    objICRecordDetail.ICRecordDetailZip = modelFile.ICRecordDetailZip;
                    objICRecordDetail.ICRecordDetailTaxID = modelFile.ICRecordDetailTaxID;
                    var _ICRecordDetailID = _ICRepository.Update((DLModel.ICRecordDetail)new DLModel.ICRecordDetail().InjectFrom(objICRecordDetail));
                }
                return modelFile.FileID;
               
            }
            else
            {
                //  Its means claim number already exists ....
                return 0;
            }
        }
        public int UpdateICFile(DLModel.File modelFile)
        {
            return _fileRepository.UpdateICFile((DLModel.File)new DLModel.File().InjectFrom(modelFile));
        }

        public string UpdateFileCheckonClaimNo(DLModel.File modelFile)
        {
            return _fileRepository.UpdateFileCheckonClaimNo((DLModel.File)new DLModel.File().InjectFrom(modelFile));
        }

        public FileDetail GetFilesBySearch(string search, int skip, int take)
        {
            return new BLModel.Paged.FileDetail
            {
                FileDetails = _fileRepository.GetFilesBySearch(search, skip, take).ToList(),
                TotalCount = _fileRepository.GetAllFilesCount(search)
            };
        }
        public DLModel.FileDetail GetFileDetailByFileId(int FileId)
        {
            return _fileRepository.GetFileDetailByFileId(FileId);
        }


        public IEnumerable<BLModel.TempExcelDataColumns> GetAllColumnNameFromTblUploadTemps()
        {
            return _fileRepository.GetAllColumnNameFromTblUploadTemps().Select(tempExcelData => new BLModel.TempExcelDataColumns().InjectFrom(tempExcelData)).Cast<LMGEDI.Core.BL.Model.TempExcelDataColumns>().ToList();
        }

        public DLModel.ErrorLogFile ALLValidateDataFromLienAndAR(int Department, int PendingUploadId, int UserID)
        {
            return _fileRepository.ALLValidateDataFromLienAndAR(Department, PendingUploadId, UserID);
        }

        public DLModel.ErrorLogFile PullDataFromLienToAR(int Department, int UserID, string Invoicedt)
        {
            return _fileRepository.PullDataFromLienToAR(Department, UserID, Invoicedt);
        }


        public int DeleteFileAccToFielID(int FileID, int DeletedBy)
        {
            return _fileRepository.DeleteFileAccToFielID(FileID, DeletedBy);
        }

        public IEnumerable<BLModel.TempExcelDataColumns> GetAllColumnNameFromTblUploadTempsContractor()
        {
            return _fileRepository.GetAllColumnNameFromTblUploadTempsContractor().Select(tempExcelData => new BLModel.TempExcelDataColumns().InjectFrom(tempExcelData)).Cast<LMGEDI.Core.BL.Model.TempExcelDataColumns>().ToList();
        }

    }
}
