using Dependency_Injection_Test_Interfaces;
using Dependency_Injection_Test_Interfaces.Classes.Simple;
using Dependency_Injection_Test_Interfaces.Convoluted;
using Dependency_Injection_Test_Interfaces.Multiple;
using Dependency_Injection_Test_Interfaces.Propertiess;
using Dependency_Injection_Test_Interfaces.Simple;
using DryIoc;
using System;
using Dependency_Injection_Test_Interfaces.Classes.Generics;

namespace DrylocResolver
{
    class Program
    {
        static void Main(string[] args)
        {

            Benchmarker benchmark = new Benchmarker();

            benchmark.ClearGarbageCollector();
            benchmark.RunTestResolve(new DryIoCResolver(), benchmark.PrepareAndRegisterAndSimpleResolve);

            benchmark.ClearGarbageCollector();
            benchmark.RunTestResolve(new DryIoCResolver(), benchmark.PrepareAndRegister);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new DryIoCResolver(), benchmark.SingletonBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new DryIoCResolver(), benchmark.TransistentBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new DryIoCResolver(), benchmark.SimpleCombinedBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new DryIoCResolver(), benchmark.ComplexBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new DryIoCResolver(), benchmark.GenericToBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new DryIoCResolver(), benchmark.PropertyToBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new DryIoCResolver(), benchmark.MultipleBenchmark);

            Console.ReadLine();
        }
    }

    public class DryIoCResolver : IContainerAdapter
    {
        private IContainer container;

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
            container.Register<ISingleton1, Singleton1>(Reuse.Singleton);
            container.Register<ISingleton2, Singleton2>(Reuse.Singleton);
            container.Register<ISingleton3, Singleton3>(Reuse.Singleton);

            container.Register<ITransient1, Transient1>();
            container.Register<ITransient2, Transient2>();
            container.Register<ITransient3, Transient3>();

            container.Register<ISimpleCombined1, SimpleCombined1>();
            container.Register<ISimpleCombined2, SimpleCombined2>();
            container.Register<ISimpleCombined3, SimpleCombined3>();
        }
        private void RegisterComplexObject()
        {
            container.Register<IBasicService1, BasicService1>(Reuse.Singleton);
            container.Register<IBasicService2, BasicService2>(Reuse.Singleton);
            container.Register<IBasicService3, BasicService3>(Reuse.Singleton);
            container.Register<INestedService1, NestedService1>();
            container.Register<INestedService2, NestedService2>();
            container.Register<INestedService3, NestedService3>();
            container.Register<IConvolutedService1, ConvolutedService1>();
            container.Register<IConvolutedService2, ConvolutedService2>();
            container.Register<IConvolutedService3, ConvolutedService3>();

        }
        private void RegisterPropertyInjection()
        {
            container.Register<IPropertyService1, PropertyService1>(Reuse.Singleton);
            container.Register<IPropertyService2, PropertyService2>(Reuse.Singleton);
            container.Register<IPropertyService3, PropertyService3>(Reuse.Singleton);
            container.Register<IStrategy1, Strategy1>(made: PropertiesAndFields.Auto);
            container.Register<IStrategy2, Strategy2>(made: PropertiesAndFields.Auto);
            container.Register<IStrategy3, Strategy3>(made: PropertiesAndFields.Auto);
            container.Register<ICompositeProperyObject1, CompositeProperyObject1>(made: PropertiesAndFields.Auto);
            container.Register<ICompositeProperyObject2, CompositeProperyObject2>(made: PropertiesAndFields.Auto);
            container.Register<ICompositeProperyObject3, CompositeProperyObject3>(made: PropertiesAndFields.Auto);
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
            container.Register<IMulti, Multi1>();
            container.Register<IMulti, Multi2>();
            container.Register<IMulti, Multi3>();
            container.Register<IMulti, Multi4>();
        }

        private void RegisterBasic()
        {
            RegisterSimple();
            RegisterStandard();
            RegisterComplexObject();
        }

        public void Prepare()
        {
            this.container = new Container();
            RegisterBasic();
            RegisterPropertyInjection();
            RegisterMultiple();
            RegisterOpenGeneric();
        }

        public void PrepareBasic()
        {
            container = new Container();
            RegisterBasic();
        }

        public object Resolve(Type type) => container.Resolve(type);

        public void Dispose()
        {
            if (this.container != null)
            {
                this.container.Dispose();
            }
        }
    }
}
