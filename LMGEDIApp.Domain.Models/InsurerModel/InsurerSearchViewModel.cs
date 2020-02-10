using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.InsurerModel
{
    public class InsurerSearchViewModel
    {
        public IEnumerable<InsurerSearch> InsurerSearch { get; set; }
        public int InsurerCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
