using LMGEDI.BL.Model.Base;
using System.Collections.Generic;

namespace LMGEDI.Core.BL.Model.Paged
{
    public class PendingUploadDetail : BasePaged
    {
        public IEnumerable<LMGEDI.Core.Data.Model.PendingUploadRecord> PendingUploadDetails { get; set; }
    }
}
