using LMGEDI.Core.BL;
using LMGEDI.Core.BL.Implementation;
using LMGEDI.Core.Data;
using LMGEDI.Core.Data.SqlServer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBusinessTierTest
{
   [TestClass]
   public class StateTest
    {
        private  IStateRepository _stateRepository;
       
        private IState _StateImplBL;

        private LMGEDI.Core.Data.Model.State DLModel = new LMGEDI.Core.Data.Model.State();

        [TestInitialize]
        public void StateInitializer()
        {
            _stateRepository = new StateRepository(new Core.Base.Data.SqlServer.Factory.BaseContextFactory<LMGEDI.Core.Data.SqlServer.LMGEDIDBContext>());
            _StateImplBL = new StateImpl(_stateRepository);
        }

        [TestMethod]
        public void Get_State()
        {
            var getState = _StateImplBL.GetState();
            Assert.IsTrue(getState != null, "Unable to find");
        }
    }
}
