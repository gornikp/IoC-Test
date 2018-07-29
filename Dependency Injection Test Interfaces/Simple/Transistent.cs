using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency_Injection_Test_Interfaces.Simple
{
    public interface ITransient1
    {
        void Fire();
    }

    public interface ITransient2
    {
        void Fire();
    }

    public interface ITransient3
    {
        void Fire();
    }

    public class Transient1 : ITransient1
    {
        private static int counter;

        public Transient1() => System.Threading.Interlocked.Increment(ref counter);

        public static int Instances
        {
            get => counter;
            set => counter = value;
        }

        public void Fire() =>  Console.WriteLine("ITransient1");

    }

    public class Transient2 : ITransient2
    {
        private static int counter;

        public Transient2() => System.Threading.Interlocked.Increment(ref counter);


        public static int Instances
        {
            get => counter;
            set => counter = value;
        }

        public void Fire() => Console.WriteLine("ITransient2");
    }


    public class Transient3 : ITransient3
    {
        private static int counter;

        public Transient3() => System.Threading.Interlocked.Increment(ref counter);
        
        public static int Instances
        {
            get => counter;
            set => counter = value;
        }

        public void Fire() => Console.WriteLine("ITransient3");
    }
}
