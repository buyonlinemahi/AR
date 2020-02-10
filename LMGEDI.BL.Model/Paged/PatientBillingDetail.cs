using LMGEDI.BL.Model;
using LMGEDI.BL.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDI.Core.BL.Model.Paged
{
    public class PatientBillingDetail : BasePaged
    {
        public IEnumerable<PatientBillingValidation> PatientBillingValidationDetails { get; set; }
    }
}
