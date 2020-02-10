using LMGEDI.Core.Data;
using System.Linq;
using Omu.ValueInjecter;
using BLModel = LMGEDI.Core.BL.Model;
using DLModel = LMGEDI.Core.Data.Model;
using System.Collections.Generic;
using LMGEDI.Core.BL.Model;
namespace LMGEDI.Core.BL.Implementation
{
    public class CommissionImpl : ICommission
    {
        private readonly ICommissionRepository _commissionRepository;
        private readonly ICommissionPaymentRepository _commissionPaymentRepository;

        public CommissionImpl(ICommissionRepository commissionRepository, ICommissionPaymentRepository commissionPaymentRepository)
        {
            _commissionRepository = commissionRepository;
            _commissionPaymentRepository = commissionPaymentRepository;
        }

        public int UpdateCommissionRecord(DLModel.Commission modelCommission)
        {
            return _commissionRepository.Update((DLModel.Commission)new DLModel.Commission().InjectFrom(modelCommission),(rk=>rk.IsPaymentRecevied));
        }

        public BLModel.Paged.CommissionDetail GetCommissionRecordByLienClientID(int _lienClientID, int _skip, int _take)
        {
            return new BLModel.Paged.CommissionDetail
            {
                CommissionDetails = _commissionRepository.GetCommissionRecordByLienClientID(_lienClientID, _skip, _take).Select(Commission => new BLModel.CommissionSearch().InjectFrom(Commission)).Cast<BLModel.CommissionSearch>().ToList(),
                TotalCount = _commissionRepository.GetCommissionRecordByLienClientIDCount(_lienClientID)
            };

        }

        public IEnumerable<LienClientBilling> GetAllLienClientBilling()
        {
            return _commissionRepository.GetAllLienClientBilling().Select(Commission => new BLModel.LienClientBilling().InjectFrom(Commission)).Cast<BLModel.LienClientBilling>().ToList();
        }

       


        public int AddCommissionPaymentRecord(DLModel.CommissionPayment modelCommissionPayment)
        {
            return _commissionPaymentRepository.Add((DLModel.CommissionPayment)new DLModel.CommissionPayment().InjectFrom(modelCommissionPayment)).CommissionPaymentID;
        }
        public int UpdateCommissionPaymentRecord(DLModel.CommissionPayment modelCommissionPayment)
        {
            return _commissionPaymentRepository.Update((DLModel.CommissionPayment)new DLModel.CommissionPayment().InjectFrom(modelCommissionPayment));
        }
        public BLModel.Paged.CommissionPaymentDetail GetAllCommissionPaymentByCommissionID(int _commissionID, int _quaterID, int _skip, int _take)
        {
            var _res = (from cp in _commissionPaymentRepository.GetDbSet().ToList()
                        where (cp.CommissionID.Equals(_commissionID)) && (cp.InvoiceDateQuarter.Equals(_quaterID))
                        select new { cp.CommissionID, cp.CommissionPaymentID, cp.CPCheckNumber, cp.CPCheckSentOn, cp.CPPaymentPaidAmount, cp.InvoiceDateQuarter, cp.IsPaymentRecevied }).OrderBy(pd => pd.CommissionPaymentID);

            return new BLModel.Paged.CommissionPaymentDetail
            {
                CommissionPaymentDetails = _res.Skip(_skip).Take(_take).Select(depat => new DLModel.CommissionPayment().InjectFrom(depat)).Cast<DLModel.CommissionPayment>().ToList(),
                TotalCount = _res.Count()
            };
        }

        public BLModel.Paged.CommissionDetail GetCommissionDashboardRecord(int _skip, int _take)
        {
            return new BLModel.Paged.CommissionDetail
            {
                CommissionDetails = _commissionRepository.GetCommissionDashboardReaord(_skip, _take).Select(Commission => new BLModel.CommissionSearch().InjectFrom(Commission)).Cast<BLModel.CommissionSearch>().ToList(),
                TotalCount = _commissionRepository.GetCommissionDashboardRecordCount()
            };
        }

    }
}
