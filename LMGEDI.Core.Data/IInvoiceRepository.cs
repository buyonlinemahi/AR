using System;
using System.Collections.Generic;
using Core.Base.Data;
using LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.Data
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        IEnumerable<InvoiceDetail> GetAllInvoice(int FileId, int skip, int take);
        int GetAllInvoiceCountByFileId(System.Linq.Expressions.Expression<Func<Invoice, bool>> where);
        IEnumerable<InvoiceICDetail> GetAllInvoiceIC(int FileId);
        int UpdateInvoiceICAmtAccFileIdInvoiceID(InvoiceICDetail objInvoiceICDetail);
        void UpdateAssignedToInvoiceIDByInvoiceID(int InvoiceID, int SubInvoiceID);
        IEnumerable<InvoiceICDetail> GetAssignedInvoicedByInvoiceID(int FileId);
    }
}
