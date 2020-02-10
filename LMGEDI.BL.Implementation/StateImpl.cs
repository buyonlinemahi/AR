using LMGEDI.Core.Data;
using System.Linq;
using Omu.ValueInjecter;
using BLModel = LMGEDI.Core.BL.Model;
using System.Collections.Generic;

namespace LMGEDI.Core.BL.Implementation
{
    public  class StateImpl  :  IState
    {
      private readonly IStateRepository _stateRepository;

      public StateImpl(IStateRepository stateRepository)
      {
          _stateRepository = stateRepository;
      }
      public IEnumerable<BLModel.State> GetState()
      {
          return _stateRepository.GetAll().Select(state => new BLModel.State().InjectFrom(state)).Cast<BLModel.State>().ToList();
      }
  }
}
