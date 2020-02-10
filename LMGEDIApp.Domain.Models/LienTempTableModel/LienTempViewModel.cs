using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.LienTempTableModel
{
    public class LienTempViewModel
    {
        public IEnumerable<LienTempTable> FileUploadViewModelResults { get; set; }
        public int FileCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int uSkip { get; set; }
    }
}
