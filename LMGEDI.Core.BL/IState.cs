using System.Collections.Generic;
using BLModel = LMGEDI.Core.BL.Model;

namespace LMGEDI.Core.BL
{
    public interface IState
    {
        IEnumerable<BLModel.State> GetState();
    }
}
