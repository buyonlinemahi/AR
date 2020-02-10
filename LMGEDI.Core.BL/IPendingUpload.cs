using DLModel = LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;

namespace LMGEDI.Core.BL
{
    public interface IPendingUpload
    {
        int AddPendingUploadRecord(DLModel.PendingUpload modelPendingUpload);
        int UpdatePendingUploadRecord(DLModel.PendingUpload modelPendingUpload);
        BLModel.Paged.PendingUploadDetail GetAllPendingUploadRecord(int skip, int take);
    }
}
