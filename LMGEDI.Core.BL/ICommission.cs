using LMGEDI.Core.Data.Model;
using System.Collections.Generic;
using BLModel = LMGEDI.Core.BL.Model;
using DLModel = LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.BL
{
    public interface ICommission
    {
        BLModel.Paged.CommissionDetail GetCommissionRecordByLienClientID(int _lienClientID, int _skip, int _take);
        int UpdateCommissionRecord(DLModel.Commission modelCommission);

        IEnumerable<BLModel.LienClientBilling> GetAllLienClientBilling();
        int AddCommissionPaymentRecord(DLModel.CommissionPayment modelCommissionPayment);
        int UpdateCommissionPaymentRecord(DLModel.CommissionPayment modelCommissionPayment);
        BLModel.Paged.CommissionPaymentDetail GetAllCommissionPaymentByCommissionID(int _commissionID, int _quaterID, int _skip, int _take);

        BLModel.Paged.CommissionDetail GetCommissionDashboardRecord(int _skip, int _take);

    }
}
