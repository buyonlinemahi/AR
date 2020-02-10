using LMGEDIApp.Domain.Models.User;
using System.Linq;
using System.Web.Mvc;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDIApp.Infrastructure.Base;
using LMGEDIApp.Infrastructure.Global;
using Omu.ValueInjecter;
using AutoMapper;
using LMGEDIApp.Infrastructure.ApplicationServices.Contracts;
using LMGEDIApp.Infrastructure.ApplicationFilters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using LMGEDIApp.Domain.Models.ClientCommissionModel;

namespace LMGEDI.Controllers
{
    public class CommissionController : BaseController
    {
        private  ICommission _icommission;      

        public CommissionController(ICommission icommission)
        {
            _icommission = icommission;
            
        }
      
        // GET: User

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClientCommission()
        {
            return View();
        }
        [HttpPost]
        public ActionResult getCommissionDashboardRecord(int? skip, int? take)
        {
            try
            {
                if (skip == 0)
                {
                    CommissionDetail objCommissionDetail = new CommissionDetail();
                    var getFileAllData = _icommission.GetCommissionDashboardRecord(GlobalConst.Records.Skip, GlobalConst.Records.LandingTake25);
                    objCommissionDetail.CommissionDetails = Mapper.Map<IEnumerable<CommissionSearch>>(getFileAllData.CommissionDetails);
                    objCommissionDetail.TotalCount = getFileAllData.TotalCount;
                    return Json(objCommissionDetail, GlobalConst.ContentTypes.TextHtml);
                }
                else
                {
                    CommissionDetail objCommissionDetail = new CommissionDetail();
                    var getFileAllData = _icommission.GetCommissionDashboardRecord(skip.Value, GlobalConst.Records.LandingTake25);
                    objCommissionDetail.CommissionDetails = Mapper.Map<IEnumerable<CommissionSearch>>(getFileAllData.CommissionDetails);
                    objCommissionDetail.TotalCount = getFileAllData.TotalCount;
                    return Json(objCommissionDetail, GlobalConst.ContentTypes.TextHtml);
                }
            }
            catch (Exception ex)
            {
                return Json("");
            }
        }
        [HttpGet]
        public ActionResult getAllLienClientBilling()
        {  
         return Json( _icommission.GetAllLienClientBilling(), "text/html", JsonRequestBehavior.AllowGet);           
        }
        [HttpPost]
        public ActionResult getCommissionRecordByLienClientID(int _lienClientID, int? _skip, int? _take)
        {
            
            try {
                if (_skip == 0)
                {
                    CommissionDetail objCommissionDetail = new CommissionDetail();
                    var getFileAllData = _icommission.GetCommissionRecordByLienClientID(_lienClientID, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake25);
                    objCommissionDetail.CommissionDetails = Mapper.Map<IEnumerable<CommissionSearch>>(getFileAllData.CommissionDetails);
                    objCommissionDetail.TotalCount = getFileAllData.TotalCount;
                    return Json(objCommissionDetail, GlobalConst.ContentTypes.TextHtml);
                }
                else
                {
                    CommissionDetail objCommissionDetail = new CommissionDetail();
                    var getFileAllData = _icommission.GetCommissionRecordByLienClientID(_lienClientID, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake25);
                    objCommissionDetail.CommissionDetails = Mapper.Map<IEnumerable<CommissionSearch>>(getFileAllData.CommissionDetails);
                    objCommissionDetail.TotalCount = getFileAllData.TotalCount;                   
                    return Json(objCommissionDetail, GlobalConst.ContentTypes.TextHtml);
                }
            }
            catch (Exception ex)
            {
                return Json("");
            }
        }

        [HttpPost]
        public ActionResult getAllCommissionPaymentByCommissionID(int _commissionID, int _quaterID, int? _skip, int? _take)
        {
           
                CommissionPaymentDetail objcommissionPaymentDetail = new CommissionPaymentDetail();
                var getCommissionDetails = _icommission.GetAllCommissionPaymentByCommissionID(_commissionID, _quaterID, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake);
                objcommissionPaymentDetail.CommissionPaymentDetails = Mapper.Map<IEnumerable<CommissionPayment>>(getCommissionDetails.CommissionPaymentDetails);
                objcommissionPaymentDetail.TotalCount = getCommissionDetails.TotalCount;
                return Json(objcommissionPaymentDetail, GlobalConst.ContentTypes.TextHtml);
          
        }
        [HttpPost]
        public ActionResult SaveEmployerRecord(CommissionPayment _commissionPayment)
        {   
            try
            {
                if (_commissionPayment.CommissionPaymentID == 0)
                {
                    _icommission.AddCommissionPaymentRecord(Mapper.Map<LMGEDI.Core.Data.Model.CommissionPayment>(_commissionPayment));                   
                    return Json(GlobalConst.alertMessages.SaveMessage, GlobalConst.ContentTypes.TextHtml);
                }
                else {
                  _icommission.UpdateCommissionPaymentRecord(Mapper.Map<LMGEDI.Core.Data.Model.CommissionPayment>(_commissionPayment));                  
                  return Json(GlobalConst.alertMessages.SaveMessage, GlobalConst.ContentTypes.TextHtml);
                }
            }
            catch (Exception e)
            {
                return Json("");
            }
           
        }
    }
}