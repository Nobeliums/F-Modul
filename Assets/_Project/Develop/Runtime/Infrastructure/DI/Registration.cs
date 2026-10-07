using System;

namespace _Project.Develop.Runtime.Infrastructure.DI
{
    public class Registration
    {
        private Func<DIContainer, object> _register;
        
        private object _cachedInstance;
        
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
    }
}