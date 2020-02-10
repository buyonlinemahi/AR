using LMGEDI.Core.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDI.Core.BL
{
    public interface IPatientTemp
    {
        IEnumerable<TempExcelDataColumns> GetAllColumnNameFromTempPatient();
        int DuplicatePatientTempDataReplace();
    }
}
