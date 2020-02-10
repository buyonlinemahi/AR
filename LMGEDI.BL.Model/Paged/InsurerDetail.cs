using LMGEDI.BL.Model.Base;
using System.Collections.Generic;

namespace LMGEDI.Core.BL.Model.Paged
{
    public class InsurerDetail : BasePaged
    {
        public IEnumerable<InsurerSearch> InsurerDetails { get; set; }
    }
}
