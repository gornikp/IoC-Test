using Autofac;
using Castle.DynamicProxy;
using Dependency_Injection_Test_Interfaces.Classes.Simple;
using Dependency_Injection_Test_Interfaces.Convoluted;
using Dependency_Injection_Test_Interfaces.Propertiess;
using Dependency_Injection_Test_Interfaces.Simple;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using Dependency_Injection_Test_Interfaces.Classes.Generics;
using Dependency_Injection_Test_Interfaces;
using Dependency_Injection_Test_Interfaces.Multiple;

namespace AutofacResolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Benchmarker benchmark = new Benchmarker();

            benchmark.ClearGarbageCollector();
            benchmark.RunTestResolve(new AutofacResolver(), benchmark.PrepareAndRegisterAndSimpleResolve);

            benchmark.ClearGarbageCollector();
            benchmark.RunTestResolve(new AutofacResolver(), benchmark.PrepareAndRegister);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new AutofacResolver(), benchmark.SingletonBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new AutofacResolver(), benchmark.TransistentBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new AutofacResolver(), benchmark.SimpleCombinedBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new AutofacResolver(), benchmark.ComplexBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new AutofacResolver(), benchmark.InterceptorToBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new AutofacResolver(), benchmark.GenericToBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new AutofacResolver(), benchmark.PropertyToBenchmark);

            benchmark.ClearGarbageCollector();
            benchmark.RunTest(new AutofacResolver(), benchmark.MultipleBenchmark);

            Console.ReadLine();
        }
    }

    public class AutofacResolver : IContainerAdapter
    {
        private IContainer container;

        private static void RegisterBasic(ContainerBuilder autofacCB)
        {
            RegisterSimple(autofacCB);
            RegisterStandard(autofacCB);
            RegisterComplexObject(autofacCB);
        }
        private static void RegisterSimple(ContainerBuilder autofacCB)
        {
            autofacCB.Register(c => new Simple1()).As<ISimple1>();
            autofacCB.Register(c => new Simple2()).As<ISimple2>();
            autofacCB.Register(c => new Simple3()).As<ISimple3>();
            autofacCB.Register(c => new Simple4()).As<ISimple4>();
            autofacCB.Register(c => new Simple5()).As<ISimple5>();
            autofacCB.Register(c => new Simple6()).As<ISimple6>();
            autofacCB.Register(c => new Simple7()).As<ISimple7>();
            autofacCB.Register(c => new Simple8()).As<ISimple8>();
            autofacCB.Register(c => new Simple9()).As<ISimple9>();
            autofacCB.Register(c => new Simple10()).As<ISimple10>();
        }
        private static void RegisterStandard(ContainerBuilder autofacCB)
        {
            autofacCB.Register(c => new Singleton1()).As<ISingleton1>().SingleInstance();
            autofacCB.Register(c => new Singleton2()).As<ISingleton2>().SingleInstance();
            autofacCB.Register(c => new Singleton3()).As<ISingleton3>().SingleInstance();

            autofacCB.Register(c => new Transient1()).As<ITransient1>();
            autofacCB.Register(c => new Transient2()).As<ITransient2>();
            autofacCB.Register(c => new Transient3()).As<ITransient3>();

            autofacCB.Register(c => new SimpleCombined1(c.Resolve<ISingleton1>(), c.Resolve<ITransient1>())).As<ISimpleCombined1>();
            autofacCB.Register(c => new SimpleCombined2(c.Resolve<ISingleton2>(), c.Resolve<ITransient2>())).As<ISimpleCombined2>();
            autofacCB.Register(c => new SimpleCombined3(c.Resolve<ISingleton3>(), c.Resolve<ITransient3>())).As<ISimpleCombined3>();
        }
        private static void RegisterComplexObject(ContainerBuilder autofacCB)
        {
            autofacCB.Register(c => new BasicService1()).As<IBasicService1>().SingleInstance();
            autofacCB.Register(c => new BasicService2()).As<IBasicService2>().SingleInstance();
            autofacCB.Register(c => new BasicService3()).As<IBasicService3>().SingleInstance();
            autofacCB.Register(c => new NestedService1(c.Resolve<IBasicService1>())).As<INestedService1>();
            autofacCB.Register(c => new NestedService2(c.Resolve<IBasicService2>())).As<INestedService2>();
            autofacCB.Register(c => new NestedService3(c.Resolve<IBasicService3>())).As<INestedService3>();
            autofacCB.Register(c => new ConvolutedService1(c.Resolve<IBasicService1>(), c.Resolve<IBasicService2>(), c.Resolve<IBasicService3>(), c.Resolve<INestedService1>(), c.Resolve<INestedService2>(), c.Resolve<INestedService3>())).As<IConvolutedService1>();
            autofacCB.Register(c => new ConvolutedService2(c.Resolve<IBasicService1>(), c.Resolve<IBasicService2>(), c.Resolve<IBasicService3>(), c.Resolve<INestedService1>(), c.Resolve<INestedService2>(), c.Resolve<INestedService3>())).As<IConvolutedService2>();
            autofacCB.Register(c => new ConvolutedService3(c.Resolve<IBasicService1>(), c.Resolve<IBasicService2>(), c.Resolve<IBasicService3>(), c.Resolve<INestedService1>(), c.Resolve<INestedService2>(), c.Resolve<INestedService3>())).As<IConvolutedService3>();
        }
        private static void RegisterPropertyInjection(ContainerBuilder autofacCB)
        {
            autofacCB.Register(c => new PropertyService1()).As<IPropertyService1>().SingleInstance();
            autofacCB.Register(c => new PropertyService2()).As<IPropertyService2>().SingleInstance();
            autofacCB.Register(c => new PropertyService3()).As<IPropertyService3>().SingleInstance();

            autofacCB.Register(c => new Strategy1 { PropertyService1 = c.Resolve<IPropertyService1>() }).As<IStrategy1>();
            autofacCB.Register(c => new Strategy2 { PropertyService2 = c.Resolve<IPropertyService2>() }).As<IStrategy2>();
            autofacCB.Register(c => new Strategy3 { PropertyService3 = c.Resolve<IPropertyService3>() }).As<IStrategy3>();

            autofacCB.Register(c => new CompositeProperyObject1
            {
                PropertyService1 = c.Resolve<IPropertyService1>(),
                PropertyService2 = c.Resolve<IPropertyService2>(),
                PropertyService3 = c.Resolve<IPropertyService3>(),
                Strategy1 = c.Resolve<IStrategy1>(),
                Strategy2 = c.Resolve<IStrategy2>(),
                Strategy3 = c.Resolve<IStrategy3>()
            }).As<ICompositeProperyObject1>();

            autofacCB.Register(c => new CompositeProperyObject2
            {
                PropertyService1 = c.Resolve<IPropertyService1>(),
                PropertyService2 = c.Resolve<IPropertyService2>(),
                PropertyService3 = c.Resolve<IPropertyService3>(),
                Strategy1 = c.Resolve<IStrategy1>(),
                Strategy2 = c.Resolve<IStrategy2>(),
                Strategy3 = c.Resolve<IStrategy3>()
            }).As<ICompositeProperyObject2>();

            autofacCB.Register(c => new CompositeProperyObject3
            {
                PropertyService1 = c.Resolve<IPropertyService1>(),
                PropertyService2 = c.Resolve<IPropertyService2>(),
                PropertyService3 = c.Resolve<IPropertyService3>(),
                Strategy1 = c.Resolve<IStrategy1>(),
                Strategy2 = c.Resolve<IStrategy2>(),
                Strategy3 = c.Resolve<IStrategy3>()
            }).As<ICompositeProperyObject3>();
        }
        private static void RegisterOpenGeneric(ContainerBuilder autofacCB)
        {
            autofacCB.RegisterGeneric(typeof(GenericClass<>)).As(typeof(IGenericClass<>));
            autofacCB.RegisterGeneric(typeof(GenericImporter<>)).As(typeof(GenericImporter<>));
        }
        private static void RegisterInterceptor(ContainerBuilder autofacCB)
        {
            autofacCB.Register(c => new Inerceptor1()).As<IInerceptor1>().EnableInterfaceInterceptors();
            autofacCB.Register(c => new Inerceptor2()).As<IInerceptor2>().EnableInterfaceInterceptors();
            autofacCB.Register(c => new Inerceptor3()).As<IInerceptor3>().EnableInterfaceInterceptors();
        }

        private static void RegisterMultiple(ContainerBuilder autofacCB)
        {
            autofacCB.Register(c => new Multi1()).As<IMulti>();
            autofacCB.Register(c => new Multi2()).As<IMulti>();
            autofacCB.Register(c => new Multi3()).As<IMulti>();
            autofacCB.Register(c => new Multi4()).As<IMulti>();

            autofacCB.Register(c => new MultipleObject1(c.Resolve<IEnumerable<IMulti>>())).As<MultipleObject1>();
            autofacCB.Register(c => new MultipleObject2(c.Resolve<IEnumerable<IMulti>>())).As<MultipleObject2>();
            autofacCB.Register(c => new MultipleObject3(c.Resolve<IEnumerable<IMulti>>())).As<MultipleObject3>();
        }

        public void Prepare()
        {
            var autofacCB = new ContainerBuilder();

            autofacCB.Register(c => new AutofacInterceptionLogger());

            RegisterBasic(autofacCB);

            RegisterPropertyInjection(autofacCB);
            RegisterOpenGeneric(autofacCB);
            RegisterInterceptor(autofacCB);
            RegisterMultiple(autofacCB);

            this.container = autofacCB.Build();
        }

        public void PrepareBasic()
        {
            var autofacContainerBuilder = new ContainerBuilder();

            RegisterBasic(autofacContainerBuilder);

            this.container = autofacContainerBuilder.Build();
        }

        public object Resolve(Type type) => this.container.Resolve(type);

        public void Dispose()
        {
            
            if (this.container == null) return;

            this.container.Dispose();
            this.container = null;
        }
    }

    public class AutofacInterceptionLogger : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var args = string.Join(", ", invocation.Arguments.Select(x => (x ?? string.Empty).ToString()));
            Debug.WriteLine(string.Format("Autofac: {0}({1})", invocation.Method.Name, args));
            invocation.Proceed();
        }
    }
}
