using System;
using System.Collections.Generic;
using System.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace IoC_Containers
{

    public enum Lifetime
    {
        Singleton,
        Transient,
        Scoped
    }

    public class Registration
    {
        public Type ImplementationType { get; set; }
        public Lifetime Lifetime { get; set; }
        public object SingletonInstance { get; set; }  
    }

    public class IoCContainer
    {
        private readonly Dictionary<Type, Registration> _registrations = new();

        public void Register<TInterface, TImplementation>(Lifetime lifetime = Lifetime.Singleton)
            where TImplementation : class, TInterface
        {
            _registrations[typeof(TInterface)] = new Registration
            {
                ImplementationType = typeof(TImplementation),
                Lifetime = lifetime
            };
        }
        public TInterface Resolve<TInterface>() where TInterface : class
            => (TInterface)Resolve(typeof(TInterface), scopedCache: null);

        public Scope CreateScope() => new Scope(this);
        public object Resolve(Type type, Dictionary<Type, object> scopedCache)
        {
            if (!_registrations.TryGetValue(type, out var registration))
            {
                return CreateInstance(type, scopedCache);
            }

            switch (registration.Lifetime)
            {
                case Lifetime.Singleton:                   
                    return registration.SingletonInstance ??=
                        CreateInstance(registration.ImplementationType, scopedCache);

                case Lifetime.Transient:
                    return CreateInstance(registration.ImplementationType, scopedCache);

                case Lifetime.Scoped:
                    if (scopedCache.TryGetValue(type, out var cached))
                        return cached;
                    var instance = CreateInstance(registration.ImplementationType, scopedCache);
                    scopedCache[type] = instance;
                    return instance;

                default:
                    throw new Exception();
            }
        }

        private object CreateInstance(Type type, Dictionary<Type, object> scopedCache)
        {
            var constructor = type.GetConstructors().First();
            var parameters = constructor.GetParameters();

            var dependencies = parameters
                .Select(p => Resolve(p.ParameterType, scopedCache))
                .ToArray();

            return constructor.Invoke(dependencies);
        }
    }

    public class Scope : IDisposable
    {
        private readonly IoCContainer _container;
        private readonly Dictionary<Type, object> _scopedCache = new();

        internal Scope(IoCContainer container)
        {
            _container = container;
        }

        public TInterface Resolve<TInterface>() where TInterface : class
            => (TInterface)_container.Resolve(typeof(TInterface), _scopedCache);

        public void Dispose()
        {
            _scopedCache.Clear();
        }
    }
}
