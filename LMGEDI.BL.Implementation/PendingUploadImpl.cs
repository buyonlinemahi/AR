using System.Linq;
using LMGEDI.Core.Data;
using Omu.ValueInjecter;

using DLModel = LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;

namespace LMGEDI.Core.BL.Implementation
{
    public class PendingUploadImpl : IPendingUpload
    {
        private readonly IPendingUploadRepository _pendingUploadRepository;
        private readonly IUserRepository _userRepository;

        public PendingUploadImpl(IPendingUploadRepository pendingUploadRepository, IUserRepository userRepository)
        {
            _pendingUploadRepository = pendingUploadRepository;
            _userRepository = userRepository;
        }
        public int AddPendingUploadRecord(DLModel.PendingUpload modelPendingUpload)
        {
            return _pendingUploadRepository.Add((DLModel.PendingUpload)new DLModel.PendingUpload().InjectFrom(modelPendingUpload)).PendingUploadId;
        }
        public int UpdatePendingUploadRecord(DLModel.PendingUpload modelPendingUpload)
        {
            return _pendingUploadRepository.Update((DLModel.PendingUpload)new DLModel.PendingUpload().InjectFrom(modelPendingUpload), p => p.PendingUploadId,
                p => p.IsDeleted, p => p.DeletedOn, p => p.DeletedBy);
                                     
        }
        public BLModel.Paged.PendingUploadDetail GetAllPendingUploadRecord(int Skip, int Take)
        {

            var _PendingUploadDetails = (from pd in _pendingUploadRepository.GetDbSet().ToList()
                                        join ur in _userRepository.GetDbSet().ToList()
                                        on pd.UserId equals ur.UserID
                                        where (pd.IsDeleted.Equals(null) || pd.IsDeleted == false) && (pd.IsProcessed.Equals(null)|| pd.IsProcessed == false)
                                        select new { pd.PendingUploadId, pd.PendingUploadName, pd.PendingUploadDate, pd.UserId, pd.DepartmentId, pd.DeletedBy, pd.DeletedOn, ur.Username }).OrderBy(pd => pd.PendingUploadId);

            return new BLModel.Paged.PendingUploadDetail
            {
                PendingUploadDetails = _PendingUploadDetails.Skip(Skip).Take(Take).Select(depat => new DLModel.PendingUploadRecord().InjectFrom(depat)).Cast<DLModel.PendingUploadRecord>().ToList(),
                TotalCount = _PendingUploadDetails.Count()
            };
        }
    }
}
