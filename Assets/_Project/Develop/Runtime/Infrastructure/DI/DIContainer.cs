using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.DI
{
    public class DIContainer
    {
        private readonly DIContainer _headContainer;

        private readonly Dictionary<Type, Registration> _container = new();
        
        private readonly List<Type> _requests = new();

        public DIContainer() : this(null) {}

        public DIContainer(DIContainer headContainer) => _headContainer = headContainer;

        public IRegistrationOptions RegistryAsSingle<T>(Func<DIContainer, T> factory)
        {
            if (IsAlreadyRegister<T>())
                throw new InvalidOperationException($"{typeof(T)} already registered");
            
            Registration registration = new Registration(container => factory.Invoke(container));
            
            _container.Add(typeof(T), registration);

            return registration;
        }

        public T Resolve<T>()
        {
            if (_requests.Contains(typeof(T)))
                throw new InvalidOperationException($"{typeof(T)} cycled resolve");
            
            _requests.Add(typeof(T));

            try
            {
                if (_container.TryGetValue(typeof(T), out Registration result))
                    return (T)result.CreateInstanceFrom(this);
                
                if (_headContainer != null)
                    return _headContainer.Resolve<T>();
            }
            finally
            {
                _requests.Remove(typeof(T));
            }

            throw new InvalidOperationException($"No registered registration for {typeof(T)}");
        }

        public bool IsAlreadyRegister<T>()
        {
            if (_container.ContainsKey(typeof(T)))
                return true;

            if (_headContainer != null)
                return _headContainer.IsAlreadyRegister<T>();

            return false;
        }

        public void Initialize()
        {
            foreach (Registration registration in _container.Values)
            {
                if (registration.IsNonLazy)
                    registration.CreateInstanceFrom(this);
                
                registration.OnInitialize();
            }

        }
        
        public void Dispose()
        {
            foreach (Registration registration in _container.Values)
            {
                registration.OnDispose();
            }
        }
    }
}
