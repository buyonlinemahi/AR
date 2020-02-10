using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMGEDIApp.Infrastructure.ApplicationServices.Contracts;
using DomainModelExcel = LMGEDIApp.Domain.Models.ExcelUploadDomain;
using LMGEDIApp.Infrastructure.Global;
using LMGEDI.Core.BL;
using AutoMapper;
using LMGEDIApp.Domain.Models.ExcelUploadDomain;
using LMGEDIApp.Domain.Models.LienTempTableModel;
using LMGEDIApp.Domain.Models.PendingUploadModel;
using LMGEDIApp.Infrastructure.ApplicationFilters;
using System.Threading.Tasks;
using System.Globalization;
using System.Web.Configuration;

namespace LMGEDI.Controllers
{

    [AuthorizedUserCheckAttribute]
    public class ExportExcelToDatabaseController : Controller
    {
        private IDepartment _department;
        private IFile _file;
        private IPendingUpload _pendingUpload;
        private ILienTempTable _lienTempTable;
        private IStorageServices _excelUploadService;
        private IARCommonServices _arCommonService;


        public ExportExcelToDatabaseController(IDepartment department,
            IFile file,
            IPendingUpload pendingUpload,
            ILienTempTable lienTempTable,
            IStorageServices excelUploadService,
            IARCommonServices arCommonService)
        {
            _department = department;
            _file = file;
            _pendingUpload = pendingUpload;
            _lienTempTable = lienTempTable;
            _excelUploadService = excelUploadService;
            _arCommonService = arCommonService;
        }

        public ActionResult Index()
        {
            return View();
        }
      
        [HttpGet]
        public ActionResult ImportExcel()
        {
            return View();
        }

