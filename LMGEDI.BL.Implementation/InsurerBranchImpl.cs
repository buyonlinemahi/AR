using LMGEDI.Core.Data;
using System.Linq;
using Omu.ValueInjecter;
using DLModel = LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;
namespace LMGEDI.Core.BL.Implementation
{
    public class InsurerBranchImpl : IInsurerBranch
    {
        private readonly IInsurerBranchRepository _insurerBranchRepository;
        public InsurerBranchImpl(IInsurerBranchRepository insurerBranchRepository)
        {
            _insurerBranchRepository = insurerBranchRepository;
        }

        public int AddInsuranceBranchRecord(DLModel.InsurerBranch modelInsuranceBranch)
        {
            if (_insurerBranchRepository.CheckInsurerBranchExist(modelInsuranceBranch.InsurerBranchName) > 0)
                return 0;
            else
                return _insurerBranchRepository.Add((DLModel.InsurerBranch)new DLModel.InsurerBranch().InjectFrom(modelInsuranceBranch)).InsurerBranchId;
        }

        public int UpdateInsuranceBranchRecord(DLModel.InsurerBranch modelInsuranceBranch)
        {
            return _insurerBranchRepository.Update((DLModel.InsurerBranch)new DLModel.InsurerBranch().InjectFrom(modelInsuranceBranch));
        }

        public BLModel.Paged.InsurerBranchDetail GetAllInsurerBranch(string InsurerBranchName, int skip, int take)
        {
            return new BLModel.Paged.InsurerBranchDetail
            {
                InsurerBranchDetails = _insurerBranchRepository.GetAllInsurerBranch(InsurerBranchName, skip, take).Select(insurerBranch => new BLModel.InsurerBranchSearch().InjectFrom(insurerBranch)).Cast<BLModel.InsurerBranchSearch>().ToList(),
                TotalCount = _insurerBranchRepository.GetAllInsurerBranchCount(InsurerBranchName)
            };
        }
    }
}
