using BLModel = LMGEDI.Core.BL.Model;
using DLModel = LMGEDI.Core.Data.Model;


namespace LMGEDI.Core.BL
{
    public interface IAdjuster
    {
        int AddAdjusterRecord(DLModel.Adjuster modelAdjuster);
        int UpdateAdjusterRecord(DLModel.Adjuster modelAdjuster);
        BLModel.Paged.AdjusterDetail GetAllAdjusterByAdjusterName(string AdjusterName, int Skip, int Take);
        DLModel.AdjusterSearch GetLienAdjusterByAdjusterID(int AdjusterId);
        DLModel.AdjusterSearch GetAdjusterByAdjusterID(int AdjusterId);
    }
}
