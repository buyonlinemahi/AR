using LMGEDI.BL.Model.Base;
using System.Collections.Generic;

namespace LMGEDI.Core.BL.Model.Paged
{
    public class OCRDetail : BasePaged
    {
        public IEnumerable<LMGEDI.Core.Data.Model.OCR> OCRDetails { get; set; }
    }
}
