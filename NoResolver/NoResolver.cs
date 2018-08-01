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

namespace NoResolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Benchmarker benchmark = new Benchmarker();

            int numberOfThreads = 4;

            benchmark.ClearGarbageCollector();
            benchmark.RunTestResolve(new NoResolver(), benchmark.PrepareAndRegisterAndSimpleResolve);

            benchmark.ClearGarbageCollector();
            benchmark.RunTestResolve(new NoResolver(), benchmark.PrepareAndRegister);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new NoResolver(), benchmark.SingletonBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new NoResolver(), benchmark.SingletonBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new NoResolver(), benchmark.TransistentBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new NoResolver(), benchmark.TransistentBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new NoResolver(), benchmark.SimpleCombinedBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new NoResolver(), benchmark.SimpleCombinedBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new NoResolver(), benchmark.ComplexBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new NoResolver(), benchmark.ComplexBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new NoResolver(), benchmark.GenericToBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new NoResolver(), benchmark.GenericToBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new NoResolver(), benchmark.PropertyToBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new NoResolver(), benchmark.PropertyToBenchmark, numberOfThreads);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new NoResolver(), benchmark.MultipleBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new NoResolver(), benchmark.MultipleBenchmark, numberOfThreads);

            Console.ReadLine();
        }
    }

    public class NoResolver : IContainerAdapter
    {
        private readonly Dictionary<Type, Func<object>> container = new Dictionary<Type, Func<object>>();

        private void RegisterSimple()
        {
            container[typeof(ISimple1)] = () => new Simple1();
            container[typeof(ISimple2)] = () => new Simple2();
            container[typeof(ISimple3)] = () => new Simple3();
            container[typeof(ISimple4)] = () => new Simple4();
            container[typeof(ISimple5)] = () => new Simple5();
            container[typeof(ISimple6)] = () => new Simple6();
            container[typeof(ISimple7)] = () => new Simple7();
            container[typeof(ISimple8)] = () => new Simple8();
            container[typeof(ISimple9)] = () => new Simple9();
            container[typeof(ISimple10)] = () => new Simple10();
        }
        private void RegisterStandard()
        {
            ISingleton1 singleton1 = new Singleton1();

            container[typeof(ISingleton1)] = () => singleton1;
            container[typeof(ITransient1)] = () => new Transient1();
            container[typeof(ISimpleCombined1)] = () => new SimpleCombined1(singleton1, new Transient1());

            ISingleton2 singleton2 = new Singleton2();

            container[typeof(ISingleton2)] = () => singleton2;
            container[typeof(ITransient2)] = () => new Transient2();
            container[typeof(ISimpleCombined2)] = () => new SimpleCombined2(singleton2, new Transient2());

            ISingleton3 singleton3 = new Singleton3();

            container[typeof(ISingleton3)] = () => singleton3;
            container[typeof(ITransient3)] = () => new Transient3();
            container[typeof(ISimpleCombined3)] = () => new SimpleCombined3(singleton3, new Transient3());

        }
        private void RegisterComplexObject()
        {
            IBasicService1 basicService1 = new BasicService1();
            IBasicService2 basicService2 = new BasicService2();
            IBasicService3 basicService3 = new BasicService3();

            container[typeof(IBasicService1)] = () => basicService1;
            container[typeof(IBasicService2)] = () => basicService2;
            container[typeof(IBasicService3)] = () => basicService3;

            INestedService1 nestedService1 = new NestedService1(basicService1);
            INestedService2 nestedService2 = new NestedService2(basicService2);
            INestedService3 nestedService3 = new NestedService3(basicService3);

            container[typeof(INestedService1)] = () => nestedService1;
            container[typeof(INestedService2)] = () => basicService2;
            container[typeof(INestedService3)] = () => nestedService3;


            container[typeof(IConvolutedService1)] = () => new ConvolutedService1(basicService1, basicService2, basicService3, nestedService1, nestedService2, nestedService3);
            container[typeof(IConvolutedService2)] = () => new ConvolutedService2(basicService1, basicService2, basicService3, nestedService1, nestedService2, nestedService3);
            container[typeof(IConvolutedService3)] = () => new ConvolutedService3(basicService1, basicService2, basicService3, nestedService1, nestedService2, nestedService3);
        }
        private void RegisterPropertyInjection()
        {
            IPropertyService1 propertyService1 = new PropertyService1();
            IPropertyService2 propertyService2 = new PropertyService2();
            IPropertyService3 propertyService3 = new PropertyService3();

            container[typeof(ICompositeProperyObject1)] = () =>
                new CompositeProperyObject1
                {
                    PropertyService1 = propertyService1,
                    PropertyService2 = propertyService2,
                    PropertyService3 = propertyService3,
                    Strategy1 = new Strategy1 { PropertyService1 = propertyService1 },
                    Strategy2 = new Strategy2 { PropertyService2 = propertyService2 },
                    Strategy3 = new Strategy3 { PropertyService3 = propertyService3 }
                };

            container[typeof(ICompositeProperyObject2)] = () =>
                new CompositeProperyObject2
                {
                    PropertyService1 = propertyService1,
                    PropertyService2 = propertyService2,
                    PropertyService3 = propertyService3,
                    Strategy1 = new Strategy1 { PropertyService1 = propertyService1 },
                    Strategy2 = new Strategy2 { PropertyService2 = propertyService2 },
                    Strategy3 = new Strategy3 { PropertyService3 = propertyService3 }
                };

            container[typeof(ICompositeProperyObject3)] = () =>
                new CompositeProperyObject3
                {
                    PropertyService1 = propertyService1,
                    PropertyService2 = propertyService2,
                    PropertyService3 = propertyService3,
                    Strategy1 = new Strategy1 { PropertyService1 = propertyService1 },
                    Strategy2 = new Strategy2 { PropertyService2 = propertyService2 },
                    Strategy3 = new Strategy3 { PropertyService3 = propertyService3 }
                };
        }

        private void RegisterOpenGeneric()
        {
           container[typeof(GenericImporter<int>)] = () => new GenericImporter<int>(new GenericClass<int>());
           container[typeof(GenericImporter<float>)] = () => new GenericImporter<float>(new GenericClass<float>());
           container[typeof(GenericImporter<object>)] = () => new GenericImporter<object>(new GenericClass<object>());
        }

        private void RegisterMultiple()
        {

            var adapters = new List<IMulti>() { new Multi1(), new Multi2(), new Multi3(), new Multi4() };

            this.container[typeof(MultipleObject1)] = () => new MultipleObject1(adapters);
            this.container[typeof(MultipleObject2)] = () => new MultipleObject2(adapters);
            this.container[typeof(MultipleObject3)] = () => new MultipleObject3(adapters);
        }

        private void RegisterBasic()
        {
            RegisterSimple();
            RegisterStandard();
            RegisterComplexObject();
        }

        public void Prepare()
        {
            RegisterBasic();
            RegisterPropertyInjection();
            RegisterOpenGeneric();
            RegisterMultiple();

        }

        public void PrepareBasic()
        {
            RegisterBasic();

        }

        public void Dispose() { }

        public object Resolve(Type type) => container[type]();
    }
}
