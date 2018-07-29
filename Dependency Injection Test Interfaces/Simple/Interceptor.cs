using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency_Injection_Test_Interfaces.Simple
{

    public interface IInerceptor1
    {
        string Concat(string first, string second);
    }

    public interface IInerceptor2
    {
        string Concat(string first, string second);
    }

    public interface IInerceptor3
    {
        string Concat(string first, string second);
    }

    public class Inerceptor1 : IInerceptor1
    {
        private static int counter;

        public Inerceptor1() => System.Threading.Interlocked.Increment(ref counter);

        public static int Instances
        {
            get => counter;
            set => counter = value;
        }

        public virtual string Concat(string first, string second) => first + second;
    }

    public class Inerceptor2 : IInerceptor2
    {
        private static int counter;

        public Inerceptor2() => System.Threading.Interlocked.Increment(ref counter);

        public static int Instances
        {
            get => counter;
            set => counter = value;
        }

        public virtual string Concat(string first, string second) => first + second;
    }

    public class Inerceptor3 : IInerceptor3
    {
        private static int counter;

        public Inerceptor3() => System.Threading.Interlocked.Increment(ref counter);

        public static int Instances
        {
            get => counter;
            set => counter = value;
        }

        public virtual string Concat(string first, string second) => first + second;
    }
}