        /// <summary>
        /// This Method is used for AR Module Data to uploading excel field into Database temp table
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ImportExcel(HttpPostedFileBase uploadFile, int DepartmentId)
        {
            var guid = GlobalConst.CommonValues.Zero;
            PendingUpload objpendingUpload = new PendingUpload();
            try
            {
                //Upload and save the file
                //For checking purpose 
                HttpPostedFileBase File = Request.Files[GlobalConst.ConstantChar.UploadFile];
                string excelPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath].ToString()), GlobalConst.ConstantChar.ExcelUploads, Path.GetFileNameWithoutExtension(uploadFile.FileName) + Guid.NewGuid().ToString() + Path.GetExtension(uploadFile.FileName));
                _excelUploadService.CreateExcelUploadFolder(Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath].ToString()), GlobalConst.ConstantChar.ExcelUploads);
                File.SaveAs(excelPath);

                objpendingUpload.PendingUploadName = Path.GetFileName(uploadFile.FileName);
                objpendingUpload.UserId = Convert.ToInt32(Request.Cookies[GlobalConst.SessionKeys.userDetailMgmt].Values[GlobalConst.SessionKeys.userID]);
                objpendingUpload.DepartmentId = DepartmentId;
                objpendingUpload.PendingUploadDate = DateTime.Now;
                guid = _pendingUpload.AddPendingUploadRecord(Mapper.Map<LMGEDI.Core.Data.Model.PendingUpload>(objpendingUpload));  //for adding Row to Pending Upload table return  id
                objpendingUpload.PendingUploadId = guid;
                objpendingUpload.IsDeleted = false;

                DataTable importedData = ImportExcelSheet(excelPath, guid, DepartmentId);

                if (importedData.Rows.Count > GlobalConst.CommonValues.Zero)
                {
                    //Read the Header From Excel 
                    List<DomainModelExcel.ExcelHeader> listColumn = ImportExcelSheetHeader(excelPath);
                    IEnumerable<ExcelHeader> modelExcelHeaderData = listColumn;

                    //This is use for fetching  Temp Data table
                   
                      var  ColumnTableName = (objpendingUpload.DepartmentId == GlobalConst.CommonValues.Five ? _file.GetAllColumnNameFromTblUploadTempsContractor() : _file.GetAllColumnNameFromTblUploadTemps());
                  


                    //Add  Database Column Header into Model
                    IEnumerable<TempExcelDataColumns> modelTempExcelData = Mapper.Map<IEnumerable<TempExcelDataColumns>>(ColumnTableName);


                    // Insert Into Database with SQLBulkCopy
                    string consString = ConfigurationManager.ConnectionStrings[GlobalConst.Configuration.ContextClass].ConnectionString;
                    using (SqlConnection con = new SqlConnection(consString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            //Set the database table name
                            if (objpendingUpload.DepartmentId == GlobalConst.CommonValues.Five)
                                sqlBulkCopy.DestinationTableName = GlobalConst.ExcelTempTables.ExcelUploadContractorTables;
                            else
                                sqlBulkCopy.DestinationTableName = GlobalConst.ExcelTempTables.ExcelUploadTables;

                            //[OPTIONAL]: Map the Excel columns with that of the database table
                            Dictionary<string, string> mapColumns = MakeMappingColumns(modelExcelHeaderData, modelTempExcelData);

                            foreach (var mapping in mapColumns)
                            {
                                sqlBulkCopy.ColumnMappings.Add(mapping.Key, mapping.Value);
                            }
                            con.Open();
                            sqlBulkCopy.BatchSize = GlobalConst.CommonValues.SQLBatchSize;
                            sqlBulkCopy.BulkCopyTimeout = GlobalConst.CommonValues.SQLTimeOut;
                            sqlBulkCopy.WriteToServer(importedData);
                            con.Close();
                        }
                    }
                    if ((System.IO.File.Exists(excelPath)))
                        System.IO.File.Delete(excelPath);
                }
                else
                {
                    objpendingUpload.PendingUploadId = guid;
                    objpendingUpload.IsDeleted = true;
                }    
            }
            catch(Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                objpendingUpload.PendingUploadId = guid;
                objpendingUpload.IsDeleted = true;
            }
            return Json(objpendingUpload, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult ValidateExcelTempDataImport(int Department,int pendingUploadId)
        {
            try
            {
                var validate = _file.ALLValidateDataFromLienAndAR(Department, pendingUploadId, Convert.ToInt32(Request.Cookies[GlobalConst.SessionKeys.userDetailMgmt].Values[GlobalConst.SessionKeys.userID]));
                if (validate.ErrorNumberLoop == null)
                    return Json(GlobalConst.CommonValues.Complete);
                else
                {
                    string Message = validate.ErrorMessage;
                    string StackTrace = "Error Occur at UploadedID = " + validate.ErrorNumberLoop + Environment.NewLine;
                    StackTrace += "Error Occur at Stored Procedure [" + validate.ErrorProcedure + "] Line = " + validate.ErrorLine;
                    _arCommonService.CreateErrorLog(Message, StackTrace);
                    return Json(GlobalConst.ObjectTypes.Error);
                }
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return Json(GlobalConst.ObjectTypes.Error);
            }
        }

        [HttpGet]
        public ActionResult GetFileAllData()
        {
            try
            {
                LienTempViewModel objLienTempTable = new LienTempViewModel();
                var getFileAllData = _lienTempTable.GetAllLienTempData(GlobalConst.Records.Skip, GlobalConst.Records.LandingTake);
                objLienTempTable.FileUploadViewModelResults = Mapper.Map<IEnumerable<LienTempTable>>(getFileAllData.LienTempTableDetails);
                objLienTempTable.FileCount = getFileAllData.TotalCount;
                return View(GlobalConst.Views.ExportExcelToDatabase.ImportExcel, objLienTempTable);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View(GlobalConst.Views.ExportExcelToDatabase.ImportExcel);
            }
        }
        [HttpPost]
        public ActionResult GetFileAllData(int skip)
        {
            try
            {
                LienTempViewModel objLienTempTable = new LienTempViewModel();
                var getFileAllData = _lienTempTable.GetAllLienTempData(skip, GlobalConst.Records.LandingTake);
                objLienTempTable.FileUploadViewModelResults = Mapper.Map<IEnumerable<LienTempTable>>(getFileAllData.LienTempTableDetails);
                objLienTempTable.FileCount = getFileAllData.TotalCount;
                objLienTempTable.uSkip = skip;
                return Json(objLienTempTable, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return Json(GlobalConst.ObjectTypes.Error);
            }
        }

        [HttpPost]
        public ActionResult DeleteFilesByUploadID(int uploadID)
        {
            try
            {
                _lienTempTable.DeleteLienTempTableByUploadID(uploadID);
                return Json( GlobalConst.alertMessages.DeletedSuccessfully);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return Json(GlobalConst.ObjectTypes.Error);
            }
        }
        public DataTable ImportExcelSheet(string filePath, int guid, int DepartmentId)
        {
            DataTable dtImportData = new DataTable();
            try
            {
                //If csv file have header then "true" else "false"
                bool hasHeader = true;
                using (var pck = new OfficeOpenXml.ExcelPackage())
                {
                    using (var stream = System.IO.File.OpenRead(filePath))
                    {
                        pck.Load(stream);
                    }

                    //replace excel sheet name, by default "Sheet1"
                    var ws = pck.Workbook.Worksheets[GlobalConst.CommonValues.Sheet1];
                    foreach (var rowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        string val = hasHeader ? rowCell.Text : string.Format(GlobalConst.ConstantChar.columnZero, rowCell.Start.Column);
                        dtImportData.Columns.Add(val);
                    }

                    dtImportData.Columns.Add(GlobalConst.CommonValues.InvoiceDept);
                    dtImportData.Columns.Add(GlobalConst.CommonValues.PendingUploadId);

                    var startRow = hasHeader ? 2 : 1;
                    var i = 0;
                    for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        var row = dtImportData.NewRow();

                        Parallel.ForEach(wsRow, cell =>
                        {
                            if (Convert.ToString(cell.Value) != GlobalConst.ConstantChar.Blank)
                                row[cell.Start.Column - 1] = Convert.ToString(cell.Value).Replace(GlobalConst.ConstantChar.Dollor, GlobalConst.ConstantChar.Blank);
                            else
                                row[cell.Start.Column - 1] = GlobalConst.ConstantChar.UpdateText;
                        });
                  
                        dtImportData.Rows.Add(row);
                        dtImportData.Rows[i][GlobalConst.CommonValues.InvoiceDept] = DepartmentId;
                        dtImportData.Rows[i][GlobalConst.CommonValues.PendingUploadId] = guid;
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
            }
            return dtImportData;
        }
       
        private Dictionary<string, string> MakeMappingColumns(IEnumerable<ExcelHeader> modelExcelHeaderData, IEnumerable<TempExcelDataColumns> modelTempExcelData)
        {
            Dictionary<string, string> mappingColumns = new Dictionary<string, string>();
            try
            {
                // This code snippet will club two different models make it one model
                var excelHeaderAndTempExcel = modelExcelHeaderData.Zip(modelTempExcelData, (e, t) => new { modelExcelHeaderData = e, modelTempExcelData = t });
                foreach (var ExportData in excelHeaderAndTempExcel)
                {
                    mappingColumns.Add(ExportData.modelExcelHeaderData.ExcelHeaderName, ExportData.modelTempExcelData.ColumnName);
                }
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
            }
            return mappingColumns;
        }
       
        public List<DomainModelExcel.ExcelHeader> ImportExcelSheetHeader(string filePath)
        {
            List<DomainModelExcel.ExcelHeader> listColumn = new List<DomainModelExcel.ExcelHeader>();
            try
            {
                //If csv file have header then "true" else "false"
                bool hasHeader = true;
                using (var pck = new OfficeOpenXml.ExcelPackage())
                {
                    using (var stream = System.IO.File.OpenRead(filePath))
                    {
                        pck.Load(stream);
                    }
                    //replace excel sheet name, by default "Sheet1"
                    var ws = pck.Workbook.Worksheets[GlobalConst.CommonValues.Sheet1];
                   
                    foreach (var rowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        string val = hasHeader ? rowCell.Text : string.Format(GlobalConst.ConstantChar.columnZero, rowCell.Start.Column);
                        DomainModelExcel.ExcelHeader excelHeaderModel = new DomainModelExcel.ExcelHeader
                        {
                            ExcelHeaderName = val
                        };
                        listColumn.Add(excelHeaderModel);
                    }

                    DomainModelExcel.ExcelHeader inv = new DomainModelExcel.ExcelHeader
                    {
                        ExcelHeaderName = GlobalConst.CommonValues.InvoiceDept
                    };

                    DomainModelExcel.ExcelHeader t = new DomainModelExcel.ExcelHeader
                    {
                        ExcelHeaderName = GlobalConst.CommonValues.PendingUploadId
                    };
                    listColumn.Add(inv);
                    listColumn.Add(t);
                }
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
            }
            return listColumn;
        }

        /// <summary>
        /// To get department list to bind Department dropdown 
        /// </summary>
        /// <returns></returns>
        public ActionResult getDepartment()
        {
            try
            {
                return Json(_department.GetDepartment(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return Json(GlobalConst.ConstantChar.ErrorTest);
            }
        }

        [HttpPost]
        public ActionResult UpdateLienItemTable(IEnumerable<LienTempTable> objLienTempTable)
        {
            try
            {
                var validate = _lienTempTable.UpdateLienTempClaimNumber(Mapper.Map<IEnumerable<LMGEDI.Core.Data.Model.LienTempTable>>(objLienTempTable));
                if (validate.ErrorNumberLoop == null)
                    return Json(GlobalConst.CommonValues.Complete);
                else if (validate.ErrorNumberLoop == 0)
                    return Json(GlobalConst.CommonValues.CompleteUpdateOnly);
                else
                {
                    string Message = validate.ErrorMessage;
                    string StackTrace = "Error Occur at UploadedID = " + validate.ErrorNumberLoop + Environment.NewLine;
                    StackTrace += "Error Occur at Stored Procedure [" + validate.ErrorProcedure + "] Line = " + validate.ErrorLine;
                    _arCommonService.CreateErrorLog(Message, StackTrace);
                    return Json(GlobalConst.ObjectTypes.Error);
                }
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return Json(GlobalConst.ObjectTypes.Error);
            }
        }

        [HttpPost]
        public ActionResult UpdatePendingUploadDeleted(int pendingUploadID)
        {
            try
            {
                PendingUpload objpendingUp = new PendingUpload();
                objpendingUp.PendingUploadId = pendingUploadID;
                objpendingUp.IsDeleted = true;
                objpendingUp.DeletedOn = DateTime.Now;
                objpendingUp.DeletedBy = Convert.ToInt32(Request.Cookies[GlobalConst.SessionKeys.userDetailMgmt].Values[GlobalConst.SessionKeys.userID]);
                //var s = _pendingUpload.UpdatePendingUploadRecord(Mapper.Map<LMGEDI.Core.Data.Model.PendingUpload>(objpendingUp));   //for adding Row to Pending Upload table return  id
                return Json(_pendingUpload.UpdatePendingUploadRecord(Mapper.Map<LMGEDI.Core.Data.Model.PendingUpload>(objpendingUp)));
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return Json(GlobalConst.ConstantChar.ErrorTest);
            }
        }

        [HttpPost]
        public ActionResult PullDataFromLienToAR()
        {
            try
            {
          
                var validate = _file.PullDataFromLienToAR(GlobalConst.CommonValues.One, Convert.ToInt32(Request.Cookies[GlobalConst.SessionKeys.userDetailMgmt].Values[GlobalConst.SessionKeys.userID]), WebConfigurationManager.AppSettings[GlobalConst.ConstantChar.AfterInvoiceDate]);
                if (validate.ErrorNumberLoop == null)
                    return Json(GlobalConst.CommonValues.Complete);
                else
                {
                    string Message = validate.ErrorMessage;
                    string StackTrace = "Error Occur at UploadedID = " + validate.ErrorNumberLoop + Environment.NewLine;
                    StackTrace += "Error Occur at Stored Procedure [" + validate.ErrorProcedure + "] Line = " + validate.ErrorLine;
                    _arCommonService.CreateErrorLog(Message, StackTrace);
                    return Json(GlobalConst.ObjectTypes.Error);
                }
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return Json(GlobalConst.ObjectTypes.Error);
            }
        }
    }
}