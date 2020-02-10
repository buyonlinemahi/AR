using Core.Base.Data;
using LMGEDI.Core.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDI.Core.Data
{
    public interface IPatientTempRepository : IBaseRepository<PatientTemp>
    {
        IEnumerable<TempExcelDataColumns> GetAllColumnNameFromTempPatient();
        int DuplicatePatientTempDataReplace();
    }
}
