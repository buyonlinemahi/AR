using System.Linq;
using System.Web.Mvc;
using LMGEDI.Core.BL;
using LMGEDIApp.Infrastructure.Global;
using LMGEDIApp.Domain.Models.FileModel;
using LMGEDIApp.Domain.Models.AdjusterModel;
using LMGEDIApp.Domain.Models.EmployerModel;
using LMGEDIApp.Domain.Models.InsurerModel;
using LMGEDIApp.Domain.Models.InsurerBranchModel;
using Omu.ValueInjecter;
using AutoMapper;
using System;
using System.Collections.Generic;
using LMGEDIApp.Domain.Models.InvoiceModel;
using LMGEDIApp.Domain.Models.DepartmentModel;
using LMGEDIApp.Domain.Models.StateModel;
using LMGEDIApp.Domain.Models.PaymentModel;
using LMGEDIApp.Infrastructure.ApplicationServices.Contracts;
using System.Configuration;
using System.IO;
using LMGEDIApp.Infrastructure.ApplicationFilters;
using LMGEDIApp.Domain.Models.InvoiceNoteModel;
using LMGEDIApp.Domain.Models.InvoiceAdjustmentModel;
using iTextSharp.text;
using System.Collections;

namespace LMGEDI.Controllers
{
    [AuthorizedUserCheckAttribute]
    public class FileController : Controller
    {
        private IFile _fileBL;
        private IInvoiceNote _invoiceNote;
        private IAdjuster _adjusterBL;
        private IEmployer _employerBL;
        private IInsurer _insurerBL;
        private IInsurerBranch _insurerBranchBL;
        private IDepartment _department;
        private IInvoice _invoice;
        private IPayment _payment;
        private IPaymentRefund _paymentRefund;
        private IStorageServices _storageServices;
        private IARCommonServices _arCommonService;
        private IState _stateBL;
        public FileController(IFile fileBL, IAdjuster adjusterBL, IEmployer employerBL, IInsurer insurerBL, IInsurerBranch insurerBranchBL, IDepartment department, IInvoice invoice, IPayment payment, IStorageServices storageServices
        , IARCommonServices arCommonService, IInvoiceNote invoiceNote, IPaymentRefund paymentRefund, IState state)
        {
            _fileBL = fileBL;
            _adjusterBL = adjusterBL;
            _employerBL = employerBL;
            _insurerBL = insurerBL;
            _insurerBranchBL = insurerBranchBL;
            _department = department;
            _invoice = invoice;
            _payment = payment;
            _storageServices = storageServices;
            _arCommonService = arCommonService;
            _invoiceNote = invoiceNote;
            _paymentRefund = paymentRefund;
            _stateBL = state;
        }
        // GET: File
        [HttpGet]
        public ActionResult Index(int id)
        {
            if (id != int.Parse(GlobalConst.ConstantChar.Zero))
            {
                
                FileDetailViewModel FileDetailViewModel = new FileDetailViewModel();
                try
                {
                    FileDetailViewModel.FileDetail = Mapper.Map<FileDetail>(_fileBL.GetFileDetailByFileId(id));
                    if (FileDetailViewModel.FileDetail != null)
                    {
                        FileDetailViewModel.FileDetail.Departments = Mapper.Map<IEnumerable<Department>>(_department.GetDepartment());
                     
                        FileDetailViewModel.FileDetail.States = Mapper.Map<IEnumerable<State>>(_stateBL.GetState());

                       
                        //else{
                        //    FileDetailViewModel.FileDetail.Departments = Mapper.Map<IEnumerable<Department>>(_department.GetDepartment().Where(x => x.DepartmentId != 5));
                            
                        //}
                        

                        var getAllInvoices = _invoice.GetInvoiceRecordByFileId(FileDetailViewModel.FileDetail.FileID, GlobalConst.Records.Skip, GlobalConst.Records.Take);
                        if (FileDetailViewModel.FileDetail.IsLienAdjusterID)
                        {
                            FileDetailViewModel.FileDetail.AdjusterDetails = Mapper.Map<AdjusterSearch>(_adjusterBL.GetLienAdjusterByAdjusterID(FileDetailViewModel.FileDetail.AdjusterId));
                        }
                        else
                        {
                            FileDetailViewModel.FileDetail.AdjusterDetails = Mapper.Map<AdjusterSearch>(_adjusterBL.GetAdjusterByAdjusterID(FileDetailViewModel.FileDetail.AdjusterId));
                        }
                        InvoiceViewModel InvoiceViewModel = new LMGEDIApp.Domain.Models.InvoiceModel.InvoiceViewModel();
                        InvoiceViewModel.InvoiceDetails = getAllInvoices.InvoiceDetails.Select(InvoiceResult => new Invoice().InjectFrom(InvoiceResult)).Cast<Invoice>().ToList();
                        InvoiceViewModel.InvoiceCount = getAllInvoices.TotalCount;
                        FileDetailViewModel.InvoiceViewModel = InvoiceViewModel;
                        return View(FileDetailViewModel);
                    }
                    else
                        return View();
                }
                catch (Exception ex)
                {
                    _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddFile(LMGEDIApp.Domain.Models.FileModel.File objFile)
        {
            try
            {
                if (objFile.FileID == 0)
                {
                    objFile.DeletedOn = null;
                   
                    int i = _fileBL.AddFileRecord(Mapper.Map<LMGEDI.Core.BL.Model.File>(objFile));
                    if (i > 0)
                    {
                        return Json(i, GlobalConst.ContentTypes.TextHtml);
                    }
                    else
                    {
                        return Json(GlobalConst.alertMessages.DuplicateClaimNo, GlobalConst.ContentTypes.TextHtml);
                    }
                }
                else
                {
                    if (objFile.DepartmentID == 5)
                    {
                       var fileid = _fileBL.UpdateFileRecord(Mapper.Map<LMGEDI.Core.BL.Model.File>(objFile));
                        return Json(GlobalConst.alertMessages.UpdatedSuccessfully, GlobalConst.ContentTypes.TextHtml);                        
                    }
                    else
                    {
                       
                        objFile.DeletedBy = Convert.ToInt32(Request.Cookies[GlobalConst.SessionKeys.userDetailMgmt].Values[GlobalConst.SessionKeys.userID]);
                        objFile.DeletedOn = System.DateTime.Now;

                        if (_fileBL.UpdateFileCheckonClaimNo(Mapper.Map<LMGEDI.Core.Data.Model.File>(objFile)) != GlobalConst.ConstantChar.Zero)
                            return Json(GlobalConst.alertMessages.UpdatedSuccessfully, GlobalConst.ContentTypes.TextHtml);
                        else
                            return Json(GlobalConst.alertMessages.DuplicateInvoice, GlobalConst.ContentTypes.TextHtml);
                    }                    
                }
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddAdjuster(Adjuster objAdjuster)
        {
            try
            {
                objAdjuster.AdjusterId = _adjusterBL.AddAdjusterRecord(Mapper.Map<LMGEDI.Core.Data.Model.Adjuster>(objAdjuster));
                return Json(objAdjuster, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddEmployer(Employer objEmployer)
        {
            try
            {
                objEmployer.EmployerId = _employerBL.AddEmployerRecord(Mapper.Map<LMGEDI.Core.Data.Model.Employer>(objEmployer));
                return Json(objEmployer, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddInsurer(Insurer objInsurer)
        {
            try
            {
                objInsurer.InsurerId = _insurerBL.AddInsurerRecord(Mapper.Map<LMGEDI.Core.Data.Model.Insurer>(objInsurer));
                return Json(objInsurer, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddInsurerBranch(InsurerBranch objInsurerBranch)
        {
            try
            {
                objInsurerBranch.InsurerBranchId = _insurerBranchBL.AddInsuranceBranchRecord(Mapper.Map<LMGEDI.Core.Data.Model.InsurerBranch>(objInsurerBranch));
                return Json(objInsurerBranch, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        /// <summary>
        /// old method for adjuster popup search . . . TG
        /// </summary>
        /// <param name="AdjusterName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult getAdjusterByAdjusterName(string AdjusterName)
        {
            try
            {
                var getAllViewModel = _adjusterBL.GetAllAdjusterByAdjusterName(AdjusterName, GlobalConst.Records.Skip, GlobalConst.Records.FileTake);
                FileViewModel fileviewModel = new FileViewModel();
                fileviewModel.AdjusterDetails = Mapper.Map<IEnumerable<AdjusterSearch>>(getAllViewModel.AdjusterDetails);
                return Json(fileviewModel.AdjusterDetails, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [HttpPost]
        public ActionResult getAdjusterByAdjusterNameSearch(string AdjusterName, int skip)
        {
            try
            {
                var getAllViewModel = _adjusterBL.GetAllAdjusterByAdjusterName(AdjusterName, skip, GlobalConst.Records.FileTake);
                AdjusterSearchViewModel adjusterSearchViewModel = new AdjusterSearchViewModel();
                adjusterSearchViewModel.AdjusterSearch = getAllViewModel.AdjusterDetails.Select(adjusterSearch => new AdjusterSearch().InjectFrom(adjusterSearch)).Cast<AdjusterSearch>().ToList();
                adjusterSearchViewModel.AdjusterCount = getAllViewModel.TotalCount;
                return Json(adjusterSearchViewModel, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        /// <summary>
        /// old method for employer popup search
        /// </summary>
        /// <param name="InsurerName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult getEmployerByEmployerName(string EmployerName)
        {
            try
            {
                var getAllViewModel = _employerBL.GetAllEmployerByEmployerName(EmployerName, GlobalConst.Records.Skip, GlobalConst.Records.FileTake);
                FileViewModel fileviewModel = new FileViewModel();
                fileviewModel.EmployerDetails = Mapper.Map<IEnumerable<EmployerSearch>>(getAllViewModel.EmployerDetails);
                return Json(fileviewModel.EmployerDetails, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [HttpPost]
        public ActionResult getEmployerByEmployerNameSearch(string EmployerName, int skip)
        {
            try
            {
                var getAllViewModel = _employerBL.GetAllEmployerByEmployerName(EmployerName, skip, GlobalConst.Records.FileTake);
                EmployerSearchViewModel employerSearchViewModel = new EmployerSearchViewModel();
                employerSearchViewModel.EmployerSearch = Mapper.Map<IEnumerable<EmployerSearch>>(getAllViewModel.EmployerDetails);
                employerSearchViewModel.EmployerCount = getAllViewModel.TotalCount;
                return Json(employerSearchViewModel, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        /// <summary>
        /// old method for insurer popup search
        /// </summary>
        /// <param name="InsurerName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult getInsurerByInsurerName(string InsurerName)
        {
            try
            {
                var getAllViewModel = _insurerBL.GetAllInsurerByName(InsurerName, GlobalConst.Records.Skip, GlobalConst.Records.FileTake);
                FileViewModel fileviewModel = new FileViewModel();
                fileviewModel.InsurerDetails = Mapper.Map<IEnumerable<InsurerSearch>>(getAllViewModel.InsurerDetails);
                return Json(fileviewModel.InsurerDetails, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [HttpPost]
        public ActionResult getInsurerByInsurerNameSearch(string InsurerName, int skip)
        {
            var getAllViewModel = _insurerBL.GetAllInsurerByName(InsurerName, skip, GlobalConst.Records.FileTake);
            try
            {
                InsurerSearchViewModel insurerSearchViewModel = new InsurerSearchViewModel();
                insurerSearchViewModel.InsurerSearch = Mapper.Map<IEnumerable<InsurerSearch>>(getAllViewModel.InsurerDetails);
                insurerSearchViewModel.InsurerCount = getAllViewModel.TotalCount;
                return Json(insurerSearchViewModel, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }

        [HttpPost]
        public ActionResult UpdateAssignedToInvoiceID(int InvoiceID, int[] SubInvoiceID) 
        {
            try
            {
                foreach (int _subInvoiceID in SubInvoiceID)
                {
                    _invoice.UpdateAssignedToInvoiceIDByInvoiceID(InvoiceID, _subInvoiceID);
                }

                return Json("Done");
            }
            catch(Exception ex)
            {
                return Json(ex.Message);
            }            
        }

        [HttpPost]
        public ActionResult AddInvoice( Invoice invoice,InvoiceAdjustment invoiceAdjustment)
        {
            string path = GlobalConst.ConstantChar.Blank;
            string filename = Guid.NewGuid().ToString() + GlobalConst.Extension.PDF;
            int _InvID;
            var _depID = 0;
            if (invoice.InvoiceID == 0)
            {

                if (invoice.DepartmentId == 5)
                    _depID = GlobalConst.CommonValues.Five;
                else
                    _depID = GlobalConst.CommonValues.One;

                if (invoice.DepartmentId == 5 && invoice.UploadFile != null)
                {
                    filename = Guid.NewGuid().ToString() + GlobalConst.Extension.PDF;
                    invoice.InvoiceFileName = filename;
                    string _dueDateString = _paymentRefund.GetNetworkingDays(Convert.ToString(invoice.InvoiceDate), _depID);
                    invoice.InvoiceDueDate = Convert.ToDateTime(_dueDateString);
                    _InvID = _invoice.AddInvoiceRecord(Mapper.Map<LMGEDI.Core.Data.Model.Invoice>(invoice));
                    path = _storageServices.CreateInvoiceICFolder(_InvID, Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath]));
                    path = path + GlobalConst.ConstantChar.SingleQouteDoubleSlash + filename;
                    invoice.UploadFile.SaveAs(path);
                }
                else
                {
                    invoice.InvoiceDueDate = Convert.ToDateTime(_paymentRefund.GetNetworkingDays(Convert.ToString(invoice.InvoiceDate), _depID));
                    _InvID = _invoice.AddInvoiceRecord(Mapper.Map<LMGEDI.Core.Data.Model.Invoice>(invoice));
                }
                invoiceAdjustment.InvoiceID = _InvID;
                if (_InvID != 0)
                {
                    int InvAdjID = _invoice.AddInvoiceAdjustment(Mapper.Map<LMGEDI.Core.Data.Model.InvoiceAdjustment>(invoiceAdjustment));
                }
                return Json(_InvID, GlobalConst.ContentTypes.TextHtml);

            }
            else
            {
                if (invoice.DepartmentId == 5 && invoice.UploadFile != null)
                {
                    path = _storageServices.CreateInvoiceICFolder(invoice.InvoiceID, Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath]));
                    path = path + GlobalConst.ConstantChar.SingleQouteDoubleSlash + filename;
                    invoice.UploadFile.SaveAs(path);
                    invoice.InvoiceFileName = filename;
                }


                if (invoice.DepartmentId == 5)
                    _depID = GlobalConst.CommonValues.Five;
                else
                    _depID = GlobalConst.CommonValues.One;


                invoice.InvoiceDueDate = Convert.ToDateTime(_paymentRefund.GetNetworkingDays(Convert.ToString(invoice.InvoiceDate), _depID));
                _InvID = _invoice.UpdateInvoiceRecord(Mapper.Map<LMGEDI.Core.Data.Model.Invoice>(invoice));
                invoiceAdjustment.InvoiceID = invoice.InvoiceID;
                int InvAdjID = _invoice.AddInvoiceAdjustment(Mapper.Map<LMGEDI.Core.Data.Model.InvoiceAdjustment>(invoiceAdjustment));

                return Json(invoice.InvoiceID, GlobalConst.ContentTypes.TextHtml);             
            }
        }
        public ActionResult getDepartment(int isIC)
        {
            try
            {
                if (isIC == 1)
                {
                    return Json(_department.GetDepartment().Where(x => x.DepartmentId != 5), JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(_department.GetDepartment(), JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }

        public ActionResult getState()
        {
            try
            {
                return Json(_stateBL.GetState(), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
 
            }
        }


        [HttpGet]
        [AuthorizedUserCheckAttribute]
        public ActionResult FileLanding(string searchFile)
        {
            try
            {
                FileSearchViewModel fileSearchviewmodel = new FileSearchViewModel();
                if (searchFile != null)
                {
                    var getAllFiles = _fileBL.GetFilesBySearch(searchFile, 0, GlobalConst.Records.LandingTake);
                    fileSearchviewmodel.FileSearchResult = getAllFiles.FileDetails.Select(FileSearchResult => new FileSearchResult().InjectFrom(FileSearchResult)).Cast<FileSearchResult>().ToList();
                    fileSearchviewmodel.FileCount = getAllFiles.TotalCount;
                    ViewBag.SearchText = searchFile;
                    return View(fileSearchviewmodel);
                }               
                return View(fileSearchviewmodel);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [HttpPost]
        public ActionResult GetFileBySearch(string searchFile, int Skip)
        {
            try
            {
                if (searchFile == null)
                    searchFile = GlobalConst.ConstantChar.Blank;
                var getAllFiles = _fileBL.GetFilesBySearch(searchFile, Skip, GlobalConst.Records.LandingTake);

                FileSearchViewModel fileSearchviewmodel = new FileSearchViewModel();
                fileSearchviewmodel.FileSearchResult = getAllFiles.FileDetails.Select(FileSearchResult => new FileSearchResult().InjectFrom(FileSearchResult)).Cast<FileSearchResult>().ToList();
                fileSearchviewmodel.FileCount = getAllFiles.TotalCount;
                return Json(fileSearchviewmodel, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [HttpPost]
        public ActionResult FileLanding(int Skip, string searchFile)
        {
            try
            {
                if (searchFile == null)
                    searchFile = GlobalConst.ConstantChar.Blank;
                FileSearchViewModel fileSearchviewmodel = new FileSearchViewModel();
                var getAllFiles = _fileBL.GetFilesBySearch(searchFile, Skip, GlobalConst.Records.LandingTake);
                fileSearchviewmodel.FileSearchResult = getAllFiles.FileDetails.Select(FileSearchResult => new FileSearchResult().InjectFrom(FileSearchResult)).Cast<FileSearchResult>().ToList();
                fileSearchviewmodel.FileCount = getAllFiles.TotalCount;
                return Json(fileSearchviewmodel, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        
        [HttpPost]
        public ActionResult GetAllInsurerBranch(string InsurerBranchName, int skip)
        {
            try
            {
                var getAllViewModel = _insurerBranchBL.GetAllInsurerBranch(InsurerBranchName, skip, GlobalConst.Records.FileTake);
                InsurerBranchSearchViewViewModel insurerBranchSearchViewViewModel = new InsurerBranchSearchViewViewModel();
                insurerBranchSearchViewViewModel.InsurerBranchSearch = Mapper.Map<IEnumerable<InsurerBranchSearch>>(getAllViewModel.InsurerBranchDetails);
                insurerBranchSearchViewViewModel.InsurerBranchCount = getAllViewModel.TotalCount;
                return Json(insurerBranchSearchViewViewModel, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [HttpPost]
        public ActionResult GetPaymentRecordByInvoiceId(int InvoiceId, int Skip, string FileID)
        {
            try
            {
                string path = GlobalConst.ConstantChar.Blank;
                var getAllPayments = _payment.GetPaymentRecordByInvoiceId(InvoiceId, Skip, GlobalConst.Records.Take);
                PaymentViewModel PaymentViewModel = new PaymentViewModel();
                PaymentViewModel.PaymentDetails = getAllPayments.PaymentDetails.Select(PaymentResult => new Payment().InjectFrom(PaymentResult)).Cast<Payment>().ToList();
                foreach (Payment payment in PaymentViewModel.PaymentDetails)
                {
                    path = Path.Combine(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath], FileID, payment.InvoiceId.ToString(), payment.PaymentId.ToString());
                    payment.CheckDownloadPath = path + GlobalConst.ConstantChar.DoubleSlash + payment.CheckUploadName;
                }
                PaymentViewModel.PaymentCount = getAllPayments.TotalCount;
                return Json(PaymentViewModel, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [HttpPost]
        public ActionResult SavePaymentRecord(Payment payment)
        {
            try
            {
                string[] extension = null;
                string path = GlobalConst.ConstantChar.Blank;
                string DownloadPath = GlobalConst.ConstantChar.Blank;
                string filename = GlobalConst.ConstantChar.Blank;
                if (payment.CheckUploadName1 != null)
                {
                    filename = payment.CheckUploadName1.FileName;
                    filename = filename.Substring(filename.LastIndexOf(GlobalConst.ConstantChar.SingleQouteDoubleSlash) + 1);
                    extension = filename.Split(GlobalConst.ConstantChar.SingleQouteDot);
                }
                if (payment.PaymentId == 0)
                {
                    if (extension != null)
                    {
                        payment.CheckUploadName = Guid.NewGuid().ToString() + GlobalConst.Extension.PDF;
                    }
                    payment.PaymentId = _payment.AddPaymentRecord(Mapper.Map<LMGEDI.Core.Data.Model.Payment>(payment));
                    payment.flag = true;
                    if (payment.PaymentId == -1)
                        payment.IsPaymentGreater = true;
                    if (extension != null && payment.PaymentId != 0 && payment.PaymentId != -1)
                    {
                        bool s = _storageServices.CreatePaymentCheckFolder(payment.FileID, payment.InvoiceId, payment.PaymentId, Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath]));
                        path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath]), payment.FileID.ToString(), payment.InvoiceId.ToString(), payment.PaymentId.ToString());
                        DownloadPath = Path.Combine(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath], payment.FileID.ToString(), payment.InvoiceId.ToString(), payment.PaymentId.ToString());
                        payment.CheckUploadName1.SaveAs(path + GlobalConst.ConstantChar.DoubleSlash + payment.CheckUploadName);
                        payment.CheckDownloadPath = DownloadPath + GlobalConst.ConstantChar.DoubleSlash + payment.CheckUploadName;
                    }
                }
                else
                {
                    string filename1 = GlobalConst.ConstantChar.Blank;
                    if (extension != null)
                    {
                        filename1 = Guid.NewGuid().ToString() + GlobalConst.Extension.PDF; //DateTime.Now.ToString(GlobalConst.ConstantChar.ddMMyyyyss) + GlobalConst.ConstantChar.Dot + extension[1];
                    }
                    path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath]), payment.FileID.ToString(), payment.InvoiceId.ToString(), payment.PaymentId.ToString());
                    DownloadPath = Path.Combine(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath], payment.FileID.ToString(), payment.InvoiceId.ToString(), payment.PaymentId.ToString());
                    if (extension != null)
                    {
                        bool s = _storageServices.CreatePaymentCheckFolder(payment.FileID, payment.InvoiceId, payment.PaymentId, Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath]));
                        bool result = System.IO.File.Exists(path + GlobalConst.ConstantChar.DoubleSlash + payment.CheckUploadName);
                        if (result)
                        {
                            System.IO.File.Delete(path + GlobalConst.ConstantChar.DoubleSlash + payment.CheckUploadName);
                        }
                        payment.CheckUploadName = filename1;

                    }
                    if (payment.CheckUploadName1 == null)
                    {
                        if (System.IO.File.Exists(path + GlobalConst.ConstantChar.DoubleSlash + payment.CheckUploadName))
                        {
                            string uploadedfilename = Guid.NewGuid().ToString() + GlobalConst.Extension.PDF;
                            System.IO.File.Move(path + GlobalConst.ConstantChar.DoubleSlash + payment.CheckUploadName, path + GlobalConst.ConstantChar.DoubleSlash + uploadedfilename);
                            payment.CheckUploadName = uploadedfilename;
                        }
                    }
                    int PaymentId = _payment.UpdatePaymentRecord(Mapper.Map<LMGEDI.Core.Data.Model.Payment>(payment));
                    if (PaymentId == -1)
                        payment.IsPaymentGreater = true;

                    if ((PaymentId != -1 && PaymentId != 0) && payment.CheckUploadName1 != null)
                        payment.CheckUploadName1.SaveAs(path + GlobalConst.ConstantChar.DoubleSlash + filename1);

                    payment.flag = false;
                    payment.CheckDownloadPath = DownloadPath + GlobalConst.ConstantChar.DoubleSlash + payment.CheckUploadName;
                }
                payment.CheckUploadName1 = null;
                return Json(payment, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [HttpPost]
        public ActionResult InvoiceDetail(string C, int FileID,int Skip)
        {
            try
            {
                InvoiceViewModel InvoiceViewModel = new LMGEDIApp.Domain.Models.InvoiceModel.InvoiceViewModel();
                if (C != GlobalConst.ConstantChar.Blank && C != null)
                {
                    var getAllInvoices = _invoice.GetInvoiceRecordByFileId(FileID, Skip, GlobalConst.Records.Take);
                    InvoiceViewModel.InvoiceDetails = getAllInvoices.InvoiceDetails.Select(InvoiceResult => new Invoice().InjectFrom(InvoiceResult)).Cast<Invoice>().ToList();
                    InvoiceViewModel.InvoiceCount = getAllInvoices.TotalCount;
                }
                return Json(InvoiceViewModel, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        
        [HttpPost]
        public ActionResult GetInvoiceNotesByInvoiceID(int InvoiceID,int skip)
        {
            var _noteDetails = _invoiceNote.GetInvoiceNoteRecordByInvoiceID(InvoiceID, skip, GlobalConst.Records.Take5);
            InvoiceNoteViewModel _invoiceNoteDetail = new InvoiceNoteViewModel();
            _invoiceNoteDetail.InvoiceNoteDetails = Mapper.Map<IEnumerable<InvoiceNoteDetail>>(_noteDetails.InvoiceNoteDetails);
            _invoiceNoteDetail.TotalCount = _noteDetails.TotalCount;
            return Json(_invoiceNoteDetail);
        }
        [HttpPost]
        public ActionResult saveInvoiceNote(InvoiceNote modelinvoiceNote)
        {
            int i = _invoiceNote.AddInvoiceNoteRecord(Mapper.Map<LMGEDI.Core.Data.Model.InvoiceNote>(modelinvoiceNote));
            return Json(i);
        }
        /// <summary>
        /// This is method is used for saving updating 
        /// Payment Refund ..............Mahi
        /// </summary>
        /// <param name="_paymentRF"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SavePaymentRefundRecord(PaymentRefund _paymentRF)
        {
            try
            {
                string[] extension = null;
                string path = GlobalConst.ConstantChar.Blank;
                string DownloadPath = GlobalConst.ConstantChar.Blank;
                string filename = GlobalConst.ConstantChar.Blank;
                if (_paymentRF.CheckUploadName1 != null)
                {
                    filename = _paymentRF.CheckUploadName1.FileName;
                    filename = filename.Substring(filename.LastIndexOf(GlobalConst.ConstantChar.SingleQouteDoubleSlash) + 1);
                    extension = filename.Split(GlobalConst.ConstantChar.SingleQouteDot);
                }
                if (_paymentRF.PaymentRefundID == 0)
                {
                    if (extension != null)
                    {
                        _paymentRF.CheckUploadName = Guid.NewGuid().ToString() + GlobalConst.Extension.PDF;
                    }
                    _paymentRF.PaymentRefundID = _paymentRefund.AddPaymentRefundRecord(Mapper.Map<LMGEDI.Core.Data.Model.PaymentRefund>(_paymentRF));
                    _paymentRF.flag = true;
                    if (_paymentRF.PaymentRefundID == -1)
                        _paymentRF.IsPaymentGreater = true;
                    if (extension != null && _paymentRF.PaymentRefundID != 0 && _paymentRF.PaymentRefundID != -1)
                    {
                        bool s = _storageServices.CreatePaymentRefundCheckFolder(_paymentRF.FileID, _paymentRF.InvoiceId, _paymentRF.PaymentRefundID, Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath]));
                        path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath]), _paymentRF.FileID.ToString(), _paymentRF.InvoiceId.ToString(), _paymentRF.PaymentRefundID.ToString());
                        DownloadPath = Path.Combine(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath], _paymentRF.FileID.ToString(), _paymentRF.InvoiceId.ToString(), _paymentRF.PaymentRefundID.ToString());
                        _paymentRF.CheckUploadName1.SaveAs(path + GlobalConst.ConstantChar.DoubleSlash + _paymentRF.CheckUploadName);
                        _paymentRF.CheckDownloadPath = DownloadPath + GlobalConst.ConstantChar.DoubleSlash + _paymentRF.CheckUploadName;
                    }
                }
                else
                {
                    string filename1 = GlobalConst.ConstantChar.Blank;
                    if (extension != null)
                    {
                        filename1 = Guid.NewGuid().ToString() + GlobalConst.Extension.PDF; //DateTime.Now.ToString(GlobalConst.ConstantChar.ddMMyyyyss) + GlobalConst.ConstantChar.Dot + extension[1];
                    }
                    path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath]), _paymentRF.FileID.ToString(), _paymentRF.InvoiceId.ToString(), _paymentRF.PaymentRefundID.ToString());
                    DownloadPath = Path.Combine(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath], _paymentRF.FileID.ToString(), _paymentRF.InvoiceId.ToString(), _paymentRF.PaymentRefundID.ToString());
                    if (extension != null)
                    {
                        bool s = _storageServices.CreatePaymentRefundCheckFolder(_paymentRF.FileID, _paymentRF.InvoiceId, _paymentRF.PaymentRefundID, Server.MapPath(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath]));
                        bool result = System.IO.File.Exists(path + GlobalConst.ConstantChar.DoubleSlash + _paymentRF.CheckUploadName);
                        if (result)
                        {
                            System.IO.File.Delete(path + GlobalConst.ConstantChar.DoubleSlash + _paymentRF.CheckUploadName);
                        }
                        _paymentRF.CheckUploadName = filename1;

                    }
                    if (_paymentRF.CheckUploadName1 == null)
                    {
                        if (System.IO.File.Exists(path + GlobalConst.ConstantChar.DoubleSlash + _paymentRF.CheckUploadName))
                        {
                            string uploadedfilename = Guid.NewGuid().ToString() + GlobalConst.Extension.PDF;
                            System.IO.File.Move(path + GlobalConst.ConstantChar.DoubleSlash + _paymentRF.CheckUploadName, path + GlobalConst.ConstantChar.DoubleSlash + uploadedfilename);
                            _paymentRF.CheckUploadName = uploadedfilename;
                        }
                    }
                    int PaymentRefundID = _paymentRefund.UpdatePaymentRefundRecord(Mapper.Map<LMGEDI.Core.Data.Model.PaymentRefund>(_paymentRF));
                    if (PaymentRefundID == -1)
                        _paymentRF.IsPaymentGreater = true;

                    if ((PaymentRefundID != -1 && PaymentRefundID != 0) && _paymentRF.CheckUploadName1 != null)
                        _paymentRF.CheckUploadName1.SaveAs(path + GlobalConst.ConstantChar.DoubleSlash + filename1);

                    _paymentRF.flag = false;
                    _paymentRF.CheckDownloadPath = DownloadPath + GlobalConst.ConstantChar.DoubleSlash + _paymentRF.CheckUploadName;
                }
                _paymentRF.CheckUploadName1 = null;
                return Json(_paymentRF, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }

        [HttpPost]
        public ActionResult GetPaymentRefundRecordByInvoiceId(int InvoiceId, int Skip, string FileID)
        {
            try
            {
                string path = GlobalConst.ConstantChar.Blank;
                var getAllPaymentRefunds = _paymentRefund.GetPaymentRefundRecordByInvoiceId(InvoiceId, Skip, GlobalConst.Records.Take);
                PaymentRefundViewModel _PaymentRefundViewModel = new PaymentRefundViewModel();
                _PaymentRefundViewModel.PaymentRefundDetails = getAllPaymentRefunds.PaymentRefundDetails.Select(PaymentRefundResult => new PaymentRefund().InjectFrom(PaymentRefundResult)).Cast<PaymentRefund>().ToList();
                foreach (PaymentRefund paymentRefund in _PaymentRefundViewModel.PaymentRefundDetails)
                {
                    path = Path.Combine(ConfigurationManager.AppSettings[GlobalConst.ConstantChar.StoragePath], FileID, paymentRefund.InvoiceId.ToString(), paymentRefund.PaymentRefundID.ToString());
                    paymentRefund.CheckDownloadPath = path + GlobalConst.ConstantChar.DoubleSlash + paymentRefund.CheckUploadName;
                }
                _PaymentRefundViewModel.PaymentRefundCount = getAllPaymentRefunds.TotalCount;
                return Json(_PaymentRefundViewModel, GlobalConst.ContentTypes.TextHtml);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }

        public ActionResult InvoiceIC(int id)
        {
            ViewBag.FileID = id;
            if (id != int.Parse(GlobalConst.ConstantChar.Zero))
            {               
                try
                {
                    var getAllInvoices = _invoice.GetInvoiceICRecord(id);
                    InvoiceICViewModel invoiceViewModel = new LMGEDIApp.Domain.Models.InvoiceModel.InvoiceICViewModel();
                    invoiceViewModel.InvoiceICDetails = Mapper.Map<IEnumerable<InvoiceICDetail>>(getAllInvoices);            
                    return View(invoiceViewModel);                   
                }
                catch (Exception ex)
                {
                    _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult UpdateInvoiceICByFileID(string FileInvoiceList,int fileID)
        {
            try
            {
                int FileId = fileID;
                List<InvoiceICDetail> objInvoiceICDetail = new List<InvoiceICDetail>();
                string[] arr = FileInvoiceList.Split(',');
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    string _ia = arr[i];
                    string[] arrFields = _ia.Split('-');
                    InvoiceICDetail objInvoiceIC = new InvoiceICDetail
                    {
                        FileID = Convert.ToInt32(arrFields[0]),
                        InvoiceID = Convert.ToInt32(arrFields[1]),
                        InvoiceICAmt = Convert.ToDecimal(arrFields[2]),
                    };
                    objInvoiceICDetail.Add(objInvoiceIC);
                }
                var validate = _invoice.UpdateInvoiceICAmt(Mapper.Map<IEnumerable<LMGEDI.Core.Data.Model.InvoiceICDetail>>(objInvoiceICDetail));


                var getAllInvoices = _invoice.GetInvoiceICRecord(FileId);
                InvoiceICViewModel invoiceViewModel = new LMGEDIApp.Domain.Models.InvoiceModel.InvoiceICViewModel();
                invoiceViewModel.InvoiceICDetails = Mapper.Map<IEnumerable<InvoiceICDetail>>(getAllInvoices);
                return Json(invoiceViewModel);                   
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }

        public ActionResult GetAssignedInvoices(int InvoiceID)
        {
            var AssignedInvoices = _invoice.GetAssignedInvoicedByInvoiceID(InvoiceID);
            return Json(AssignedInvoices);
        }
        
    }
}