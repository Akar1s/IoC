using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoC_Containers
{
    internal class DI_Container
    {
        private readonly Dictionary<Type, object> _container = new();

        public void Add<TInterface, TImplementation>() where TImplementation : class, new()
        {
            var interfaceType = typeof(TInterface);
            if (!_container.ContainsKey(interfaceType))
            {
                _container[interfaceType] = CreateInstance(typeof(TImplementation));
            }
        }

        public TService Resolve<TService>() where TService : class
            => (TService)Resolve(typeof(TService));

        private object Resolve(Type typeServ)
        {
            if (_container.ContainsKey(typeServ))
            {
                return _container[typeServ];
            }

            return CreateInstance(typeServ);
        }

        private object CreateInstance(Type typeServ)
        {
            var contructor = typeServ.GetConstructors().First();
            var parameters = contructor.GetParameters();

            var dependensys = parameters
                .Select(p => Resolve(p.ParameterType))
                .ToArray();

            return contructor.Invoke(dependensys);
        }
    }
}