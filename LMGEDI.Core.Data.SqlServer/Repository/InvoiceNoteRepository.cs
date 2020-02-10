using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class InvoiceNoteRepository : BaseRepository<InvoiceNote, LMGEDIDBContext>, IInvoiceNoteRepository
    {
        public InvoiceNoteRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory), contextFactory)
        {
        }
        public IEnumerable<InvoiceNoteDetail> GetAllInvoiceNotes(int InvoiceID, int skip, int take)
        {
            SqlParameter _InvoiceID = new SqlParameter("@InvoiceID", InvoiceID);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return Context.Database.SqlQuery<InvoiceNoteDetail>(Constant.StoredProcedureConst.InvoiceRepositoryProcedure.GetAllInvoiceNotes, _InvoiceID, _skip, _take);
        }
        public int GetAllInvoiceNotesByInvoiceID(System.Linq.Expressions.Expression<Func<InvoiceNote, bool>> where)
        {
            return dbset.Where(where).Count();
        }
    }
}
