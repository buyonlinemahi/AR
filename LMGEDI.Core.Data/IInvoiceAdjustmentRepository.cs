using Core.Base.Data;
using LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.Data
{
    public interface IInvoiceAdjustmentRepository : IBaseRepository<InvoiceAdjustment>
    {
        int UpdateInvoiceAdjuster(int InvoiceID,string AdjustmentNotes,int CreatedBy,decimal AdjustmentAmt);
    }
}
