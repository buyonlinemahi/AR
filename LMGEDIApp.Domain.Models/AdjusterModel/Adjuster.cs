using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.AdjusterModel
{
    public class Adjuster
    {
        public int AdjusterId { get; set; }
        public string AdjusterFirstName { get; set; }
        public string AdjusterLastName { get; set; }
        public string AdjusterPhone { get; set; }
        public string AdjusterFax { get; set; }
        public string AdjusterEmail { get; set; }
    }
}
