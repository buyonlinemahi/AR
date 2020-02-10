using LMGEDI.Core.Data;
using System.Linq;
using Omu.ValueInjecter;
using BLModel = LMGEDI.Core.BL.Model;
using System.Collections.Generic;

namespace LMGEDI.Core.BL.Implementation
{
    public class DepartmentImpl : IDepartment
    {
        private readonly IDepartmentRepository _departmentRepository;
         public DepartmentImpl(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
         public IEnumerable<BLModel.Department> GetDepartment()
         {
             return _departmentRepository.GetAll().Select(depat => new BLModel.Department().InjectFrom(depat)).Cast<BLModel.Department>().ToList();
         }
    }
}
