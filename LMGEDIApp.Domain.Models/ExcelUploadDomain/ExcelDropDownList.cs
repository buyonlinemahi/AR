using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMGEDIApp.Domain.Models.ExcelUploadDomain;

namespace LMGEDIApp.Domain.Models.ExcelUploadDomain
{
    public class ExcelDropDownList
    {
        public IEnumerable<TempExcelDataColumns> TempExcelDataColumnsDetails { get; set; }
        public IEnumerable<ExcelHeader> ExcelHeaderDetails { get; set; }
        public string rdoValue { get; set; }
    }      
}
