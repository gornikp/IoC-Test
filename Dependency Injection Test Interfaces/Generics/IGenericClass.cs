using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency_Injection_Test_Interfaces.Classes.Generics
{
    public interface IGenericClass<T>
    {
        T Tval { get; set; }
    }

    public class GenericClass<T> : IGenericClass<T>
    {
        public T Tval { get; set; }
    }

    public class GenericImporter<T>
    {
        private static int counter;

        public GenericImporter(IGenericClass<T> importGenericInterface)
        {
            if (importGenericInterface == null) throw new ArgumentNullException(nameof(importGenericInterface));

            System.Threading.Interlocked.Increment(ref counter);
        }

        public static int Instances
        {
            get { return counter; }
            set { counter = value; }
        }
    }

}
