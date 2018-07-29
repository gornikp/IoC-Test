using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency_Injection_Test_Interfaces.Propertiess
{
    public interface ICompositeProperyObject1
    {
        void IsPropNull();
    }

    public class CompositeProperyObject1 : ICompositeProperyObject1
    {
        private static int counter;

        public CompositeProperyObject1() => System.Threading.Interlocked.Increment(ref counter);

        public static int Instances
        {
            get => counter;
            set => counter = value;
        }

        public IPropertyService1 PropertyService1 { get; set; }
        public IPropertyService2 PropertyService2 { get; set; }
        public IPropertyService3 PropertyService3 { get; set; }
        public IStrategy1 Strategy1 { get; set; }
        public IStrategy2 Strategy2 { get; set; }
        public IStrategy3 Strategy3 { get; set; }

        public void IsPropNull()
        {
            if (this.PropertyService1 == null) throw new Exception("PropertyService1 is null for " + this.GetType().Name);

            if (this.PropertyService2 == null) throw new Exception("PropertyService2 is null for " + this.GetType().Name);

            if (this.PropertyService3 == null) throw new Exception("ServiceC is null for " + this.GetType().Name);

            if (this.Strategy1 == null) throw new Exception("Strategy1 is null for " + this.GetType().Name);

            this.Strategy1.IsPropNull();

            if (this.Strategy2 == null) throw new Exception("Strategy2 is null for " + this.GetType().Name);

            this.Strategy2.IsPropNull();

            if (this.Strategy3 == null) throw new Exception("Strategy3 is null for " + this.GetType().Name);

            this.Strategy3.IsPropNull();
        }
    }

    public interface ICompositeProperyObject2
    {
        void IsPropNull();
    }

    public class CompositeProperyObject2: ICompositeProperyObject2
    {
        private static int counter;

        public CompositeProperyObject2() => System.Threading.Interlocked.Increment(ref counter);

        public static int Instances
        {
            get => counter;
            set => counter = value;
        }

        public IPropertyService1 PropertyService1 { get; set; }
        public IPropertyService2 PropertyService2 { get; set; }
        public IPropertyService3 PropertyService3 { get; set; }
        public IStrategy1 Strategy1 { get; set; }
        public IStrategy2 Strategy2 { get; set; }
        public IStrategy3 Strategy3 { get; set; }

        public void IsPropNull()
        {
            if (this.PropertyService1 == null) throw new Exception("PropertyService1 is null for " + this.GetType().Name);

            if (this.PropertyService2 == null) throw new Exception("PropertyService2 is null for " + this.GetType().Name);

            if (this.PropertyService3 == null) throw new Exception("ServiceC is null for CompositeProperyObject3 ");

            if (this.Strategy1 == null) throw new Exception("Strategy1 is null for " + this.GetType().Name);

            this.Strategy1.IsPropNull();

            if (this.Strategy2 == null) throw new Exception("Strategy2 is null for " + this.GetType().Name);

            this.Strategy2.IsPropNull();

            if (this.Strategy3 == null) throw new Exception("Strategy3 is null for " + this.GetType().Name);

            this.Strategy3.IsPropNull();
        }
    }

    public interface ICompositeProperyObject3
    {
        void IsPropNull();
    }

    public class CompositeProperyObject3 : ICompositeProperyObject3
    {
        private static int counter;

        public CompositeProperyObject3() => System.Threading.Interlocked.Increment(ref counter);

        public static int Instances
        {
            get => counter;
            set => counter = value;
        }

        public IPropertyService1 PropertyService1 { get; set; }
        public IPropertyService2 PropertyService2 { get; set; }
        public IPropertyService3 PropertyService3 { get; set; }
        public IStrategy1 Strategy1 { get; set; }
        public IStrategy2 Strategy2 { get; set; }
        public IStrategy3 Strategy3 { get; set; }

        public void IsPropNull()
        {
            if (this.PropertyService1 == null) throw new Exception("PropertyService1 is null for " + this.GetType().Name);

            if (this.PropertyService2 == null) throw new Exception("PropertyService2 is null for " + this.GetType().Name);

            if (this.PropertyService3 == null)  throw new Exception("ServiceC is null for CompositeProperyObject3 ");

            if (this.Strategy1 == null)  throw new Exception("Strategy1 is null for " + this.GetType().Name);

            this.Strategy1.IsPropNull();

            if (this.Strategy2 == null) throw new Exception("Strategy2 is null for " + this.GetType().Name);

            this.Strategy2.IsPropNull();

            if (this.Strategy3 == null) throw new Exception("Strategy3 is null for " + this.GetType().Name);

            this.Strategy3.IsPropNull();
        }
    }
}
