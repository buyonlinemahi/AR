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
    public class InvoiceRepository : BaseRepository<Invoice,LMGEDIDBContext>,IInvoiceRepository
    {
        public InvoiceRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory), contextFactory)
        {
        }

        public IEnumerable<InvoiceDetail> GetAllInvoice(int FileId, int skip, int take)
        {
            SqlParameter _FileId = new SqlParameter("@FileId", FileId);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return Context.Database.SqlQuery<InvoiceDetail>(Constant.StoredProcedureConst.InvoiceRepositoryProcedure.GetAllInvoice, _FileId, _skip, _take);
        }
        
        public int GetAllInvoiceCountByFileId(System.Linq.Expressions.Expression<Func<Invoice, bool>> where)
        {
            return dbset.Where(where).Count();
        }

        public IEnumerable<InvoiceICDetail> GetAllInvoiceIC(int FileId)
        {
            SqlParameter _FileID = new SqlParameter("@FileId", FileId);
            return Context.Database.SqlQuery<InvoiceICDetail>(Constant.StoredProcedureConst.InvoiceRepositoryProcedure.GetAllInvoiceIC, _FileID);
        }

        public int UpdateInvoiceICAmtAccFileIdInvoiceID(InvoiceICDetail objInvoiceICDetail)
        {
            SqlParameter _FileID = new SqlParameter("@FileId", objInvoiceICDetail.FileID);
            SqlParameter _InvoiceID = new SqlParameter("@InvoiceID", objInvoiceICDetail.InvoiceID);
            SqlParameter _InvoiceICAmt = new SqlParameter("@InvoiceICAmt", objInvoiceICDetail.InvoiceICAmt);
            return Context.Database.ExecuteSqlCommand(Constant.StoredProcedureConst.InvoiceRepositoryProcedure.UpdateICAmtAccFileIdInvID, _FileID, _InvoiceID, _InvoiceICAmt);
        }

        public void UpdateAssignedToInvoiceIDByInvoiceID(int InvoiceID, int SubInvoiceID)
        {
            SqlParameter _InvoiceID = new SqlParameter("@InvoiceID", InvoiceID);
            SqlParameter _SubInvoiceID = new SqlParameter("@SubInvoiceID", SubInvoiceID);
            Context.Database.ExecuteSqlCommand(Constant.StoredProcedureConst.InvoiceRepositoryProcedure.UpdateAssignedToInvoiceIDByInvoiceID, _InvoiceID, _SubInvoiceID);
        }

        public IEnumerable<InvoiceICDetail> GetAssignedInvoicedByInvoiceID(int InvoiceID)
        {
            SqlParameter _InvoiceID = new SqlParameter("@InvoiceID", InvoiceID);
            return Context.Database.SqlQuery<InvoiceICDetail>(Constant.StoredProcedureConst.InvoiceRepositoryProcedure.GetAssignedInvoicedByInvoiceID, _InvoiceID);
        }
    }
}
