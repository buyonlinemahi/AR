using LMGEDI.Core.Data.Model;
using System.Collections.Generic;
using BLModel = LMGEDI.Core.BL.Model;

namespace LMGEDI.Core.BL
{
    public interface ILienTempTable
    {
        BLModel.Paged.LienTempTableDetail GetAllLienTempData(int Skip, int Take);
        void DeleteLienTempTableByUploadID(int uploadID);
        ErrorLogFile UpdateLienTempClaimNumber(IEnumerable<LienTempTable> objLienTempTable);
    }
}
