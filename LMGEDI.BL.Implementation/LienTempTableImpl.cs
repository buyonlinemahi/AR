using LMGEDI.Core.Data;
using LMGEDI.Core.Data.Model;
using System.Linq;
using Omu.ValueInjecter;
using BLModel = LMGEDI.Core.BL.Model;
using DLModel = LMGEDI.Core.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMGEDI.Core.BL.Implementation
{
    public class LienTempTableImpl : ILienTempTable
    {
        private readonly ILienTempTableRepository _lienTempTableRepository;

        public LienTempTableImpl(ILienTempTableRepository lienTempTableRepository)
        {
            _lienTempTableRepository = lienTempTableRepository;
        }

        public BLModel.Paged.LienTempTableDetail GetAllLienTempData(int Skip, int Take)
        {
            return new BLModel.Paged.LienTempTableDetail
            {
                LienTempTableDetails = _lienTempTableRepository.GetAllLienTempRecords(Skip,Take),
                TotalCount = _lienTempTableRepository.GetAll().Count()
            };
        }

        public void DeleteLienTempTableByUploadID(int uploadID)
        {
            _lienTempTableRepository.Delete(lienTempTableDetail => lienTempTableDetail.UploadId == uploadID);
        }

        public ErrorLogFile UpdateLienTempClaimNumber(IEnumerable<LienTempTable> objLienTempTable)
        {            
            var LienTempTableModel = objLienTempTable.ToList();
                
            foreach (var lientempmodel in LienTempTableModel)
            {
                _lienTempTableRepository.UpdateLienTempClaimNumberByUploadID(lientempmodel);
            }

            return _lienTempTableRepository.ProcessedLienTempTable();           
        }
    }
}
