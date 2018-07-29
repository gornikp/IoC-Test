using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency_Injection_Test_Interfaces.Convoluted
{

    public interface INestedService1 { }

    public class NestedService1 : INestedService1
    {
        public NestedService1(IBasicService1 basicService)
        {
            if (basicService == null)  throw new ArgumentNullException(nameof(basicService));
        }
    }

    public interface INestedService2 { }

    public class NestedService2 : INestedService2
    {
        public NestedService2(IBasicService2 basicService)
        {
            if (basicService == null) throw new ArgumentNullException(nameof(basicService));
        }
    }

    public interface INestedService3 { }

    public class NestedService3 : INestedService3
    {
        public NestedService3(IBasicService3 basicService)
        {
            if (basicService == null) throw new ArgumentNullException(nameof(basicService));
        }
    }
}
