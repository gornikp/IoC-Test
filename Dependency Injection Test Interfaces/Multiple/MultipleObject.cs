using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency_Injection_Test_Interfaces.Multiple
{
    public class MultipleObject1
    {
        public MultipleObject1(IEnumerable<IMulti> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            int adapterCount = 0;
            foreach (var adapter in items)
            {
                if (adapter == null)
                {
                    throw new ArgumentException("adapters item should be not null");
                }

                ++adapterCount;
            }

            if (adapterCount != 4)
            {
                throw new ArgumentException("there should be 4 adapters and there where: " + adapterCount, nameof(items));
            }
        }
    }

    public class MultipleObject2
    {
        public MultipleObject2(IEnumerable<IMulti> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            int adapterCount = 0;
            foreach (var adapter in items)
            {
                if (adapter == null)
                {
                    throw new ArgumentException("adapters item should be not null");
                }

                ++adapterCount;
            }

            if (adapterCount != 4)
            {
                throw new ArgumentException("there should be 4 adapters and there where: " + adapterCount, nameof(items));
            }
        }
    }

    public class MultipleObject3
    {
        public MultipleObject3(IEnumerable<IMulti> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            int adapterCount = 0;
            foreach (var adapter in items)
            {
                if (adapter == null)
                {
                    throw new ArgumentException("adapters item should be not null");
                }

                ++adapterCount;
            }

            if (adapterCount != 4)
            {
                throw new ArgumentException("there should be 4 adapters and there where: " + adapterCount, nameof(items));
            }
        }
    }

    public interface IMulti { }
    public class Multi1 : IMulti { }
    public class Multi2 : IMulti { }
    public class Multi3 : IMulti { }
    public class Multi4 : IMulti { }
}
