using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency_Injection_Test_Interfaces.Simple
{
    public interface ISimpleCombined1
    {
        void SampleMethod();
    }

    public interface ISimpleCombined2
    {
        void SampleMethod();
    }

    public interface ISimpleCombined3
    {
        void SampleMethod();
    }

    public class SimpleCombined1 : ISimpleCombined1
    {
        private static int counter;
        public SimpleCombined1(ISingleton1 first, ITransient1 second)
        {
            if (first == null)  throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
        }
        public static int Instances { get => counter; set => counter = value; }
        public void SampleMethod() => Console.WriteLine("SimpleCombined1");
    }

    public class SimpleCombined2 : ISimpleCombined2
    {
        private static int counter;
        public SimpleCombined2(ISingleton2 first, ITransient2 second)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
        }
        public static int Instances { get => counter; set => counter = value; }
        public void SampleMethod() => Console.WriteLine("SimpleCombined1");
    }

    public class SimpleCombined3 : ISimpleCombined3
    {
        private static int counter;
        public SimpleCombined3(ISingleton3 first, ITransient3 second)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
        }
        public static int Instances { get => counter; set => counter = value; }
        public void SampleMethod() => Console.WriteLine("SimpleCombined1");
    }
}
