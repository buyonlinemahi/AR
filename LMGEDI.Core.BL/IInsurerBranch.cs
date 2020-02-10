using DLModel = LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;

namespace LMGEDI.Core.BL
{
    public interface IInsurerBranch 
    {
        int AddInsuranceBranchRecord(DLModel.InsurerBranch modelInsuranceBranch);
        int UpdateInsuranceBranchRecord(DLModel.InsurerBranch modelInsuranceBranch);
        BLModel.Paged.InsurerBranchDetail GetAllInsurerBranch(string InsurerBranchName, int skip, int take);
    }
}
