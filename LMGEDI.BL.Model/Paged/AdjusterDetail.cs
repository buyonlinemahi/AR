using System.Collections.Generic;
using LMGEDI.BL.Model.Base;


namespace LMGEDI.Core.BL.Model.Paged
{
    public class AdjusterDetail : BasePaged
    {
        public IEnumerable<AdjusterSearch> AdjusterDetails { get; set; }        
    }
}
