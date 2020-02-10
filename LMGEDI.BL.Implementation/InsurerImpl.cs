using LMGEDI.Core.Data;
using System.Linq;
using Omu.ValueInjecter;
using DLModel = LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;
namespace LMGEDI.Core.BL.Implementation
{
    public class InsurerImpl : IInsurer
    {
        private readonly IInsurerRepository _insurerRepository;
        public InsurerImpl(IInsurerRepository insurerRepository)
        {
            _insurerRepository = insurerRepository;
        }

        public int AddInsurerRecord(DLModel.Insurer modelInsurance)
        {
            if (_insurerRepository.CheckInsurerExist(modelInsurance.InsurerName) > 0)
                return 0;
            else
                return _insurerRepository.Add((DLModel.Insurer)new DLModel.Insurer().InjectFrom(modelInsurance)).InsurerId;
        }

        public int UpdateInsurerRecord(DLModel.Insurer modelInsurance)
        {
            return _insurerRepository.Update((DLModel.Insurer)new DLModel.Insurer().InjectFrom(modelInsurance));
        }

        public BLModel.Paged.InsurerDetail GetAllInsurerByName(string InsurerName, int Skip, int Take)
        {
            return new BLModel.Paged.InsurerDetail
            {
                InsurerDetails = _insurerRepository.GetAllInsurer(InsurerName, Skip, Take).Select(adjuster => new BLModel.InsurerSearch().InjectFrom(adjuster)).Cast<BLModel.InsurerSearch>().ToList(),
                TotalCount = _insurerRepository.GetAllInsurerCount(InsurerName)
            };

        }
    }
}
