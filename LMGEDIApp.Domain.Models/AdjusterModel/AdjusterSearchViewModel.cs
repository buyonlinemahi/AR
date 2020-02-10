using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.AdjusterModel
{
    public class AdjusterSearchViewModel
    {
        public IEnumerable<AdjusterSearch> AdjusterSearch { get; set; }
        public int AdjusterCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

    }
}
