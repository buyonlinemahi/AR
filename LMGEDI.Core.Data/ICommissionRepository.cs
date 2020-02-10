using System.Collections.Generic;
using Core.Base.Data;
using LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.Data
{
    public interface ICommissionRepository : IBaseRepository<Commission>
    {
        IEnumerable<CommissionSearch> GetCommissionRecordByLienClientID(int _lienClientID, int _skip, int _take);
        int GetCommissionRecordByLienClientIDCount(int _lienClientID);

        IEnumerable<LienClientBilling> GetAllLienClientBilling();
        IEnumerable<CommissionSearch> GetCommissionDashboardReaord(int skip, int take);
        int GetCommissionDashboardRecordCount();
    }
}
