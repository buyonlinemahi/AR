using System.Web.Mvc;
using LMGEDI.Core.BL;
using LMGEDIApp.Infrastructure.Global;
using LMGEDIApp.Domain.Models.PatientModel;
using LMGEDIApp.Domain.Models.Patient;
using AutoMapper;
using System;
using System.Collections.Generic;
using LMGEDIApp.Infrastructure.ApplicationFilters;

namespace LMGEDI.Controllers
{
    [AuthorizedUserCheckAttribute]
    public class PatientController : Controller
    {
        private IPatient _patientBL;
        private IPatientHistory _patientHistoryBL;
        public PatientController(IPatient patientBL, IPatientHistory patientHistoryBL)
        {
            _patientBL = patientBL;
            _patientHistoryBL = patientHistoryBL;
        }

        // GET: Patient
        public ActionResult Index()
        {
            PatientViewModel patientViewModel = new PatientViewModel();
            var getAllPatient = _patientBL.GetAllPatient(GlobalConst.Records.Skip, GlobalConst.Records.LandingTake);
            patientViewModel.PatientSearchResult = Mapper.Map<IEnumerable<PatientSearchResult>>(getAllPatient.PatientDetails);
            patientViewModel.PatientCount = getAllPatient.TotalCount;
            return View(patientViewModel);

        }
        [HttpPost]
        public ActionResult Index(int Skip)
        {
            PatientViewModel patientViewModel = new PatientViewModel();
            var getAllPatient = _patientBL.GetAllPatient(Skip, GlobalConst.Records.LandingTake);
            patientViewModel.PatientSearchResult = Mapper.Map<IEnumerable<PatientSearchResult>>(getAllPatient.PatientDetails);
            patientViewModel.PatientCount = getAllPatient.TotalCount;
            return Json(patientViewModel, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult GetAllPatientByName(string PatientName, int Skip)
        {
            PatientViewModel patientViewModel = new PatientViewModel();
            var getAllPatient = _patientBL.GetAllPatientByName(PatientName, Skip, GlobalConst.Records.LandingTake);
            patientViewModel.PatientSearchResult = Mapper.Map<IEnumerable<PatientSearchResult>>(getAllPatient.PatientDetails);
            patientViewModel.PatientCount = getAllPatient.TotalCount;
            return Json(patientViewModel, GlobalConst.ContentTypes.TextHtml);
        }

        public ActionResult Detail(int id)
        {

            Patient patientModel = Mapper.Map<Patient>(_patientBL.GetPatientByID(id));
            return View(patientModel);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Patient patient)
        {
            int id = _patientBL.AddPatient(Mapper.Map<LMGEDI.Core.Data.Model.Patient>(patient));
            return Json("Patient Insert Successfully");
        }

        [HttpPost]
        public ActionResult Update(PatientDetailViewModel viewmodel)
        {
            int id = _patientBL.UpdatePatient(Mapper.Map<LMGEDI.Core.Data.Model.Patient>(viewmodel.patient));
            
            if (viewmodel.patientHistory.PatientAccountHistory != viewmodel.patient.PatientAccount)
                viewmodel.patientHistory.Description += "PatientAccount:" + viewmodel.patientHistory.PatientAccountHistory + "||";

            if (viewmodel.patientHistory.PatientFirstHistory != viewmodel.patient.PatientFirst)
                viewmodel.patientHistory.Description += "PatientFirst:" + viewmodel.patientHistory.PatientFirstHistory + "||";

            if (viewmodel.patientHistory.PatientLastHistory != viewmodel.patient.PatientLast)
                viewmodel.patientHistory.Description += "PatientLast:" + viewmodel.patientHistory.PatientLastHistory + "||";

            if (viewmodel.patientHistory.PatientGenderHistory.Trim() != viewmodel.patient.PatientGender)
                viewmodel.patientHistory.Description += "PatientGender:" + viewmodel.patientHistory.PatientGenderHistory.Trim() + "||";

            if (Convert.ToDateTime(viewmodel.patientHistory.PatientDOBHistory).ToString("MM/dd/yyyy")!= viewmodel.patient.PatientDOB)
                viewmodel.patientHistory.Description += "PatientDOB:" + Convert.ToDateTime(viewmodel.patientHistory.PatientDOBHistory).ToString("MM/dd/yyyy") + "||";

            if (viewmodel.patientHistory.PatientClaimHistory != viewmodel.patient.PatientClaim)
                viewmodel.patientHistory.Description += "PatientClaim:" + viewmodel.patientHistory.PatientClaimHistory + "||";

            if (viewmodel.patientHistory.PatientSSNHistory != viewmodel.patient.PatientSSN)
                viewmodel.patientHistory.Description += "PatientSSN:" + viewmodel.patientHistory.PatientSSNHistory + "||";

             if (viewmodel.patientHistory.PatientWCABHistory != viewmodel.patient.PatientWCAB)
                viewmodel.patientHistory.Description += "PatientWCAB:" + viewmodel.patientHistory.PatientWCABHistory + "||";
           
            if (viewmodel.patientHistory.PatientEmployerHistory != viewmodel.patient.PatientEmployer)
                viewmodel.patientHistory.Description += "PatientEmployer:" + viewmodel.patientHistory.PatientEmployerHistory + "||";
           
            if (viewmodel.patientHistory.PatientInsuranceHistory != viewmodel.patient.PatientInsurance)
                viewmodel.patientHistory.Description += "PatientInsurance:" + viewmodel.patientHistory.PatientInsuranceHistory + "||";
            if (viewmodel.patientHistory.Description != null)
            {
                    viewmodel.patientHistory.Description = viewmodel.patientHistory.Description.Substring(0, viewmodel.patientHistory.Description.LastIndexOf("||"));
                    int patientHistoryid = _patientHistoryBL.InsertPatientUpdateHistory(Mapper.Map<LMGEDI.Core.Data.Model.PatientHistory>(viewmodel.patientHistory));
            }

            return Json("Patient Update Successfully");
        }
    }
}