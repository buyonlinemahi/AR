using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDI.Core.Data.Model
{
   public class ICRecordDetail
    {
     public int ICRecordDetailID { get; set; }
     public int FileID { get; set; }
     public string ICRecordDetailAddress { get; set; }
     public string ICRecordDetailCity { get; set; }
     public int StateID { get; set; }
     public string ICRecordDetailZip { get; set; }
     public string ICRecordDetailTaxID { get; set; }
    }
}
