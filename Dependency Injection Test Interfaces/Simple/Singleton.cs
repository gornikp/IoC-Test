using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency_Injection_Test_Interfaces.Simple
{
    public interface ISingleton1
    {
        void DoSomething();
    }

    public interface ISingleton2
    {
        void DoSomething();
    }

    public interface ISingleton3
    {
        void DoSomething();
    }

    public class Singleton1 : ISingleton1
    {
        private static int counter;

        public Singleton1() => System.Threading.Interlocked.Increment(ref counter);

        public static int Instances
        {
            get => counter;
            set => counter = value;
        }

        public void DoSomething() => Console.WriteLine("ISingleton1");
    }


    public class Singleton2 : ISingleton2
    {
        private static int counter;

        public Singleton2() => System.Threading.Interlocked.Increment(ref counter);


        public static int Instances
        {
            get => counter;
            set => counter = value;
        }

        public void DoSomething() => Console.WriteLine("ISingleton2");
    }

    public class Singleton3 : ISingleton3
    {
        private static int counter;

        public Singleton3() => System.Threading.Interlocked.Increment(ref counter);

        public static int Instances
        {
            get => counter;
            set => counter = value;
        }

        public void DoSomething() => Console.WriteLine("ISingleton3");
    }
}
