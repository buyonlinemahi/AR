using LMGEDIApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bytescout.BarCodeReader;
using System.Web.Configuration;
using LMGEDI.Core.BL;
using LMGEDIApp.Domain.Models.OCRModel;
using LMGEDIApp.Infrastructure.ApplicationServices.Contracts;
using AutoMapper;
using LMGEDIApp.Infrastructure.ApplicationFilters;
using System.Threading.Tasks;
using System.Globalization;

namespace LMGEDI.Controllers
{
    [AuthorizedUserCheckAttribute]
    public class CheckAssignmentController : Controller
    {
        private IOCR _ocr;
        private IStorageServices _storageServices;
        private IARCommonServices _arCommonService;
        // GET: CheckAssignment
        public CheckAssignmentController(IOCR ocr, IStorageServices storageServices, IARCommonServices arCommonService)
        {
            _storageServices = storageServices;
            _arCommonService = arCommonService;
            _ocr = ocr;
        }

        [HttpGet]
        public ActionResult Index()
        {
            OCRFileSearchViewModel ocrFileSearcViewModel = new OCRFileSearchViewModel();
            try
            {
                var ocr = _ocr.GetAllOCRRecords(GlobalConst.ConstantChar.Percentage, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake); // ie <ocr>
                ocrFileSearcViewModel.OCRSearchResult = Mapper.Map<IEnumerable<OCRModel>>(ocr.OCRDetails);
                ocrFileSearcViewModel.OCRCount = ocr.TotalCount;
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
            }
                return View(ocrFileSearcViewModel);
        }

        [HttpPost]
        public ActionResult Index(int skip)
        {
            OCRFileSearchViewModel ocrFileSearcViewModel = new OCRFileSearchViewModel();
            try
            {
                var ocr = _ocr.GetAllOCRRecords(GlobalConst.ConstantChar.Percentage, skip, GlobalConst.Records.LandingTake); // ie <ocr>
                ocrFileSearcViewModel.OCRSearchResult = Mapper.Map<IEnumerable<OCRModel>>(ocr.OCRDetails);
                ocrFileSearcViewModel.OCRCount = ocr.TotalCount;
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
            }
            return Json(ocrFileSearcViewModel, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult UploadOCRFile(HttpPostedFileBase uploadOCRFile)
        {
            string result = GlobalConst.ConstantChar.Blank;
            try
            {
                OCRModel ocrModel = new OCRModel();
                HttpPostedFileBase File = Request.Files[GlobalConst.ConstantChar.UploadOCRFile];
                bool r = _storageServices.CreateOCRUploadFolder(Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath]), GlobalConst.ConstantChar.OCRUploads);
                string OCRPath = Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath].ToString()) + GlobalConst.ConstantChar.DoubleSlash +
                     GlobalConst.ConstantChar.OCRUploads + GlobalConst.ConstantChar.DoubleSlash;

                string OCRFilePath = OCRPath + Path.GetFileName(uploadOCRFile.FileName);
                File.SaveAs(OCRFilePath);
                Reader bc = new Reader();
                bc.RegistrationKey = WebConfigurationManager.AppSettings[GlobalConst.ConstantChar.BarCodeKey];
                bc.RegistrationName = WebConfigurationManager.AppSettings[GlobalConst.ConstantChar.BarCodeKeyName];
                bc.BarcodeTypesToFind.Code39 = true;

                iTextSharp.text.pdf.PdfReader reader = null;
                reader = new iTextSharp.text.pdf.PdfReader(OCRFilePath);
                FoundBarcode[] barcodes = bc.ReadFrom(OCRFilePath).Where(val => val.Value == GlobalConst.ConstantChar.BarCodeCHECK).ToArray();                

