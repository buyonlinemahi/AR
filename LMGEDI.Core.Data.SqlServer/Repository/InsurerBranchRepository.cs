using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class InsurerBranchRepository : BaseRepository<InsurerBranch,LMGEDIDBContext>,IInsurerBranchRepository
    {
        public InsurerBranchRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory),contextFactory)
        {
        }

         public IEnumerable<InsurerBranchSearch> GetAllInsurerBranch(string InsurerBranchName, int skip, int take)
         {
             SqlParameter _InsurerBranchName = new SqlParameter("@InsurerBranchName", InsurerBranchName);
             SqlParameter _skip = new SqlParameter("@Skip", skip);
             SqlParameter _take = new SqlParameter("@Take", take);
             return Context.Database.SqlQuery<InsurerBranchSearch>(Constant.StoredProcedureConst.InsurerBranchRepositoryProcedure.GetAllInsurerBranch, _InsurerBranchName, _skip, _take);
         }

        public int GetAllInsurerBranchCount(string InsurerBranchName)
         {
             SqlParameter _InsurerBranchName = new SqlParameter("@InsurerBranchName", InsurerBranchName);
             return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.InsurerBranchRepositoryProcedure.GetAllInsurerBranchCount, _InsurerBranchName).SingleOrDefault();
         }
        public int GetAllInsurerBranchCountById(System.Linq.Expressions.Expression<Func<InsurerBranch, bool>> where)
        {
            return dbset.Where(where).Count();
        }
        public IEnumerable<InsurerBranchSearch> GetAllInsurerBranchByInsurerID(int InsurerId, string InsurerBranchName, char DBName, int skip, int take)
        {
            SqlParameter _InsurerId = new SqlParameter("@InsurerId", InsurerId);
            SqlParameter _InsurerBranchName = new SqlParameter("@InsurerBranchName", InsurerBranchName);
            SqlParameter _DBName = new SqlParameter("@DBName", DBName);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return Context.Database.SqlQuery<InsurerBranchSearch>(Constant.StoredProcedureConst.InsurerBranchRepositoryProcedure.GetAllInsurerBranchByInsurerID, _InsurerId, _InsurerBranchName, _DBName, _skip, _take);
        }

        public int CheckInsurerBranchExist(string InsurerBranchName)
        {
            SqlParameter _InsurerBranchName = new SqlParameter("@InsurerBranchName", InsurerBranchName);
            return Context.Database.SqlQuery<int>(Constant.StoredProcedureConst.InsurerBranchRepositoryProcedure.CheckInsurerBranchExist, _InsurerBranchName).SingleOrDefault();
        }
    }
}
