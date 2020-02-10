using LMGEDI.Core.Data;
using System.Linq;
using Omu.ValueInjecter;
using BLModel = LMGEDI.Core.BL.Model;
using DLModel = LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.BL.Implementation
{
    public class AdjusterImpl : IAdjuster
    {
        private readonly IAdjusterRepository _adusterRepository;

        public AdjusterImpl(IAdjusterRepository adjusterRepository)
        {
            _adusterRepository = adjusterRepository;
        }

        public int AddAdjusterRecord(DLModel.Adjuster modelAdjuster)
        {
            if (_adusterRepository.CheckAdjusterExist(modelAdjuster.AdjusterFirstName, modelAdjuster.AdjusterLastName) > 0)
                return 0;
            else
                return _adusterRepository.Add((DLModel.Adjuster)new DLModel.Adjuster().InjectFrom(modelAdjuster)).AdjusterId;
        }

        public int UpdateAdjusterRecord(DLModel.Adjuster modelAdjuster)
        {
            if (_adusterRepository.GetAll(adj => (adj.AdjusterId != modelAdjuster.AdjusterId) && adj.AdjusterFirstName.Equals(modelAdjuster.AdjusterFirstName) && adj.AdjusterLastName.Equals(modelAdjuster.AdjusterLastName)).Count() > 0)
                return 0;
            else
                return _adusterRepository.Update((DLModel.Adjuster)new DLModel.Adjuster().InjectFrom(modelAdjuster));
        }

        public BLModel.Paged.AdjusterDetail GetAllAdjusterByAdjusterName(string AdjusterName,int Skip,int Take)
        {
            return new BLModel.Paged.AdjusterDetail
            {
                AdjusterDetails = _adusterRepository.GetAllAdjuster(AdjusterName, Skip, Take).Select(adjuster => new BLModel.AdjusterSearch().InjectFrom(adjuster)).Cast<BLModel.AdjusterSearch>().ToList(),
                TotalCount = _adusterRepository.GetAllAdjusterCount(AdjusterName)
            };

        }

        public DLModel.AdjusterSearch GetLienAdjusterByAdjusterID(int AdjusterId)
        {
            DLModel.AdjusterSearch adjusterDetail = _adusterRepository.GetLienAdjusterByAdjusterID(AdjusterId);
            return adjusterDetail;
        }

        public DLModel.AdjusterSearch GetAdjusterByAdjusterID(int AdjusterId)
        {
            DLModel.AdjusterSearch adjusterDetail = _adusterRepository.GetAdjusterByAdjusterID(AdjusterId);
            return adjusterDetail;
        }
    }
}
