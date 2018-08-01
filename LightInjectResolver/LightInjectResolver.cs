using Dependency_Injection_Test_Interfaces;
using Dependency_Injection_Test_Interfaces.Classes.Generics;
using Dependency_Injection_Test_Interfaces.Classes.Simple;
using Dependency_Injection_Test_Interfaces.Convoluted;
using Dependency_Injection_Test_Interfaces.Multiple;
using Dependency_Injection_Test_Interfaces.Propertiess;
using Dependency_Injection_Test_Interfaces.Simple;
using LightInject;
using LightInject.Interception;
using System;
using System.Diagnostics;
using System.Linq;

namespace LightInjectResolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Benchmarker benchmark = new Benchmarker();

            int numberOfThreads = 4;

            benchmark.ClearGarbageCollector();
            benchmark.RunTestResolve(new LightInjectResolver(), benchmark.PrepareAndRegisterAndSimpleResolve);

            benchmark.ClearGarbageCollector();
            benchmark.RunTestResolve(new LightInjectResolver(), benchmark.PrepareAndRegister);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new LightInjectResolver(), benchmark.SingletonBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new LightInjectResolver(), benchmark.SingletonBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new LightInjectResolver(), benchmark.TransistentBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new LightInjectResolver(), benchmark.TransistentBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new LightInjectResolver(), benchmark.SimpleCombinedBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new LightInjectResolver(), benchmark.SimpleCombinedBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new LightInjectResolver(), benchmark.ComplexBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new LightInjectResolver(), benchmark.ComplexBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new LightInjectResolver(), benchmark.GenericToBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new LightInjectResolver(), benchmark.GenericToBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new LightInjectResolver(), benchmark.PropertyToBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new LightInjectResolver(), benchmark.PropertyToBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new LightInjectResolver(), benchmark.MultipleBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new LightInjectResolver(), benchmark.MultipleBenchmark, numberOfThreads);

            Console.ReadLine();
        }
    }

    public class LightInjectResolver : IContainerAdapter
    {
        private ServiceContainer container;

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
            container.Register<ISingleton1, Singleton1>(new PerContainerLifetime());
            container.Register<ISingleton2, Singleton2>(new PerContainerLifetime());
            container.Register<ISingleton3, Singleton3>(new PerContainerLifetime());

            container.Register<ITransient1, Transient1>();
            container.Register<ITransient2, Transient2>();
            container.Register<ITransient3, Transient3>();

            container.Register<ISimpleCombined1, SimpleCombined1>();
            container.Register<ISimpleCombined2, SimpleCombined2>();
            container.Register<ISimpleCombined3, SimpleCombined3>();
        }
        private void RegisterComplexObject()
        {
            container.Register<IBasicService1, BasicService1>(new PerContainerLifetime());
            container.Register<IBasicService2, BasicService2>(new PerContainerLifetime());
            container.Register<IBasicService3, BasicService3>(new PerContainerLifetime());
            container.Register<INestedService1, NestedService1>();
            container.Register<INestedService2, NestedService2>();
            container.Register<INestedService3, NestedService3>();
            container.Register<IConvolutedService1, ConvolutedService1>();
            container.Register<IConvolutedService2, ConvolutedService2>();
            container.Register<IConvolutedService3, ConvolutedService3>();

        }
        private void RegisterPropertyInjection()
        {
            container.Register<IPropertyService1, PropertyService1>(new PerContainerLifetime());
            container.Register<IPropertyService2, PropertyService2>(new PerContainerLifetime());
            container.Register<IPropertyService3, PropertyService3>(new PerContainerLifetime());
            container.Register<IStrategy1, Strategy1>();
            container.Register<IStrategy2, Strategy2>();
            container.Register<IStrategy3, Strategy3>();
            container.Register<ICompositeProperyObject1, CompositeProperyObject1>();
            container.Register<ICompositeProperyObject2, CompositeProperyObject2>();
            container.Register<ICompositeProperyObject3, CompositeProperyObject3>();
        }

        private void RegisterOpenGeneric()
        {
            container.Register(typeof(IGenericClass<>), typeof(GenericClass<>));
            container.Register(typeof(GenericImporter<>), typeof(GenericImporter<>));
        }

        private void RegisterMultiple()
        {
            container.Register<MultipleObject1>();
            container.Register<MultipleObject2>();
            container.Register<MultipleObject3>();
            container.Register<IMulti, Multi1>("Muli1");
            container.Register<IMulti, Multi2>("Muli2");
            container.Register<IMulti, Multi3>("Muli3");
            container.Register<IMulti, Multi4>("Muli4");
        }

        private void RegisterInterceptor()
        {
            container.Intercept(sr => sr.ServiceType == typeof(IInerceptor1), factory => new LightInjectInterceptionLogger());
            container.Intercept(sr => sr.ServiceType == typeof(IInerceptor2), factory => new LightInjectInterceptionLogger());
            container.Intercept(sr => sr.ServiceType == typeof(IInerceptor3), factory => new LightInjectInterceptionLogger());
        }
        private void RegisterBasic()
        {
            RegisterSimple();
            RegisterStandard();
            RegisterComplexObject();
        }

        public void Prepare()
        {
            container = new ServiceContainer();
            RegisterBasic();
            RegisterPropertyInjection();
            RegisterOpenGeneric();
            RegisterMultiple();
            RegisterInterceptor();

        }

        public void PrepareBasic()
        {
            container = new ServiceContainer();
            RegisterBasic();

        }

        public void Dispose() => container = null;       

        public object Resolve(Type type) => this.container.GetInstance(type);
    }

    public class LightInjectInterceptionLogger : IInterceptor
    {
        public object Invoke(IInvocationInfo invocationInfo)
        {
            var args = string.Join(", ", invocationInfo.Arguments.Select(x => (x ?? string.Empty).ToString()));
            Debug.WriteLine("LightInject: {0}({1})", invocationInfo.Method, args);
            return invocationInfo.Proceed();
        }
    }
}
