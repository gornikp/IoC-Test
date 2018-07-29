using System;

namespace Dependency_Injection_Test_Interfaces.Convoluted
{

    public interface IConvolutedService1 { }

    public interface IConvolutedService2 { }

    public interface IConvolutedService3 { }

    public class ConvolutedService1 : IConvolutedService1
    {
        private static int counter;
        public ConvolutedService1(IBasicService1 basicService1, IBasicService2 basicService2, IBasicService3 basicService3, INestedService1 nestedService1, INestedService2 nestedService2, INestedService3 nestedService3)
        {
            if (basicService1 == null) throw new ArgumentNullException(nameof(basicService1));
            if (basicService2 == null) throw new ArgumentNullException(nameof(basicService2));
            if (basicService3 == null) throw new ArgumentNullException(nameof(basicService3));
            if (nestedService1 == null) throw new ArgumentNullException(nameof(nestedService1));
            if (nestedService2 == null) throw new ArgumentNullException(nameof(nestedService2));
            if (nestedService3 == null) throw new ArgumentNullException(nameof(nestedService3));

            System.Threading.Interlocked.Increment(ref counter);
        }

        public static int Instances
        {
            get => counter;
            set => counter = value;
        }
    }

    public class ConvolutedService2 : IConvolutedService2
    {
        private static int counter;
        public ConvolutedService2(IBasicService1 basicService1, IBasicService2 basicService2, IBasicService3 basicService3, INestedService1 nestedService1, INestedService2 nestedService2, INestedService3 nestedService3)
        {
            if (basicService1 == null) throw new ArgumentNullException(nameof(basicService1));
            if (basicService2 == null) throw new ArgumentNullException(nameof(basicService2));
            if (basicService3 == null) throw new ArgumentNullException(nameof(basicService3));
            if (nestedService1 == null) throw new ArgumentNullException(nameof(nestedService1));
            if (nestedService2 == null) throw new ArgumentNullException(nameof(nestedService2));
            if (nestedService3 == null) throw new ArgumentNullException(nameof(nestedService3));

            System.Threading.Interlocked.Increment(ref counter);
        }

        public static int Instances
        {
            get => counter;
            set => counter = value;
        }
    }

    public class ConvolutedService3 : IConvolutedService3
    {
        private static int counter;
        public ConvolutedService3(IBasicService1 basicService1, IBasicService2 basicService2, IBasicService3 basicService3, INestedService1 nestedService1, INestedService2 nestedService2, INestedService3 nestedService3)
        {
            if (basicService1 == null) throw new ArgumentNullException(nameof(basicService1));
            if (basicService2 == null) throw new ArgumentNullException(nameof(basicService2));
            if (basicService3 == null) throw new ArgumentNullException(nameof(basicService3));
            if (nestedService1 == null) throw new ArgumentNullException(nameof(nestedService1));
            if (nestedService2 == null) throw new ArgumentNullException(nameof(nestedService2));
            if (nestedService3 == null) throw new ArgumentNullException(nameof(nestedService3));

            System.Threading.Interlocked.Increment(ref counter);
        }

        public static int Instances
        {
            get => counter;
            set => counter = value;
        }
    }
}
