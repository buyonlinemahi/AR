using Core.Base.Data;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;

namespace LMGEDI.Core.Data
{
    public interface ILienTempTableRepository : IBaseRepository<LienTempTable>
    {
        int UpdateLienTempClaimNumberByUploadID(LienTempTable _LienTempTable);
        ErrorLogFile ProcessedLienTempTable();
        IEnumerable<LienTempTableRecords> GetAllLienTempRecords(int skip, int take);
    }
}
