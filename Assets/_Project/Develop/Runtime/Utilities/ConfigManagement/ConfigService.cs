using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Utilities;
using Unity.VisualScripting;

namespace _Project.Develop.Runtime.Utilities.ConfigManagement
{
    public class ConfigService
    {
        private readonly IConfigLoader[]  _loaders;

        private Dictionary<Type, object> _configs = new();
        
        public ConfigService(params IConfigLoader[] loaders)
            =>  _loaders = loaders;
        
        public IEnumerator LoadAsync()
        {
            _configs.Clear();

            foreach (var loader in _loaders)
                yield return loader.LoadAsync(loadedConfigs => _configs.AddRange(loadedConfigs));
        }

        public T GetConfig<T>() where T : class
        {
            if (_configs.ContainsKey(typeof(T)) == false)
                throw new InvalidOperationException($"Config {nameof(T)} is not in service");
            
            return (T)_configs[typeof(T)];
        }
    }
}