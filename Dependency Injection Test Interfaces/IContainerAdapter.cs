using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency_Injection_Test_Interfaces
{
    public interface IContainerAdapter : IDisposable
    {
        void Prepare();
        void PrepareBasic();      
        object Resolve(Type type);
    }
}
