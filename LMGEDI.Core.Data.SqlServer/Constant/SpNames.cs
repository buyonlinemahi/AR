
namespace LMGEDI.Core.Data.SqlServer.Constant
{
    public class StoredProcedureConst
    {
        private const string SQLExec = "EXEC ";


        public struct TempExcelDataRepositoryProcedure
        {
            //-----------------PatientTemp----------------------------//
            public const string GetAllColumnNameFromTempPatient = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllColumnNameFromTempPatient";
        }
        public struct AdjusterRepositoryProcedure
        {
            public const string GetAllAdjuster = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllAdjuster @AdjusterName,@Skip, @Take";
            public const string GetAllAdjusterCount = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllAdjusterCount @AdjusterName";
            public const string CheckAdjusterExist = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Check_AdjusterExist @AdjusterFirstName,@AdjusterLastName";
            public const string GetAdjusterByAdjusterID = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AdjusterByAdjusterID @AdjusterId";
            public const string GetLienAdjusterByAdjusterID = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_LienAdjusterByAdjusterID @AdjusterId";
        }
        public struct EmployerRepositoryProcedure
        {
            public const string GetAllEmployer = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllEmployer @EmployerName, @Skip, @Take";
            public const string GetAllEmployerCount = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllEmployerCount @EmployerName ";
            public const string CheckEmployerExist = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Check_EmployerExist @EmployerName";

        }
        public struct FileRepositoryProcedure
        {
            public const string GetFilesCount = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_FilesCount @SearchText";
            public const string GetFilesBySearch = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllFiles @SearchText, @Skip, @Take";
            public const string GetFileDetailByFileId = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_FileDetailByFileId @FileId";
            public const string GetAllColumnNameFromTblUploadTemps = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllColumnNameFromTblUploadTemps";
            public const string GetAllColumnNameFromTblUploadTempsContractor = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllColumnNameFromTblUploadTempsContractor";
            public const string ALLValidateDataFromLienAndAR = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "ALLValidateDataFromLienAndAR  @Department,@PendingUploadId,@UserID";
            public const string DeleteFileAccToFielID = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Delete_FileAccToFielID  @FileID,@DeletedBy";
            public const string CheckClaimNumberExist = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Check_ClaimNumberExist @ClaimNumber";
            public const string UpdateFileCheckonClaimNo = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Update_File @FileID, @FirstName,@LastName,@ClaimNumber,@InsurerId,@InsurerBranchId,@AdjusterId,@EmployerId,@IsLienClaimNumber,@IsLienInsurerID,@IsLienInsurerBranchID,@IsLienEmployerID,@IsLienAdjusterID,@DeletedBy,@DeletedOn,@Notes";
            public const string MoveTempUploadDataDepLien = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Move_TempUploadDataDepLien  @Department,@PendingUploadId,@UserID";
            public const string MoveTempUploadDataDepAR = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Move_TempUploadDataDepAR  @Department,@PendingUploadId,@UserID";
            public const string MoveTempUploadDataDepContractor = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Move_TempUploadDataDepContractor  @Department,@PendingUploadId,@UserID";
            public const string MoveFrLienToAR = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Move_FrLienToAR  @Department,@UserID,@Invoicedt,@ForJob";
            public const string UpdateICFile = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Update_ICFile @FileID,	@FirstName,	@LastName" ;
        }
        public struct InsurerRepositoryProcedure
        {
            public const string GetAllInsurer = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllInsurer @InsurerName, @Skip, @Take";
            public const string GetAllInsurerCount = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllInsurerCount @InsurerName";
            public const string CheckInsurerExist = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Check_InsurerExist @InsurerName";
        }
        public struct InsurerBranchRepositoryProcedure
        {
            public const string GetAllInsurerBranch = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllInsurerBranch @InsurerBranchName, @Skip, @Take";
            public const string GetAllInsurerBranchCount = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllInsurerBranchCount @InsurerBranchName";
            public const string GetAllInsurerBranchByInsurerID = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllInsurerBranchByInsurerID @InsurerId,@InsurerBranchName,@DBName,@Skip,@Take";
            public const string CheckInsurerBranchExist = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Check_InsurerBranchExist @InsurerBranchName";
        }
        public struct InvoiceRepositoryProcedure
        {
            public const string GetAllInvoice = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllInvoice @FileId, @Skip, @Take";

            public const string GetAllInvoiceIC = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllInvoiceIC @FileId";

            public const string UpdateICAmtAccFileIdInvID = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Update_ICAmtAccFileIdInvID @FileId, @InvoiceID, @InvoiceICAmt";

            public const string UpdateAssignedToInvoiceIDByInvoiceID = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Update_AssignedToInvoiceIDByInvoiceID @InvoiceID, @SubInvoiceID";
            public const string GetAssignedInvoicedByInvoiceID = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AssignedInvoicedByInvoiceID @InvoiceID";
            //---------------------------------Invoice Notes ----------------------------------------------//
            public const string GetAllInvoiceNotes = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_InvoiceDetailByInvoiceID @InvoiceID, @Skip, @Take";
            //---------------------------------Invoice Adjustments ----------------------------------------//
            public const string UpdateInvoiceAdjustmentByInvoiceID = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Update_InvoiceAdjustmentByInvoiceID @InvoiceID,@AdjustmentNotes,@CreatedBy,@AdjustmentAmt";
        }
        public struct LienTempTableRepositoryProcedure
        {
            public const string UpdateLienTempClaimNumberByUploadID = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Update_LienTempClaimNumberByUploadID  @Claim, @UploadId, @FirstName, @LastName, @Adjuster,@Insurer,@InsurerBranch,@Employer";
            public const string ProcessedLienTempTable = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Processed_LienTempTable";
            public const string GetAllLienTempRecords = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllLienTempRecords  @Skip, @Take";

        }

        public struct OCRRepositoryProcedure
        {
            public const string GetOCRFilesInvoiceRecords = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_OCRFilesInvoiceRecords  @SearchText, @Skip, @Take";
            public const string GetOCRFilesInvoiceRecordsCount = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_OCRFilesInvoiceRecordsCount @SearchText";

            public const string GetOCRPaymentFilesRecords = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_OCRPaymentFilesRecords  @SearchText, @Skip, @Take";
            public const string GetOCRPaymentFilesRecordsCount = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_OCRPaymentFilesRecordsCount @SearchText";

            public const string AddOCRPaymentRecords = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Add_OCRPaymentRecords @OCRId, @CreatedBy, @InvoiceId, @PaymentAmount, @PaymentReceived, @CheckNumber,@CheckDate, @CheckUploadName";

        }

        public struct PaymentRefundRepositoryProcedure
        {
            public const string GetNetworkingDays = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_NetworkingDays  @InvoiceDate, @DepartmentId";
        }

        public struct CommissionRepositoryProcedure
        {
            public const string GetCommissionRecordByLienClientID = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_CommissionRecordByLienClientID  @LienClientID, @Skip,@Take";
            public const string GetCommissionRecordByLienClientIDCount = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_CommissionRecordByLienClientIDCount  @LienClientID";
            public static string GetAllLienClientBilling = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_AllLienClientBilling";
            public const string GetCommissionDashboardReaord = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_CommissionDashboardRecord @Skip,@Take";
            public const string GetCommissionDashboardRecordCount = SQLExec + Consts.Schema.GLOBAL + Consts.Schema.DOT + "Get_CommissionDashboardRecordCount";  
        }

    }
}