                int i = 0;
                for (i = 0; i <= barcodes.Length - 1; i++)
                {
                    string newOCRfileName = Guid.NewGuid().ToString() + GlobalConst.Extension.PDF;
                    string newOCRfilepath = OCRPath + newOCRfileName;
                    string[] barcodename1 = barcodes[i].Value.Split(GlobalConst.ConstantChar.RoundBracketStart);
                    string barcodename = barcodename1[0];
                    int pagenoE = 0;
                    int pagenoS = barcodes[i].Page; //page starting
                    bool flag = false;

                    if (i == barcodes.Length - 1)
                    {
                        pagenoE = reader.NumberOfPages;
                    }
                    else
                    {
                        int num = 1;
                        while (flag == false)
                        {
                            if (i + num < barcodes.Length)
                            {
                                if (barcodename == GlobalConst.ConstantChar.BarCodeCHECK)
                                {
                                    pagenoE = barcodes[i + num].Page;
                                    flag = true;
                                }
                            }
                            else
                            {
                                flag = true;
                                pagenoE = reader.NumberOfPages;
                            }
                            num = num + 1;
                        }
                    }

                    if (barcodename == GlobalConst.ConstantChar.BarCodeCHECK)
                    {
                        ocrModel.OcrFileName = newOCRfileName;
                        ocrModel.CreatedBy = Convert.ToInt32(Request.Cookies[GlobalConst.SessionKeys.userDetailMgmt].Values[GlobalConst.SessionKeys.userID]);
                        ocrModel.CreatedOn = DateTime.Now;
                        _ocr.AddOCRRecord(Mapper.Map<LMGEDI.Core.Data.Model.OCR>(ocrModel));
                        ExtractPage(OCRFilePath, newOCRfilepath, pagenoS + 1, pagenoE - 1);
                        result = GlobalConst.ConstantChar.True;
                    }

                }

