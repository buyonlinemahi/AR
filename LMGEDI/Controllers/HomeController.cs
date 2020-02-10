using System.Linq;
using System.Web.Mvc;
using LMGEDI.Core.BL;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using LMGEDI.Core.BL.Implementation;
using LMGEDIApp.Infrastructure.Global;
using BaseProject = Core.Base.Data;
using LMGEDIApp.Domain.Models.Patient;
using Omu.ValueInjecter;
using LMGEDIApp.Infrastructure.ApplicationFilters;

namespace LMGEDI.Controllers
{
   [AuthorizedUserCheckAttribute]
    public class HomeController : Controller
    {
        private IPatientRepository _patientRepository;
        private IPatient _patientImplBL;
        public HomeController()
        {
            _patientRepository = new PatientRepository(new BaseProject.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _patientImplBL = new PatientImpl(_patientRepository);
        }

        // GET: Home
        public ActionResult Index()
        {
            PatientViewModel patientViewModel = new PatientViewModel();
            var getAllPatient = _patientImplBL.GetAllPatient(GlobalConst.Records.Skip, GlobalConst.Records.LandingTake);
            patientViewModel.PatientSearchResult = getAllPatient.PatientDetails.Select(patientSearchResult => new PatientSearchResult().InjectFrom(patientSearchResult)).Cast<PatientSearchResult>().ToList();
            patientViewModel.PatientCount = getAllPatient.TotalCount;
            return View(patientViewModel);
           
        }
        [HttpPost]
        public ActionResult Index(int Skip)
        {
          PatientViewModel patientViewModel = new PatientViewModel();
          var getAllPatient = _patientImplBL.GetAllPatient(Skip, GlobalConst.Records.LandingTake);
          patientViewModel.PatientSearchResult = getAllPatient.PatientDetails.Select(patientSearchResult => new PatientSearchResult().InjectFrom(patientSearchResult)).Cast<PatientSearchResult>().ToList();
          patientViewModel.PatientCount = getAllPatient.TotalCount;
          return Json(patientViewModel,GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult GetAllPatientByName(string PatientName, int Skip)
        {
            PatientViewModel patientViewModel = new PatientViewModel();
            var getAllPatient = _patientImplBL.GetAllPatientByName(PatientName, Skip, GlobalConst.Records.LandingTake);
            patientViewModel.PatientSearchResult = getAllPatient.PatientDetails.Select(patientSearchResult => new PatientSearchResult().InjectFrom(patientSearchResult)).Cast<PatientSearchResult>().ToList();
            patientViewModel.PatientCount = getAllPatient.TotalCount;    
            return Json(patientViewModel, GlobalConst.ContentTypes.TextHtml);
        }
    }
}