using Dependency_Injection_Test_Interfaces.Classes.Generics;
using Dependency_Injection_Test_Interfaces.Convoluted;
using Dependency_Injection_Test_Interfaces.Multiple;
using Dependency_Injection_Test_Interfaces.Propertiess;
using Dependency_Injection_Test_Interfaces.Simple;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dependency_Injection_Test_Interfaces
{
    public interface IBenchmarker
    {
        void RunTest(IContainerAdapter container, Action<IContainerAdapter> actionToBenchmark);

        void PrepareAndRegisterAndSimpleResolve(IContainerAdapter container);

    }

    public class Benchmarker : IBenchmarker
    {
        public void ClearGarbageCollector()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void RunTestResolve(IContainerAdapter container, Action<IContainerAdapter> actionToBenchmark)
        {
            Stopwatch sw = new Stopwatch();
            Warmup(container);
            actionToBenchmark(container); // Run once to prepare method
            sw.Start();

            for (var i = 0; i < 2000; i++)
            {
                actionToBenchmark(container);
            }

            sw.Stop();

            Console.WriteLine(actionToBenchmark.Method.Name + ": " + sw.ElapsedMilliseconds);
        }


        public void RunTestResolve(IContainerAdapter container, Action<IContainerAdapter> actionToBenchmark, int numberOfThreads)
        {
            Stopwatch sw = new Stopwatch();
            var count = 2000 / numberOfThreads;
            Warmup(container);
            actionToBenchmark(container); // Run once to prepare method
            var threads = new List<Thread>();
            for (int i = 0; i < numberOfThreads; i++)
            {
                threads.Add(new Thread(() =>
                {
                    for (var j = 0; j < count; j++)
                    {
                        actionToBenchmark(container);
                    }
                }));
            }

            sw.Start();
            threads.ForEach(t => t.Start());
            threads.ForEach(t => t.Join());
            sw.Stop();

            Console.WriteLine(actionToBenchmark.Method.Name + " using " + numberOfThreads + " threads: " + sw.ElapsedMilliseconds);
        }

        public void RunTest(IContainerAdapter container, Action<IContainerAdapter> actionToBenchmark, int numberOfThreads)
        {
            Stopwatch sw = new Stopwatch();
            Warmup2(container);

            var count = 500000 / numberOfThreads;
            actionToBenchmark(container); // Run once to prepare method
            var threads = new List<Thread>();
            for (int i = 0; i < numberOfThreads; i++)
            {
                threads.Add(new Thread(() =>
                {
                    for (var j = 0; j < count; j++)
                    {
                        actionToBenchmark(container);
                    }
                }));
            }

            sw.Start();
            threads.ForEach(t => t.Start());
            threads.ForEach(t => t.Join());
            sw.Stop();

            Console.WriteLine(actionToBenchmark.Method.Name + " using " + numberOfThreads + " threads: " + sw.ElapsedMilliseconds);
        }

        public void RunTest(IContainerAdapter container, Action<IContainerAdapter> actionToBenchmark)
        {
            Stopwatch sw = new Stopwatch();
            Warmup2(container);
            actionToBenchmark(container); // Run once to prepare method
            sw.Start();

            for (var i = 0; i < 500000; i++)
            {
                actionToBenchmark(container);
            }

            sw.Stop();

            Console.WriteLine(actionToBenchmark.Method.Name + ": " + sw.ElapsedMilliseconds);
        }

        private void Warmup2(IContainerAdapter container)
        {
            container.Prepare();
            ZeroCounters();
        }

        private void Warmup(IContainerAdapter container)
        {
            container.PrepareBasic();
            ZeroCounters();
            container.Dispose();
        }

        public void PrepareAndRegisterAndSimpleResolve(IContainerAdapter container)
        {
            container.PrepareBasic();
            container.Resolve(typeof(IBasicService1));
            container.Resolve(typeof(ISingleton1));
            container.Dispose();
        }

        public void PrepareAndRegister(IContainerAdapter container)
        {
            container.PrepareBasic();
            container.Dispose();
        }

        public void SingletonBenchmark(IContainerAdapter container)
        {
            var singleton1 = (ISingleton1)container.Resolve(typeof(ISingleton1));
            var singleton2 = (ISingleton2)container.Resolve(typeof(ISingleton2));
            var singleton3 = (ISingleton3)container.Resolve(typeof(ISingleton3));
        }

        public void TransistentBenchmark(IContainerAdapter container)
        {
            var transient1 = (ITransient1)container.Resolve(typeof(ITransient1));
            var transient2 = (ITransient2)container.Resolve(typeof(ITransient2));
            var transient3 = (ITransient3)container.Resolve(typeof(ITransient3));
        }

        public void SimpleCombinedBenchmark(IContainerAdapter container)
        {
            var combined1 = (ISimpleCombined1)container.Resolve(typeof(ISimpleCombined1));
            var combined2 = (ISimpleCombined2)container.Resolve(typeof(ISimpleCombined2));
            var combined3 = (ISimpleCombined3)container.Resolve(typeof(ISimpleCombined3));
        }

        public void ComplexBenchmark(IContainerAdapter container)
        {
            var complex1 = (IConvolutedService1)container.Resolve(typeof(IConvolutedService1));
            var complex2 = (IConvolutedService2)container.Resolve(typeof(IConvolutedService2));
            var complex3 = (IConvolutedService3)container.Resolve(typeof(IConvolutedService3));
        }

        public void InterceptorToBenchmark(IContainerAdapter container)
        {
            var result1 = (IInerceptor1)container.Resolve(typeof(IInerceptor1));
            var result2 = (IInerceptor2)container.Resolve(typeof(IInerceptor2));
            var result3 = (IInerceptor3)container.Resolve(typeof(IInerceptor3));

            result1.Concat("Hello", "Wolrd");
            result2.Concat("Hello", "Wolrd");
            result3.Concat("Hello", "Wolrd");
        }

        public void GenericToBenchmark(IContainerAdapter container)
        {
            var generic1 = (GenericImporter<int>)container.Resolve(typeof(GenericImporter<int>));
            var generic2 = (GenericImporter<float>)container.Resolve(typeof(GenericImporter<float>));
            var generic3 = (GenericImporter<object>)container.Resolve(typeof(GenericImporter<object>));
        }

        public void PropertyToBenchmark(IContainerAdapter container)
        {
            var complex1 = (ICompositeProperyObject1)container.Resolve(typeof(ICompositeProperyObject1));
            var complex2 = (ICompositeProperyObject2)container.Resolve(typeof(ICompositeProperyObject2));
            var complex3 = (ICompositeProperyObject3)container.Resolve(typeof(ICompositeProperyObject3));
        }

        public void MultipleBenchmark(IContainerAdapter container)
        {
            var iEnumerableImport1 = (MultipleObject1)container.Resolve(typeof(MultipleObject1));
            var iEnumerableImport2 = (MultipleObject2)container.Resolve(typeof(MultipleObject2));
            var iEnumerableImport3 = (MultipleObject3)container.Resolve(typeof(MultipleObject3));
        }

        public void ZeroCounters()
        {
            ConvolutedService1.Instances = 0;
            ConvolutedService2.Instances = 0;
            ConvolutedService3.Instances = 0;
            GenericImporter<int>.Instances = 0;
            GenericImporter<float>.Instances = 0;
            GenericImporter<object>.Instances = 0;
            CompositeProperyObject1.Instances = 0;
            CompositeProperyObject2.Instances = 0;
            CompositeProperyObject3.Instances = 0;
            Inerceptor1.Instances = 0;
            Inerceptor2.Instances = 0;
            Inerceptor3.Instances = 0;
            SimpleCombined1.Instances = 0;
            SimpleCombined2.Instances = 0;
            SimpleCombined3.Instances = 0;
            Singleton1.Instances = 0;
            Singleton2.Instances = 0;
            Singleton3.Instances = 0;
            Transient1.Instances = 0;
            Transient2.Instances = 0;
            Transient3.Instances = 0;
        }
    }
}
