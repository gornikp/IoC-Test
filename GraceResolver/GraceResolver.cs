using Dependency_Injection_Test_Interfaces;
using Dependency_Injection_Test_Interfaces.Classes.Generics;
using Dependency_Injection_Test_Interfaces.Classes.Simple;
using Dependency_Injection_Test_Interfaces.Convoluted;
using Dependency_Injection_Test_Interfaces.Multiple;
using Dependency_Injection_Test_Interfaces.Propertiess;
using Dependency_Injection_Test_Interfaces.Simple;
using Grace.DependencyInjection;
using Grace.Dynamic;
using System;

namespace GraceResolver
{
    class Program
    {
        static void Main(string[] args)
        {

            Benchmarker benchmark = new Benchmarker();

            benchmark.ClearGarbageCollector();
            benchmark.RunTestResolve(new GraceResolver(), benchmark.PrepareAndRegisterAndSimpleResolve);

            benchmark.ClearGarbageCollector();
            benchmark.RunTestResolve(new GraceResolver(), benchmark.PrepareAndRegister);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new GraceResolver(), benchmark.SingletonBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new GraceResolver(), benchmark.TransistentBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new GraceResolver(), benchmark.SimpleCombinedBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new GraceResolver(), benchmark.ComplexBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new GraceResolver(), benchmark.GenericToBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new GraceResolver(), benchmark.PropertyToBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new GraceResolver(), benchmark.MultipleBenchmark);

            Console.ReadLine();
        }
    }

    public class GraceResolver : IContainerAdapter
    {
        private DependencyInjectionContainer container;

        private void RegisterBasic()
        {
            RegisterSimple();
            RegisterStandard();
            RegisterComplexObject();
        }
        private void RegisterSimple()
        {
            container.Configure(
                c =>
                {
                    c.Export<Simple1>().As<ISimple1>();
                    c.Export<Simple2>().As<ISimple2>();
                    c.Export<Simple3>().As<ISimple3>();
                    c.Export<Simple4>().As<ISimple4>();
                    c.Export<Simple5>().As<ISimple5>();
                    c.Export<Simple6>().As<ISimple6>();
                    c.Export<Simple7>().As<ISimple7>();
                    c.Export<Simple8>().As<ISimple8>();
                    c.Export<Simple9>().As<ISimple9>();
                    c.Export<Simple10>().As<ISimple10>();
                });
        }
        private void RegisterStandard()
        {
            this.container.Configure(
                c =>
                {
                    c.Export<Singleton1>().As<ISingleton1>().Lifestyle.Singleton();
                    c.Export<Singleton2>().As<ISingleton2>().Lifestyle.Singleton();
                    c.Export<Singleton3>().As<ISingleton3>().Lifestyle.Singleton();
                    c.Export<Transient1>().As<ITransient1>();
                    c.Export<Transient2>().As<ITransient2>();
                    c.Export<Transient3>().As<ITransient3>();
                    c.Export<SimpleCombined1>().As<ISimpleCombined1>();
                    c.Export<SimpleCombined2>().As<ISimpleCombined2>();
                    c.Export<SimpleCombined3>().As<ISimpleCombined3>();
                });
        }
        private void RegisterComplexObject()
        {
            container.Configure(
                c =>
                {
                    c.Export<BasicService1>().As<IBasicService1>().Lifestyle.Singleton();
                    c.Export<BasicService2>().As<IBasicService2>().Lifestyle.Singleton();
                    c.Export<BasicService3>().As<IBasicService3>().Lifestyle.Singleton();
                    c.Export<NestedService1>().As<INestedService1>();
                    c.Export<NestedService2>().As<INestedService2>();
                    c.Export<NestedService3>().As<INestedService3>();
                    c.Export<ConvolutedService1>().As<IConvolutedService1>();
                    c.Export<ConvolutedService2>().As<IConvolutedService2>();
                    c.Export<ConvolutedService3>().As<IConvolutedService3>();
                });
           }
        private void RegisterPropertyInjection()
        {
            this.container.Configure(c =>
            {
                c.Export<PropertyService1>().As<IPropertyService1>().Lifestyle.Singleton();
                c.Export<PropertyService2>().As<IPropertyService2>().Lifestyle.Singleton();
                c.Export<PropertyService3>().As<IPropertyService3>().Lifestyle.Singleton();

                c.Export<Strategy1>().As<IStrategy1>().ImportProperty(x => x.PropertyService1);
                c.Export<Strategy2>().As<IStrategy2>().ImportProperty(x => x.PropertyService2);
                c.Export<Strategy3>().As<IStrategy3>().ImportProperty(x => x.PropertyService3);

                c.Export<CompositeProperyObject1>().As<ICompositeProperyObject1>().AutoWireProperties();
                c.Export<CompositeProperyObject2>().As<ICompositeProperyObject2>().AutoWireProperties();
                c.Export<CompositeProperyObject3>().As<ICompositeProperyObject3>().AutoWireProperties();
            });
        }     
        private void RegisterOpenGeneric()
        {
            container.Configure(c =>
            {
                c.Export(typeof(GenericImporter<>));
                c.Export(typeof(GenericClass<>)).As(typeof(IGenericClass<>));
            });
        }      

        private void RegisterMultiple()
        {
            container.Configure(
                c =>
                {
                    c.Export<Multi1>().As<IMulti>();
                    c.Export<Multi2>().As<IMulti>();
                    c.Export<Multi3>().As<IMulti>();
                    c.Export<Multi4>().As<IMulti>();
                    c.Export<MultipleObject1>();
                    c.Export<MultipleObject2>();
                    c.Export<MultipleObject3>();
                });
        }

        public void Prepare()
        {
            container = new DependencyInjectionContainer(GraceDynamicMethod.Configuration());

            RegisterBasic();
            RegisterPropertyInjection();
            RegisterOpenGeneric();
            RegisterMultiple();
        }

        public void PrepareBasic()
        {
            container = new DependencyInjectionContainer(GraceDynamicMethod.Configuration());

            RegisterBasic();
        }

        public void Dispose()
        {
            if (this.container == null)
            {
                return;
            }

            this.container.Dispose();
            this.container = null;
        }

        public object Resolve(Type type) => container.Locate(type);
    }
}
