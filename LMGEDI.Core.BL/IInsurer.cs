
using BLModel = LMGEDI.Core.BL.Model;
using DLModel = LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.BL
{
    public interface IInsurer
    {
        int AddInsurerRecord(DLModel.Insurer modelInsurance);
        int UpdateInsurerRecord(DLModel.Insurer modelInsurance);
        BLModel.Paged.InsurerDetail GetAllInsurerByName(string InsurerName, int Skip, int Take);
    }
}
