using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.FileModel
{
    public class FileSearchViewModel
    {
        public IEnumerable<FileSearchResult> FileSearchResult { get; set; }
        public int FileCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
