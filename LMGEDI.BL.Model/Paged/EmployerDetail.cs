using LMGEDI.BL.Model.Base;
using System.Collections.Generic;

namespace LMGEDI.Core.BL.Model.Paged
{
    public class EmployerDetail : BasePaged
    {
        public IEnumerable<EmployerSearch> EmployerDetails { get; set; }
    }
}
