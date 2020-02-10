using Core.Base.Data;
using System.Collections.Generic;
using LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.Data
{
    public interface IEmployerRepository : IBaseRepository<Employer>
    {
        IEnumerable<EmployerSearch> GetAllEmployer(string EmployerName, int skip, int take);
        int GetAllEmployerCount(string EmployerName);
        int CheckEmployerExist(string EmployerName);
    }
}
