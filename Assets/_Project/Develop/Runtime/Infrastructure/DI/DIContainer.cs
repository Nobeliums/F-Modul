using System;
using System.Collections.Generic;

namespace _Project.Develop.Runtime.Infrastructure.DI
{
    public class DIContainer
    {
        private DIContainer _headContainer;

        private Dictionary<Type, Registration> _registrations = new();

        public void RegistryAsSingle<T>(Func<DIContainer, T> factory)
        {
            Registration registration = new Registration(container => factory.Invoke(container));
            
            _registrations.Add(typeof(T), registration);
        }

        public T Resolve<T>()
        {
            if (_registrations.TryGetValue(typeof(T), out Registration result))
                return (T) result.CreateInstanceFrom(this);
            
            if (_headContainer != null)
                _headContainer.Resolve<T>();
            
            throw new InvalidOperationException($"No registered registration for {typeof(T)}");
        }
    }
}
