
namespace LMGEDIApp.Infrastructure.Global
{
    public class GlobalConst
    {
        public enum PatientSearchCriteria { ByAllPatients =1, ByPatientFirstName = 2,ByPatientLastName = 3}

        public struct SessionKeys
        {
            public const string userDetailMgmt = "userDetailMgmt";
            public const string userID = "userID";
            public const string userName = "userName";
            public const string UserFullName = "UserFullName";
        }
        public struct Records
        {
            public const int Skip = 0;
            public const int Take = 10;
            public const int LandingTake = 20;
            public const int FileTake = 10;
            public const int Take5 = 5;          
            public const int LandingTake25 = 25;
        }
        public struct Controllers
        {
            public const string User = "User";
            public const string Home = "Home";
            public const string Patient = "Patient";
            public const string File = "File";
            public const string ExportExcelToDatabase = "ExportExcelToDatabase";
            public const string CheckAssignment = "CheckAssignment";
            public const string Commission = "Commission";
        }
       
        public struct Actions
        {
            public struct UserController
            {
                public const string Index = "Index";
                public const string Login = "Login";
                public const string Userlanding = "Userlanding";
                public const string Add = "Add";
                public const string Detail = "Detail";
                public const string Update = "Update";
            }
            
            public struct PatientController
            {
                public const string Index = "Index";
                public const string Update = "Update";
                public const string Add = "Add";
            }
            public struct FileController
            {
                public const string Index = "Index";
                public const string Add = "Add";
                public const string Update = "Update";
                public const string AddInvoice = "AddInvoice";
                public const string AddFile = "AddFile";
                public const string AddAdjuster = "AddAdjuster";
                public const string AddEmployer = "AddEmployer";
                public const string AddInsurer = "AddInsurer";
                public const string AddInsurerBranch = "AddInsurerBranch";
                public const string FileLanding = "FileLanding";
                public const string SavePaymentRecord = "SavePaymentRecord";
                public const string SavePaymentRefundRecord = "SavePaymentRefundRecord";
            }

            public struct CommissionController
            {
                public const string SaveEmployerRecord = "SaveEmployerRecord";
            }

            public struct ExportExcelToDatabaseController
            {
                public const string Index = "Index";
                public const string MappingExcelDatabaseField = "MappingExcelDatabaseField";
                public const string ImportExcel = "ImportExcel";
                public const string UpdateLienItemTable = "UpdateLienItemTable";
         
            }
            //not area specific controller
            public struct HomeController
            {
                public const string Index = "Index";
                public const string LogOut = "LogOut";
            }

            public struct CheckAssignmentController 
            {
                public const string Index = "Index";
                public const string UploadOCRFile = "UploadOCRFile";
            }
        }

        public struct Views
        {
            public struct User
            {
                public const string Index = "Index";
                public const string Login = "Login";
                public const string Userlanding = "Userlanding";
                public const string Add = "Add";
                public const string Detail = "Detail";
            }
            public struct Home
            {
                public const string Index = "Index";
            }
            public struct Patient
            {
                public const string Detail = "Detail";
                public const string Add = "Add";
            }
            public struct ExportExcelToDatabase
            {
                public const string ImportExcel = "../ExportExcelToDatabase/ImportExcel";
            }

        }

        public struct ContentTypes
        {
            public const string TextHtml = "text/html";
        }

        public struct ObjectTypes
        {
            public const string Success = "success";
            public const string Error = "Error occur";
            public const string Uploaded = "Uploaded Successfully";
        }
       
        public struct CommonValues
        {
            public const string PatientInfoTemp = "0";
            public const string PatientBillingTemp = "1";
            public const string Excel97_03 = ".xls";
            public const string Excel07_higher = ".xlsx";
            public const string UploadDrivePath = "~/UploadExcel/";
            public const string TableName = "TABLE_NAME";
            public const string ColumnName = "Column_name";
            public const string PendingUploadId = "PendingUploadId";
            public const string InvoiceDept = "InvoiceDept";
            public const string Sheet1 = "Sheet1";
            public const string Complete = "1";
            public const string CompleteUpdateOnly = "0";
            public const int One = 1;
            public const int Five = 5;
            public const int Zero = 0;
            public const int SQLBatchSize = 500;
            public const int SQLTimeOut = 3600000;
        }

        public struct ExcelTempTables
        {
            public const string PatientInfoTempTable = "global.tblPatientTemp";
            public const string PatientBillingTempTable = "global.tblPatientBillingTemp";
            public const string SelectExcelData = "SELECT * FROM [";
            public const string CommandEnd = "]";
            public const string ExcelUploadTables ="global.tblUploadTemps";
            public const string ExcelUploadContractorTables = "global.tblUploadTempsContractor";
        }
        public struct Configuration
        {
            public const string ContextClass = "LMGEDIDBContext";
            public const string Excel03 = "Excel03ConString";
            public const string Excel07 = "Excel07+ConString";
        }
        public struct alertMessages
        {
            public const string InsertedSuccessfully = "Inserted Successfully.";
            public const string Error = "Error occur, Please check it.";        
            public const string UpdatedSuccessfully = "Updated Successfully.";
            public const string DeletedSuccessfully = "Deleted Successfully.";
            public const string SaveMessage = "Saved Successfully";
            // for user
            public const string UserNamePasswordIncorrect = "UserName or Password is incorrect.";

            public const string UserNameInactive = "UserName Inactive.";
            public const string DuplicateInvoice = "Duplicate invoice record exist.";
            public const string DuplicateClaimNo = "Duplicate Claim Number exist.";
            public const string ExceptionOccured = "Exception Occured please see the log file";
        }
        public struct ConstantChar
        {
            public const char RoundBracketStart = '(';
            public const char NullChar = '\0';
            public const string Blank = "";
            public const string DoubleSlash = "\\";
            public const string FrontSlash = "/";
            public const string Dot = ".";
            public const string columnZero = "Column {0}";
            public const string StoragePath = "StoragePath";
            public const string UploadFile = "uploadFile";
            public const string OCRUploads = "OCRUploads";
            public const string ExcelUploads = "ExcelUploads";
            public const string UploadOCRFile = "uploadOCRFile";
            public const string Percentage = "%";
            public const string Dollor = "$";
            public const string BarCodeKey = "BarCodeKey";
            public const string BarCodeKeyName = "BarCodeKeyName";
            public const string BarCodeCHECK = "CHECK";
            public const string ddMMyyyyss = "ddMMyyyyss";
            public const string True = "true";
            public const string Zero = "0";
            public const string UpdateText = "Update";
            public const string ErrorTest = "Error";

            public const char SingleQouteDoubleSlash = '\\';
            public const char SingleQouteDot = '.';
            public const string AfterInvoiceDate = "AfterInvoiceDate";

            

        }

        public struct Extension
        {
            public const string PDF = ".pdf";
        }
    }
}
