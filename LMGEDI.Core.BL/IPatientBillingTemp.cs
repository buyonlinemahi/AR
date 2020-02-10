using LMGEDI.Core.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDI.Core.BL
{
    public interface IPatientBillingTemp
    {
        IEnumerable<TempExcelDataColumns> GetAllColumnNameFromTempPatientBilling();
        int DuplicatePatientBillingTempDataReplace();
    }
}
