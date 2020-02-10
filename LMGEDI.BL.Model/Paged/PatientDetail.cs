using LMGEDI.BL.Model.Base;
using System.Collections.Generic;
namespace LMGEDI.Core.BL.Model.Paged
{
    public class PatientDetail : BasePaged
    {
        public IEnumerable<Patient> PatientDetails { get; set; }
    }
}
