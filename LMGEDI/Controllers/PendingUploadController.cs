using System;
using System.Linq;
using System.Web.Mvc;
using LMGEDI.Core.BL;
using LMGEDIApp.Infrastructure.ApplicationServices.Contracts;
using LMGEDIApp.Domain.Models.PendingUploadModel;
using AutoMapper;
using LMGEDIApp.Infrastructure.Global;
using Omu.ValueInjecter;
using LMGEDIApp.Infrastructure.ApplicationFilters;
namespace LMGEDI.Controllers
{
    [AuthorizedUserCheckAttribute]
    public class PendingUploadController : Controller
    {
        private readonly IPendingUpload _pendingUpload;
        private readonly IARCommonServices _arCommonService;
        public PendingUploadController(IPendingUpload pendingUpload, IARCommonServices arCommonService)
        {
            _pendingUpload = pendingUpload;
            _arCommonService = arCommonService;
        }
        public ActionResult Index()
        {
            try
            {
                var getAllViewModel = _pendingUpload.GetAllPendingUploadRecord(GlobalConst.Records.Skip, GlobalConst.Records.LandingTake);
                PendingUploadDetail pendingUploadDetail = new PendingUploadDetail();
                pendingUploadDetail.PendingUploads = getAllViewModel.PendingUploadDetails.Select(PendingUploads => new PendingUploadRecord().InjectFrom(PendingUploads)).Cast<PendingUploadRecord>().ToList();
                pendingUploadDetail.TotalCount = getAllViewModel.TotalCount;
                return View(pendingUploadDetail);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(int skip)
        {
            try
            {
                var getAllViewModel = _pendingUpload.GetAllPendingUploadRecord(skip, GlobalConst.Records.LandingTake);
                PendingUploadDetail pendingUploadDetail = new PendingUploadDetail();
                pendingUploadDetail.PendingUploads = getAllViewModel.PendingUploadDetails.Select(PendingUploads => new PendingUploadRecord().InjectFrom(PendingUploads)).Cast<PendingUploadRecord>().ToList();
                pendingUploadDetail.TotalCount = getAllViewModel.TotalCount;
                return Json(pendingUploadDetail);
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
        [HttpPost]
        public ActionResult UpdatePendingUploadRecord(PendingUpload pendingUpload)
        {
            try
            {
                pendingUpload.DeletedBy = int.Parse(Request.Cookies[GlobalConst.SessionKeys.userDetailMgmt].Values[GlobalConst.SessionKeys.userID]);
                pendingUpload.DeletedOn = DateTime.Now;
                pendingUpload.IsDeleted = true;
                return Json(_pendingUpload.UpdatePendingUploadRecord(Mapper.Map<LMGEDI.Core.Data.Model.PendingUpload>(pendingUpload)));
            }
            catch (Exception ex)
            {
                _arCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                return View();
            }
        }
    }
}