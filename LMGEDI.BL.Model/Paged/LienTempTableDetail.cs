using LMGEDI.BL.Model.Base;
using System.Collections.Generic;
using LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.BL.Model.Paged
{
    public class LienTempTableDetail : BasePaged
    {
        public IEnumerable<LienTempTableRecords> LienTempTableDetails { get; set; }
    }
}
