using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;
using System.Data.SqlClient;


namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class InvoiceAdjustmentRepository : BaseRepository<InvoiceAdjustment, LMGEDIDBContext>, IInvoiceAdjustmentRepository
    {
        public InvoiceAdjustmentRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory), contextFactory)
        {
            
        }
        public int UpdateInvoiceAdjuster(int InvoiceID, string AdjustmentNotes, int CreatedBy, decimal AdjustmentAmt)
        {
            SqlParameter _InvoiceID = new SqlParameter("@InvoiceID", InvoiceID);
            SqlParameter _AdjustmentNotes = new SqlParameter("@AdjustmentNotes", AdjustmentNotes == null ? string.Empty : AdjustmentNotes);
            SqlParameter _CreatedBy = new SqlParameter("@CreatedBy", CreatedBy);
            SqlParameter _AdjustmentAmt = new SqlParameter("@AdjustmentAmt", AdjustmentAmt);
            return Context.Database.ExecuteSqlCommand(Constant.StoredProcedureConst.InvoiceRepositoryProcedure.UpdateInvoiceAdjustmentByInvoiceID, _InvoiceID, _AdjustmentNotes, _CreatedBy, _AdjustmentAmt);

        }
    }
}
