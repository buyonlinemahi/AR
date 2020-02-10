using System;
using System.Collections.Generic;
using Core.Base.Data;
using LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.Data
{
    public interface IInsurerBranchRepository : IBaseRepository<InsurerBranch>
    {
        IEnumerable<InsurerBranchSearch> GetAllInsurerBranch(string InsurerBranchName, int skip, int take);
        int GetAllInsurerBranchCount(string InsurerBranchName);
        int GetAllInsurerBranchCountById(System.Linq.Expressions.Expression<Func<InsurerBranch, bool>> where);
        IEnumerable<InsurerBranchSearch> GetAllInsurerBranchByInsurerID(int InsurerId, string InsurerBranchName, char DBName, int skip, int take);
        int CheckInsurerBranchExist(string InsurerBranchName);
         
    }
}
