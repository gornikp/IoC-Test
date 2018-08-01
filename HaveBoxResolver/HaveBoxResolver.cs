using Dependency_Injection_Test_Interfaces;
using Dependency_Injection_Test_Interfaces.Classes.Generics;
using Dependency_Injection_Test_Interfaces.Classes.Simple;
using Dependency_Injection_Test_Interfaces.Convoluted;
using Dependency_Injection_Test_Interfaces.Multiple;
using Dependency_Injection_Test_Interfaces.Propertiess;
using Dependency_Injection_Test_Interfaces.Simple;
using HaveBox;
using HaveBox.HaveBoxProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaveBoxResolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Benchmarker benchmark = new Benchmarker();

            int numberOfThreads = 4;

            benchmark.ClearGarbageCollector();
            benchmark.RunTestResolve(new HaveBoxResolver(), benchmark.PrepareAndRegisterAndSimpleResolve);

            benchmark.ClearGarbageCollector();
            benchmark.RunTestResolve(new HaveBoxResolver(), benchmark.PrepareAndRegister);


            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new HaveBoxResolver(), benchmark.SingletonBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new HaveBoxResolver(), benchmark.SingletonBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new HaveBoxResolver(), benchmark.TransistentBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new HaveBoxResolver(), benchmark.TransistentBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new HaveBoxResolver(), benchmark.SimpleCombinedBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new HaveBoxResolver(), benchmark.SimpleCombinedBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new HaveBoxResolver(), benchmark.ComplexBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new HaveBoxResolver(), benchmark.ComplexBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new HaveBoxResolver(), benchmark.GenericToBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new HaveBoxResolver(), benchmark.GenericToBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new HaveBoxResolver(), benchmark.PropertyToBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new HaveBoxResolver(), benchmark.PropertyToBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new HaveBoxResolver(), benchmark.MultipleBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new HaveBoxResolver(), benchmark.MultipleBenchmark, numberOfThreads);

            Console.ReadLine();
        }
    }

    public class HaveBoxResolver : IContainerAdapter
    {
        private Container container;

        private void RegisterSimple()
        {
            container.Configure(config => config.For<ISimple1>().Use<Simple1>());
            container.Configure(config => config.For<ISimple2>().Use<Simple2>());
            container.Configure(config => config.For<ISimple3>().Use<Simple3>());
            container.Configure(config => config.For<ISimple4>().Use<Simple4>());
            container.Configure(config => config.For<ISimple5>().Use<Simple5>());
            container.Configure(config => config.For<ISimple6>().Use<Simple6>());
            container.Configure(config => config.For<ISimple7>().Use<Simple7>());
            container.Configure(config => config.For<ISimple8>().Use<Simple8>());
            container.Configure(config => config.For<ISimple9>().Use<Simple9>());
            container.Configure(config => config.For<ISimple10>().Use<Simple10>());
        }
        private void RegisterStandard()
        {
            container.Configure(config => config.For<ISingleton1>().Use<Singleton1>().AsSingleton());
            container.Configure(config => config.For<ISingleton2>().Use<Singleton2>().AsSingleton());
            container.Configure(config => config.For<ISingleton3>().Use<Singleton3>().AsSingleton());
            container.Configure(config => config.For<ITransient1>().Use<Transient1>());
            container.Configure(config => config.For<ITransient2>().Use<Transient2>());
            container.Configure(config => config.For<ITransient3>().Use<Transient3>());
            container.Configure(config => config.For<ISimpleCombined1>().Use<SimpleCombined1>());
            container.Configure(config => config.For<ISimpleCombined2>().Use<SimpleCombined2>());
            container.Configure(config => config.For<ISimpleCombined3>().Use<SimpleCombined3>());
        }
        private void RegisterComplexObject()
        {
            container.Configure(config => config.For<IBasicService1>().Use<BasicService1>().AsSingleton());
            container.Configure(config => config.For<IBasicService2>().Use<BasicService2>().AsSingleton());
            container.Configure(config => config.For<IBasicService3>().Use<BasicService3>().AsSingleton());
            container.Configure(config => config.For<INestedService1>().Use<NestedService1>());
            container.Configure(config => config.For<INestedService2>().Use<NestedService2>());
            container.Configure(config => config.For<INestedService3>().Use<NestedService3>());
            container.Configure(config => config.For<IConvolutedService1>().Use<ConvolutedService1>());
            container.Configure(config => config.For<IConvolutedService2>().Use<ConvolutedService2>());
            container.Configure(config => config.For<IConvolutedService3>().Use<ConvolutedService3>());
        }
        private void RegisterPropertyInjection()
        {
            container.Configure(config => config.For<IPropertyService1>().Use<PropertyService1>().AsSingleton());
            container.Configure(config => config.For<IPropertyService2>().Use<PropertyService2>().AsSingleton());
            container.Configure(config => config.For<IPropertyService3>().Use<PropertyService3>().AsSingleton());
            container.Configure(config => config.For<IStrategy1>().Use(() => new Strategy1 { PropertyService1 = container.GetInstance<IPropertyService1>() }));
            container.Configure(config => config.For<IStrategy2>().Use(() => new Strategy2 { PropertyService2 = container.GetInstance<IPropertyService2>() }));
            container.Configure(config => config.For<IStrategy3>().Use(() => new Strategy3 { PropertyService3 = container.GetInstance<IPropertyService3>() }));
            container.Configure(
                config => config.For<ICompositeProperyObject1>().Use(
                    () => new CompositeProperyObject1
                    {
                        PropertyService1 = container.GetInstance<IPropertyService1>(),
                        PropertyService2 = container.GetInstance<IPropertyService2>(),
                        PropertyService3 = container.GetInstance<IPropertyService3>(),
                        Strategy1 = container.GetInstance<IStrategy1>(),
                        Strategy2 = container.GetInstance<IStrategy2>(),
                        Strategy3 = container.GetInstance<IStrategy3>()
                    }));
            container.Configure(
                config => config.For<ICompositeProperyObject2>().Use(
                    () => new CompositeProperyObject2
                    {
                        PropertyService1 = container.GetInstance<IPropertyService1>(),
                        PropertyService2 = container.GetInstance<IPropertyService2>(),
                        PropertyService3 = container.GetInstance<IPropertyService3>(),
                        Strategy1 = container.GetInstance<IStrategy1>(),
                        Strategy2 = container.GetInstance<IStrategy2>(),
                        Strategy3 = container.GetInstance<IStrategy3>()
                    }));
            container.Configure(
                config => config.For<ICompositeProperyObject3>().Use(
                    () => new CompositeProperyObject3
                    {
                        PropertyService1 = container.GetInstance<IPropertyService1>(),
                        PropertyService2 = container.GetInstance<IPropertyService2>(),
                        PropertyService3 = container.GetInstance<IPropertyService3>(),
                        Strategy1 = container.GetInstance<IStrategy1>(),
                        Strategy2 = container.GetInstance<IStrategy2>(),
                        Strategy3 = container.GetInstance<IStrategy3>()
                    }));      
        }

        private void RegisterOpenGeneric()
        {
            container.Configure(config => config.For(typeof(IGenericClass<>)).Use(typeof(GenericClass<>)));
            container.Configure(config => config.For(typeof(GenericImporter<>)).Use(typeof(GenericImporter<>)));
        }

        private void RegisterMultiple()
        {
            this.container.Configure(config =>
            {
                config.For<MultipleObject1>().Use<MultipleObject1>();
                config.For<MultipleObject2>().Use<MultipleObject2>();
                config.For<MultipleObject3>().Use<MultipleObject3>();
                config.For<IMulti>().Use<Multi1>();
                config.For<IMulti>().Use<Multi2>();
                config.For<IMulti>().Use<Multi3>();
                config.For<IMulti>().Use<Multi4>();
            });
        }

        private void RegisterInterceptor()
        {
            container.Configure(config => config.For<IInerceptor1>().Use<Inerceptor1>().AndInterceptMethodsWith<HaveBoxInterceptionLogger>());
            container.Configure(config => config.For<IInerceptor2>().Use<Inerceptor2>().AndInterceptMethodsWith<HaveBoxInterceptionLogger>());
            container.Configure(config => config.For<IInerceptor3>().Use<Inerceptor3>().AndInterceptMethodsWith<HaveBoxInterceptionLogger>());
        }
        private void RegisterBasic()
        {
            RegisterSimple();
            RegisterStandard();
            RegisterComplexObject();
        }

        public void Prepare()
        {
            container = new Container();
            RegisterBasic();
            RegisterPropertyInjection();
            RegisterOpenGeneric();
            RegisterMultiple();
            RegisterInterceptor();

        }

        public void PrepareBasic()
        {
            container = new Container();
            RegisterBasic();

        }

        public void Dispose() => container = null;

        public object Resolve(Type type) => this.container.GetInstance(type);
    }

    public class HaveBoxInterceptionLogger : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var args = string.Join(", ", invocation.Args.Select(x => (x ?? string.Empty).ToString()));
            Debug.WriteLine(string.Format("HaveBox: {0}({1})", invocation.Method.Name, args));

            invocation.Proceed();
        }
    }
}
