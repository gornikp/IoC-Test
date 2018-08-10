using Dependency_Injection_Test_Interfaces;
using Dependency_Injection_Test_Interfaces.Classes.Simple;
using Dependency_Injection_Test_Interfaces.Convoluted;
using Dependency_Injection_Test_Interfaces.Multiple;
using Dependency_Injection_Test_Interfaces.Propertiess;
using Dependency_Injection_Test_Interfaces.Simple;
using MicroResolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroResolverResolver
{
    class Program
    {
        static void Main(string[] args)
        {
            //Nie wspiera generyków :(

            int numberOfThreads = 2;

            Benchmarker benchmark = new Benchmarker();

            benchmark.ClearGarbageCollector();
            benchmark.RunTestResolve(new MicroResolverResolver(), benchmark.PrepareAndRegisterAndSimpleResolve);

            benchmark.ClearGarbageCollector();
            benchmark.RunTestResolve(new MicroResolverResolver(), benchmark.PrepareAndRegister);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.SingletonBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.TransistentBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.SimpleCombinedBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.ComplexBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.PropertyToBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.MultipleBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.SingletonBenchmark, numberOfThreads);
            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.TransistentBenchmark, numberOfThreads);
            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.SimpleCombinedBenchmark, numberOfThreads);
            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.ComplexBenchmark, numberOfThreads);
            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.PropertyToBenchmark, numberOfThreads);
            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.MultipleBenchmark, numberOfThreads);

            numberOfThreads = 4;

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.SingletonBenchmark, numberOfThreads);
            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.TransistentBenchmark, numberOfThreads);
            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.SimpleCombinedBenchmark, numberOfThreads);
            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.ComplexBenchmark, numberOfThreads);
            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.PropertyToBenchmark, numberOfThreads);
            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.MultipleBenchmark, numberOfThreads);

            numberOfThreads = 8;

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.SingletonBenchmark, numberOfThreads);
            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.TransistentBenchmark, numberOfThreads);
            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.SimpleCombinedBenchmark, numberOfThreads);
            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.ComplexBenchmark, numberOfThreads);
            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.PropertyToBenchmark, numberOfThreads);
            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new MicroResolverResolver(), benchmark.MultipleBenchmark, numberOfThreads);

            Console.ReadLine();
        }
    }

    public class MicroResolverResolver : IContainerAdapter
    {
        private ObjectResolver container;

        private static void RegisterSimple(ObjectResolver r)
        {
            r.Register<ISimple1,Simple1>();
            r.Register<ISimple2,Simple2>();
            r.Register<ISimple3,Simple3>();
            r.Register<ISimple4,Simple4>();
            r.Register<ISimple5,Simple5>();
            r.Register<ISimple6,Simple6>();
            r.Register<ISimple7,Simple7>();
            r.Register<ISimple8,Simple8>();
            r.Register<ISimple9,Simple9>();
            r.Register<ISimple10,Simple10>();
        }
        private static void RegisterStandard(ObjectResolver r)
        {
            r.Register<ISingleton1,Singleton1>(Lifestyle.Singleton);
            r.Register<ISingleton2,Singleton2>(Lifestyle.Singleton);
            r.Register<ISingleton3,Singleton3>(Lifestyle.Singleton);
                                             
            r.Register<ITransient1,Transient1>();
            r.Register<ITransient2,Transient2>();
            r.Register<ITransient3,Transient3>();

            r.Register<ISimpleCombined1, SimpleCombined1>();
            r.Register<ISimpleCombined2, SimpleCombined2>();
            r.Register<ISimpleCombined3, SimpleCombined3>();
        }
        private static void RegisterComplexObject(ObjectResolver r)
        {
            r.Register<IBasicService1, BasicService1>(Lifestyle.Singleton);
            r.Register<IBasicService2, BasicService2>(Lifestyle.Singleton);
            r.Register<IBasicService3, BasicService3>(Lifestyle.Singleton);
            r.Register<INestedService1, NestedService1>();
            r.Register<INestedService2, NestedService2>();
            r.Register<INestedService3, NestedService3>();
            r.Register<IConvolutedService1, ConvolutedService1>();
            r.Register<IConvolutedService2, ConvolutedService2>();
            r.Register<IConvolutedService3, ConvolutedService3>();

        }
        private static void RegisterPropertyInjection(ObjectResolver r)
        {
            r.Register<IPropertyService1, PropertyService1>(Lifestyle.Singleton);
            r.Register<IPropertyService2, PropertyService2>(Lifestyle.Singleton);
            r.Register<IPropertyService3, PropertyService3>(Lifestyle.Singleton);
            r.Register<IStrategy1, Strategy1>();
            r.Register<IStrategy2, Strategy2>();
            r.Register<IStrategy3, Strategy3>();
            r.Register<ICompositeProperyObject1, CompositeProperyObject1>();
            r.Register<ICompositeProperyObject2, CompositeProperyObject2>();
            r.Register<ICompositeProperyObject3, CompositeProperyObject3>();
        }

        private static void RegisterMultiple(ObjectResolver resolver)
        {
            resolver.RegisterCollection<IMulti>(new[]{ typeof(Multi1), typeof(Multi2), typeof(Multi3), typeof(Multi4)});
            resolver.Register<MultipleObject1, MultipleObject1>();
            resolver.Register<MultipleObject2, MultipleObject2>();
            resolver.Register<MultipleObject3, MultipleObject3>();
        }

        private static void RegisterBasic(ObjectResolver r)
        {
            RegisterSimple(r);
            RegisterStandard(r);
            RegisterComplexObject(r);
        }

        public void Prepare()
        {
            container = ObjectResolver.Create();


            RegisterBasic(container);
            RegisterPropertyInjection(container);
            RegisterMultiple(container);

            container.Compile();
        }

        public void PrepareBasic()
        {
            container = ObjectResolver.Create();

            RegisterBasic(container);

            container.Compile();
        }

        public object Resolve(Type type) => this.container.Resolve(type);

        public void Dispose()
        {
            this.container = null;
        }
    }  
}
