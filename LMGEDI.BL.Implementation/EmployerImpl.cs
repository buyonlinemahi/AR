using LMGEDI.Core.Data;
using System.Linq;
using Omu.ValueInjecter;
using BLModel = LMGEDI.Core.BL.Model;
using DLModel = LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.BL.Implementation
{
    public class EmployerImpl : IEmployer
    {
        private readonly IEmployerRepository _employerRepository;
        public EmployerImpl(IEmployerRepository employerRepository)
        {
            _employerRepository = employerRepository;
        }

        public int AddEmployerRecord(DLModel.Employer modelEmployer)
        {
            int AllreadyExists = _employerRepository.CheckEmployerExist(modelEmployer.EmployerName);
            if (AllreadyExists > 0)
                return 0;
            else
                return _employerRepository.Add((DLModel.Employer)new DLModel.Employer().InjectFrom(modelEmployer)).EmployerId;
        }

        public int UpdateEmployerRecord(DLModel.Employer modelEmployer)
        {
            if (_employerRepository.GetAll(emp => emp.EmployerId != modelEmployer.EmployerId && emp.EmployerName.Equals(modelEmployer.EmployerName)).Count() > 0)
                return 0;
            else
                return _employerRepository.Update((DLModel.Employer)new DLModel.Employer().InjectFrom(modelEmployer));
        }

        public BLModel.Paged.EmployerDetail GetAllEmployerByEmployerName(string EmployerName, int Skip, int Take)
        {
            return new BLModel.Paged.EmployerDetail
            {
                EmployerDetails = _employerRepository.GetAllEmployer(EmployerName, Skip, Take).Select(adjuster => new BLModel.EmployerSearch().InjectFrom(adjuster)).Cast<BLModel.EmployerSearch>().ToList(),
                TotalCount = _employerRepository.GetAllEmployerCount(EmployerName)
            };

        }
    }
}
