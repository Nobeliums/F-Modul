using System;

namespace _Project.Develop.Runtime.Infrastructure.DI
{
    public class Registration : IRegistrationOptions
    {
        private Func<DIContainer, object> _register;
        
        private object _cachedInstance;

        public bool IsNonLazy { get; private set; }
        
        public Registration(Func<DIContainer, object> register) => _register = register;

        public object CreateInstanceFrom(DIContainer container)
        {
            if (_cachedInstance != null)
                return _cachedInstance;

            if (_register == null)
                throw new InvalidOperationException($"Not has register");
            
            _cachedInstance = _register.Invoke(container);
            
            return _cachedInstance;
        }

        public void OnInitialize()
        {
            if (_cachedInstance is IInitializable initializable)
                initializable.Initialize();
        }

        public void OnDispose()
        {
            if (_cachedInstance is IDisposable disposable)
                disposable.Dispose();
        }

        public void NonLazy() =>  IsNonLazy = true;
    }
}