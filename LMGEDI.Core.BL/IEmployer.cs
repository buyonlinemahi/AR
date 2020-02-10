using BLModel = LMGEDI.Core.BL.Model;
using DLModel = LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.BL
{
    public interface IEmployer
    {
        int AddEmployerRecord(DLModel.Employer modelEmployer);
        int UpdateEmployerRecord(DLModel.Employer modelEmployer);
        BLModel.Paged.EmployerDetail GetAllEmployerByEmployerName(string EmployerName, int Skip, int Take);
    }
}
