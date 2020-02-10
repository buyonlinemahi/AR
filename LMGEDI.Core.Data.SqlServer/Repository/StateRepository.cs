﻿using Core.Base.Data.SqlServer;
using Core.Base.Data.SqlServer.Factory;
using Core.Base.Data.SqlServer.Repository;
using LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.Data.SqlServer.Repository
{
    public class StateRepository : BaseRepository<State, LMGEDIDBContext>, IStateRepository
    {
        public StateRepository(IContextFactory<LMGEDIDBContext> contextFactory) :
            base(new BaseUnitOfWork<LMGEDIDBContext>(contextFactory),contextFactory)
        {
        }

    }
}
