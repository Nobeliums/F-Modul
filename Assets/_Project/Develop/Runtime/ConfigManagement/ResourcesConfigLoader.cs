using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.ResourcesManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.ConfigManagement
{
    public class ResourcesConfigLoader : IConfigLoader
    {
        private readonly ResourceLoadService _resources;

        private Dictionary<Type, string> _configsPaths = new()
        {
            {typeof(TestConfig), "TestConfig"}
        };
        
        public ResourcesConfigLoader(ResourceLoadService resources)
        {
            _resources = resources;
        }
        
        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> callback)
        {
            Dictionary<Type, object> loadedConfigs = new Dictionary<Type, object>();
            
            foreach (KeyValuePair<Type, string> configPath in _configsPaths)
            {
                ScriptableObject config = _resources.Load<ScriptableObject>(configPath.Value);
                
                if (config != null)
                    loadedConfigs.Add(configPath.Key, config);

                yield return null;
            }
            
            callback?.Invoke(loadedConfigs);
        }
    }
}