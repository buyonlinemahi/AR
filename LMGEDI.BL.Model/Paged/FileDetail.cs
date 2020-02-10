using LMGEDI.BL.Model.Base;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;

namespace LMGEDI.Core.BL.Model.Paged
{
    public class FileDetail : BasePaged
    {
        public IEnumerable<FileSearchResult> FileDetails { get; set; }
    }
}
