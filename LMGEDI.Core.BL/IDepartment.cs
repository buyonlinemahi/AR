using System.Collections.Generic;
using BLModel = LMGEDI.Core.BL.Model;

namespace LMGEDI.Core.BL
{
    public interface IDepartment
    {
         IEnumerable<BLModel.Department> GetDepartment();
    }
}