                reader.Dispose();
                System.IO.File.Delete(OCRFilePath);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
            }
            return Json(result);
        }

        public void ExtractPage(string sourcePdfPath, string outputPdfPath, int pageNumber, int pageend)
        {

            iTextSharp.text.pdf.PdfReader reader = null;
            iTextSharp.text.Document document = null;
            iTextSharp.text.pdf.PdfCopy pdfCopyProvider = null;
            iTextSharp.text.pdf.PdfImportedPage importedPage = null;

            try
            {
                // Intialize a new PdfReader instance with the contents of the source Pdf file:
                reader = new iTextSharp.text.pdf.PdfReader(sourcePdfPath);

                // Capture the correct size and orientation for the page:
                document = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(pageNumber));

                // Initialize an instance of the PdfCopyClass with the source 
                // document and an output file stream:
                pdfCopyProvider = new iTextSharp.text.pdf.PdfCopy(document,
                    new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

                document.Open();

                // Extract the desired page number:
                if (pageNumber == pageend)
                {
                    importedPage = pdfCopyProvider.GetImportedPage(reader, pageNumber + 1);
                    pdfCopyProvider.AddPage(importedPage);
                }
                else
                {
                    for (int i = pageNumber; i <= pageend; i++)
                    {
                        importedPage = pdfCopyProvider.GetImportedPage(reader, i + 1);
                        pdfCopyProvider.AddPage(importedPage);
                    }
                }

                document.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
            }

        }

        [HttpGet]
        public ActionResult AssignCheck(int id)
        {
            OCRModel ocrModel = new OCRModel();
            try
            {
                ocrModel = Mapper.Map<OCRModel>(_ocr.GetOCRDetailById(id));
                ocrModel.OcrFilePath = Path.Combine(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath].ToString(), GlobalConst.ConstantChar.OCRUploads, ocrModel.OcrFileName);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
            }
                return View(ocrModel);
        }

        [HttpPost]
        public ActionResult SearchFileAssignCheck(string searchFileName)
        {
            OCRInvoiceSearchViewModel objModel = new OCRInvoiceSearchViewModel();
            try
            {
                objModel.OCRFileSearchResult = Mapper.Map<IEnumerable<OCRFileInvoices>>(_ocr.GetOCRFilesInvoiceRecords(searchFileName, GlobalConst.Records.Skip, GlobalConst.Records.Take));
                objModel.OCRFileCount = _ocr.GetOCRFilesInvoiceRecordsCount(searchFileName);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
            }
            return Json(objModel, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult SearchFileAssignCheckSearch(string SearchName, int skip)
        {
            OCRInvoiceSearchViewModel objModel = new OCRInvoiceSearchViewModel();
            try
            {
                objModel.OCRFileSearchResult = Mapper.Map<IEnumerable<OCRFileInvoices>>(_ocr.GetOCRFilesInvoiceRecords(SearchName, skip, GlobalConst.Records.Take));
                objModel.OCRFileCount = _ocr.GetOCRFilesInvoiceRecordsCount(SearchName);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
            }
            return Json(objModel, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult FileAssignedPaymentGrid()
        {
            OCRFileAssignedViewModel objModel = new OCRFileAssignedViewModel();
            try
            {
                objModel.OCRPaymentDetailsResult = Mapper.Map<IEnumerable<OCRPaymentDetails>>(_ocr.GetOCRPaymentFilesRecords(GlobalConst.ConstantChar.Percentage, GlobalConst.Records.Skip, GlobalConst.Records.Take));
                objModel.OCRFileAssignedCount = _ocr.GetOCRPaymentFilesRecordsCount(GlobalConst.ConstantChar.Percentage);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
            }
            return Json(objModel, GlobalConst.ContentTypes.TextHtml);

        }

        [HttpPost]
        public ActionResult FileAssignedPaymentGridSearch(int skip)
        {
            OCRFileAssignedViewModel objModel = new OCRFileAssignedViewModel();
            try
            {
                objModel.OCRPaymentDetailsResult = Mapper.Map<IEnumerable<OCRPaymentDetails>>(_ocr.GetOCRPaymentFilesRecords(GlobalConst.ConstantChar.Percentage, skip, GlobalConst.Records.Take));
                objModel.OCRFileAssignedCount = _ocr.GetOCRPaymentFilesRecordsCount(GlobalConst.ConstantChar.Percentage);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
            }
            return Json(objModel, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult CheckOCRPaymentDetails(int InvoiceID, decimal? PaymentAmount)
        {
            try
            {
                OCRPaymentDetails objOCRPaymentDetails = new OCRPaymentDetails();
                objOCRPaymentDetails.InvoiceID = InvoiceID;
                objOCRPaymentDetails.PaymentAmount = PaymentAmount;
                //0 --  Full payment recevied against Invoice 
                //-1 -- Amount can't be greater then invoice amount
                return Json(_ocr.CheckOCRPaymentDetailsByInvoiceId(Mapper.Map<LMGEDI.Core.Data.Model.OCRPaymentDetails>(objOCRPaymentDetails)));
            }
            catch(Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return Json(GlobalConst.ObjectTypes.Error);
            }
        }

        [HttpPost]
        public ActionResult AddOCRPaymentRecords(IEnumerable<OCRPaymentSave> objOCRPaymentSave)
        {
            try
            {
                var result = 0;
                var ocrID = 0;
                if (objOCRPaymentSave != null)
                {
                    var objOCRpaymentList = objOCRPaymentSave.ToList();
                    foreach (var objocrPaymentSaveModel in objOCRpaymentList)
                    {
                        if (result == 0)
                        {
                            ocrID = objocrPaymentSaveModel.OCRId;
                        }
                        objocrPaymentSaveModel.OCRId = ocrID;
                        objocrPaymentSaveModel.CreatedBy = Convert.ToInt32(Request.Cookies[GlobalConst.SessionKeys.userDetailMgmt].Values[GlobalConst.SessionKeys.userID]);
                        OCRPayment ObjOCRPayment = new OCRPayment();
                        int _PaymentId = _ocr.AddOCRPaymentRecords(Mapper.Map<LMGEDI.Core.Data.Model.OCRPaymentSave>(objocrPaymentSaveModel)).PaymentId;
                        _storageServices.CreatePaymentCheckFolder(objocrPaymentSaveModel.FileID, objocrPaymentSaveModel.InvoiceID, _PaymentId, Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath]));
                        System.IO.File.Copy(Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath].ToString()) + GlobalConst.ConstantChar.DoubleSlash +
                        GlobalConst.ConstantChar.OCRUploads + GlobalConst.ConstantChar.DoubleSlash + objocrPaymentSaveModel.CheckUploadName,
                        Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath]), objocrPaymentSaveModel.FileID.ToString(), objocrPaymentSaveModel.InvoiceID.ToString(), _PaymentId.ToString(), objocrPaymentSaveModel.CheckUploadName), true);

                        if (result == 0)
                        {
                            OCRModel ocrModel = new OCRModel();
                            ocrModel = Mapper.Map<OCRModel>(_ocr.GetOCRDetailById(ocrID));
                            ocrModel.IsOCRPaymentRecevied = true;
                            int OCRUpdated = _ocr.UpdateOCRRecord(Mapper.Map<LMGEDI.Core.Data.Model.OCR>(ocrModel));
                        }
                        result = 1;
                    }
                }
                else
                {
                    result = 2;
                }
                return Json(result);
            }
            catch(Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return Json(GlobalConst.ObjectTypes.Error);
            }
        }

        [HttpPost]
        public ActionResult DeleteOCRFileDetailsById(int OCRID, string Filename)
        {
            var result = _ocr.DeleteOCRFileDetailsById(OCRID, Filename);
            string OCRPath = Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath].ToString()) + GlobalConst.ConstantChar.DoubleSlash +
                    GlobalConst.ConstantChar.OCRUploads + GlobalConst.ConstantChar.DoubleSlash;
            string OCRFilePath = OCRPath + Path.GetFileName(Filename);
            System.IO.File.Delete(OCRFilePath);
            return Json(result);
        }
    }

}