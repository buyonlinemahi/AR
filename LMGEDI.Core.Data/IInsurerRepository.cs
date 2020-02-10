using System.Collections.Generic;
using Core.Base.Data;
using LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.Data
{
    public interface IInsurerRepository : IBaseRepository<Insurer>
    {
        IEnumerable<InsurerSearch> GetAllInsurer(string InsurerName, int skip, int take);
        int GetAllInsurerCount(string InsurerName);
        int CheckInsurerExist(string InsurerName);
    }
}
