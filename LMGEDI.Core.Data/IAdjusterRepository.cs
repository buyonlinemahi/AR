using System.Collections.Generic;
using Core.Base.Data;
using LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.Data
{
    public interface IAdjusterRepository : IBaseRepository<Adjuster>
    {

        IEnumerable<AdjusterSearch> GetAllAdjuster(string AdjusterName, int skip, int take);
        int GetAllAdjusterCount(string AdjusterName);
        int CheckAdjusterExist(string AdjusterFirstName, string AdjusterLastName);
        AdjusterSearch GetLienAdjusterByAdjusterID(int AdjusterId);
        AdjusterSearch GetAdjusterByAdjusterID(int AdjusterId);        
    }
}
