using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.PendingUploadModel
{
    public class PendingUploadDetail
    {
        public IEnumerable<PendingUploadRecord> PendingUploads { get; set; }
        public int TotalCount { get; set; }
    }
}
