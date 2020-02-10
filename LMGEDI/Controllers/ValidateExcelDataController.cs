using LMGEDIApp.Infrastructure.Global;
using LMGEDIApp.Domain.Models.ValidateExcelData;
using AutoMapper;
using System.Collections.Generic;
using LMGEDI.Core.BL;
using System.Web.Mvc;
using LMGEDIApp.Infrastructure.ApplicationFilters;

namespace LMGEDI.Controllers
{
    [AuthorizedUserCheckAttribute]
    public class ValidateExcelDataController : Controller
    {
        private IPatientBillingValidation _patientBillingValidationBL;

        public ValidateExcelDataController(IPatientBillingValidation patientBillingValidationBL)
        {
            _patientBillingValidationBL = patientBillingValidationBL;
        }
       
        // GET: ValidateExcelData
        public ActionResult Index()
        {
            PatientBillingValidationViewModel patientBillingValidationViewModel = new PatientBillingValidationViewModel();
            var getAllPatientBillingValidation = _patientBillingValidationBL.GetAllFromPatientBillingValidation(GlobalConst.Records.Skip, GlobalConst.Records.LandingTake);
            patientBillingValidationViewModel.PatientBillingValidationResult = Mapper.Map<IEnumerable<PatientBillingResult>>(getAllPatientBillingValidation.PatientBillingValidationDetails);
            patientBillingValidationViewModel.PatientBillingCount = getAllPatientBillingValidation.TotalCount;
            return View(patientBillingValidationViewModel);
        }


        [HttpPost]
        public ActionResult Index(int Skip)
        {
            PatientBillingValidationViewModel patientBillingValidationViewModel = new PatientBillingValidationViewModel();
            var getAllPatientBillingValidation = _patientBillingValidationBL.GetAllFromPatientBillingValidation(Skip, GlobalConst.Records.LandingTake);
            patientBillingValidationViewModel.PatientBillingValidationResult = Mapper.Map<IEnumerable<PatientBillingResult>>(getAllPatientBillingValidation.PatientBillingValidationDetails);
            patientBillingValidationViewModel.PatientBillingCount = getAllPatientBillingValidation.TotalCount;
            return Json(patientBillingValidationViewModel, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult UpdateRowAndValidateData(PatientBillingResult patientBillingResultModel)
        {
            PatientBillingValidation _patientBillingValidationModel = new PatientBillingValidation();
            _patientBillingValidationModel.BillingID = patientBillingResultModel.BillingID;
            _patientBillingValidationModel.PatientFName = patientBillingResultModel.PatientFName;
            _patientBillingValidationModel.PatientSSN = patientBillingResultModel.PatientSSN;
            _patientBillingValidationModel.BillingInsurance = patientBillingResultModel.BillingInsurance;
            var Result = _patientBillingValidationBL.UpdateValidateDataFromBillingValidation(Mapper.Map<LMGEDI.Core.Data.Model.PatientBillingValidation>(_patientBillingValidationModel));
            return Json(Result, GlobalConst.ContentTypes.TextHtml);
        }
    }
}