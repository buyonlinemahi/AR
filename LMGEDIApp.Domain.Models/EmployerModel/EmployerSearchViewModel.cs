using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.EmployerModel
{
    public class EmployerSearchViewModel
    {
        public IEnumerable<EmployerSearch> EmployerSearch { get; set; }
        public int EmployerCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
