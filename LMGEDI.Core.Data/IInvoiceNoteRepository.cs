using System;
using System.Collections.Generic;
using Core.Base.Data;
using LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.Data
{
    public interface IInvoiceNoteRepository : IBaseRepository<InvoiceNote>
    {
        IEnumerable<InvoiceNoteDetail> GetAllInvoiceNotes(int InvoiceID, int skip, int take);
        int GetAllInvoiceNotesByInvoiceID(System.Linq.Expressions.Expression<Func<InvoiceNote, bool>> where);
    }
}
