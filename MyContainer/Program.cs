using Dependency_Injection_Test_Interfaces;
using Dependency_Injection_Test_Interfaces.Classes.Generics;
using Dependency_Injection_Test_Interfaces.Classes.Simple;
using Dependency_Injection_Test_Interfaces.Convoluted;
using Dependency_Injection_Test_Interfaces.Multiple;
using Dependency_Injection_Test_Interfaces.Propertiess;
using Dependency_Injection_Test_Interfaces.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            Benchmarker benchmark = new Benchmarker();

            int numberOfThreads = 4;

            benchmark.ClearGarbageCollector();
            benchmark.RunTestResolve(new PGIoCResolver(), benchmark.PrepareAndRegisterAndSimpleResolve);

            benchmark.ClearGarbageCollector();
            benchmark.RunTestResolve(new PGIoCResolver(), benchmark.PrepareAndRegister);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new PGIoCResolver(), benchmark.SingletonBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new PGIoCResolver(), benchmark.SingletonBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new PGIoCResolver(), benchmark.TransistentBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new PGIoCResolver(), benchmark.TransistentBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new PGIoCResolver(), benchmark.SimpleCombinedBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new PGIoCResolver(), benchmark.SimpleCombinedBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new PGIoCResolver(), benchmark.ComplexBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new PGIoCResolver(), benchmark.ComplexBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new PGIoCResolver(), benchmark.MultipleBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new PGIoCResolver(), benchmark.MultipleBenchmark, numberOfThreads);

            Console.ReadLine();
        }
    }


    public class PGIoCResolver : IContainerAdapter
    {
        private PGIoC container;

        private void RegisterBasic()
        {
            RegisterSimple();
            RegisterStandard();
            RegisterComplexObject();
        }
        private void RegisterSimple()
        {
            container.Register<ISimple1, Simple1>();
            container.Register<ISimple2, Simple2>();
            container.Register<ISimple3, Simple3>();
            container.Register<ISimple4, Simple4>();
            container.Register<ISimple5, Simple5>();
            container.Register<ISimple6, Simple6>();
            container.Register<ISimple7, Simple7>();
            container.Register<ISimple8, Simple8>();
            container.Register<ISimple9, Simple9>();
            container.Register<ISimple10, Simple10>();
        }
        private void RegisterStandard()
        {
            container.Register<ISingleton1, Singleton1>(LifeStyleEnum.Singleton);
            container.Register<ISingleton2, Singleton2>(LifeStyleEnum.Singleton);
            container.Register<ISingleton3, Singleton3>(LifeStyleEnum.Singleton);

            container.Register<ITransient1, Transient1>(LifeStyleEnum.Transient);
            container.Register<ITransient2, Transient2>(LifeStyleEnum.Transient);
            container.Register<ITransient3, Transient3>(LifeStyleEnum.Transient);

            container.Register<ISimpleCombined1, SimpleCombined1>();
            container.Register<ISimpleCombined2, SimpleCombined2>();
            container.Register<ISimpleCombined3, SimpleCombined3>();
        }
        private void RegisterComplexObject()
        {
            container.Register<IBasicService1, BasicService1>(LifeStyleEnum.Singleton);
            container.Register<IBasicService2, BasicService2>(LifeStyleEnum.Singleton);
            container.Register<IBasicService3, BasicService3>(LifeStyleEnum.Singleton);
            container.Register<INestedService1, NestedService1>();
            container.Register<INestedService2, NestedService2>();
            container.Register<INestedService3, NestedService3>();
            container.Register<IConvolutedService1, ConvolutedService1>();
            container.Register<IConvolutedService2, ConvolutedService2>();
            container.Register<IConvolutedService3, ConvolutedService3>();

        }
        private void RegisterPropertyInjection()
        {
            container.Register<IPropertyService1, PropertyService1>(LifeStyleEnum.Singleton);
            container.Register<IPropertyService2, PropertyService2>(LifeStyleEnum.Singleton);
            container.Register<IPropertyService3, PropertyService3>(LifeStyleEnum.Singleton);
            container.Register<IStrategy1, Strategy1>();
            container.Register<IStrategy2, Strategy2>();
            container.Register<IStrategy3, Strategy3>();
            container.Register<ICompositeProperyObject1, CompositeProperyObject1>();
            container.Register<ICompositeProperyObject2, CompositeProperyObject2>();
            container.Register<ICompositeProperyObject3, CompositeProperyObject3>();
        }

        private void RegisterMultiple()
        {
            container.Register<IMulti, Multi1>();
            container.Register<IMulti, Multi2>();
            container.Register<IMulti, Multi3>();
            container.Register<IMulti, Multi4>();
            container.Register<IEnumerable<IMulti>, List<IMulti>>(new List<Type> { typeof(Multi1), typeof(Multi2), typeof(Multi3), typeof(Multi4) });
            container.Register<MultipleObject1, MultipleObject1>();
            container.Register<MultipleObject2, MultipleObject2>();
            container.Register<MultipleObject3, MultipleObject3>();
        }

        public void Prepare()
        {
            container = new PGIoC();

            RegisterBasic();
            RegisterPropertyInjection();
            RegisterMultiple();
        }

        public void PrepareBasic()
        {
            container = new PGIoC();

            RegisterBasic();
        }

        public object Resolve(Type type) => this.container.Resolve(type);

        public void Dispose()
        {
            
        }
    }

}