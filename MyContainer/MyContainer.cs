using Dependency_Injection_Test_Interfaces.Multiple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyContainer
{
    public enum LifeStyleEnum
    {
        Singleton = 0,
        Transient = 1,
    }

    public enum ObjectTypeEnum
    {
        Default = 0,
        IEnumerable = 1
    }

    public interface IContainer
    {
        void Register<TResolvingType, TClass>();
        void Register<TResolvingType, TClass>(LifeStyleEnum lifeCycle);
        void Register<TResolvingType, TClass>(IEnumerable<Type> type);
        object Resolve(Type typeToResolve);
    }

    class PGIoC : IContainer
    {
        private readonly IList<ObjectInContainer> container = new List<ObjectInContainer>();

        public void Register<TResolvingType, TClass>()
        {
            Register<TResolvingType, TClass>(LifeStyleEnum.Singleton);
        }

        public void Register<TResolvingType, TClass>(LifeStyleEnum lifeCycle)
        {
            container.Add(new ObjectInContainer(typeof(TResolvingType), typeof(TClass), lifeCycle));
        }

        public void Register<TResolvingType, TClass>(IEnumerable<Type> type)
        {
            container.Add(new ObjectInContainer(typeof(TResolvingType), typeof(TClass), LifeStyleEnum.Transient, ObjectTypeEnum.IEnumerable) { ConstructorArguments = type } );
        }

        public object Resolve(Type typeToResolve)
        {
            return ResolveObject(typeToResolve);
        }

        private object ResolveObject(Type typeToResolve)
        {
            var objectInContainer = container.FirstOrDefault(o => o.ResolvingType == typeToResolve);
            if (objectInContainer == null)
                objectInContainer = container.FirstOrDefault(o => o.ResolvedClassType == typeToResolve);
            if (objectInContainer == null)
                throw new Exception(string.Format("Typu {0} nie ma w bazie zarejestrowanych serwisów", typeToResolve.Name));
            return GetInstance(objectInContainer);
        }

        private object GetInstance(ObjectInContainer objectInContainer)
        {
            if (objectInContainer.ObjectType == ObjectTypeEnum.IEnumerable)
            {
                var parameters = ResolveConstructorParametersFoIEnumerable(objectInContainer);

                Type elementType = objectInContainer.ResolvedClassType.GenericTypeArguments.First();
                MethodInfo castMethod = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(new System.Type[] { elementType });

                var castedObjectEnum = castMethod.Invoke(null, new object[] { parameters });

                objectInContainer.CreateInstance(new object[] { castedObjectEnum });
            }
            else if (objectInContainer.ClassInstance == null || objectInContainer.LifeCycle == LifeStyleEnum.Transient)
            {
                var parameters = ResolveConstructorParameters(objectInContainer);
                objectInContainer.CreateInstance(parameters.ToArray());
            }

            return objectInContainer.ClassInstance;
        }

        private IEnumerable<object> ResolveConstructorParametersFoIEnumerable(ObjectInContainer ObjectInContainer)
        {          
            foreach (var parameter in ObjectInContainer.ConstructorArguments)
            {
                yield return ResolveObject(parameter);
            }
        }

        private IEnumerable<object> ResolveConstructorParameters(ObjectInContainer ObjectInContainer)
        {
            var constructorInfo = ObjectInContainer.ResolvedClassType.GetConstructors().First();
            foreach (var parameter in constructorInfo.GetParameters())
            {
                yield return ResolveObject(parameter.ParameterType);
            }
        }
    }

    public class ObjectInContainer
    {
        public ObjectInContainer(Type resolvingType, Type resolvedClassType, LifeStyleEnum lifeCycleEnum, ObjectTypeEnum objectType = ObjectTypeEnum.Default)
        {
            ResolvingType = resolvingType;
            ResolvedClassType = resolvedClassType;
            LifeCycle = lifeCycleEnum;
            ObjectType = objectType;
        }

        public Type ResolvingType { get; private set; }

        public Type ResolvedClassType { get; private set; }

        public object ClassInstance { get; private set; }

        public LifeStyleEnum LifeCycle { get; private set; }
        public ObjectTypeEnum ObjectType { get; }
        public IEnumerable<Type> ConstructorArguments { get; internal set; }

        public void CreateInstance(params object[] args)
        {
            ClassInstance = Activator.CreateInstance(ResolvedClassType, args);
        }
    }
}
