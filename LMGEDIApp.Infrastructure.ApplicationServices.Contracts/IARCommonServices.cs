using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Infrastructure.ApplicationServices.Contracts
{
    public interface IARCommonServices
    {
        void CreateErrorLog(string errorMessage, string Stacktrace);
    }
}
