using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency_Injection_Test_Interfaces.Propertiess
{
    public interface IPropertyService1 { }

    public class PropertyService1 : IPropertyService1
    {
        public PropertyService1() { }
    }

    public interface IPropertyService2 { }

    public class PropertyService2 : IPropertyService2
    {
        public PropertyService2() { }
    }

    public interface IPropertyService3 { }

    public class PropertyService3 : IPropertyService3
    {
        public PropertyService3() { }
    }


    public interface IStrategy1
    {
        void IsPropNull();
    }

    public class Strategy1 : IStrategy1
    {
        public IPropertyService1 PropertyService1 { get; set; }

        public void IsPropNull()
        {
            if (this.PropertyService1 == null)
            {
                throw new Exception("PropertyService1 was null for Strategy1");
            }
        }
    }

    public interface IStrategy2
    {
        void IsPropNull();
    }

    public class Strategy2 : IStrategy2
    {
        public IPropertyService2 PropertyService2 { get; set; }

        public void IsPropNull()
        {
            if (this.PropertyService2 == null)
            {
                throw new Exception("PropertyService2 was null for Strategy2");
            }
        }
    }

    public interface IStrategy3
    {
        void IsPropNull();
    }

    public class Strategy3 : IStrategy3
    {
        public IPropertyService3 PropertyService3 { get; set; }

        public void IsPropNull()
        {
            if (this.PropertyService3 == null)
            {
                throw new Exception("PropertyService3 was null for Strategy3");
            }
        }
    }
}
