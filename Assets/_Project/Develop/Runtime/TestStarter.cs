using System;
using System.Collections;
using _Project.Develop.Runtime.ConfigManagement;
using _Project.Develop.Runtime.CoroutineManagement;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.ResourcesManagement;
using UnityEngine;

namespace _Project.Develop.Runtime
{
    public class TestStarter : MonoBehaviour
    {
        DIContainer _container;

        private void Awake()
        {
            _container = new DIContainer();
            _container.RegistryAsSingle(CreateLoadService);
            _container.RegistryAsSingle(CreateCoroutinePerformer);
            _container.RegistryAsSingle(CreateConfigService);

            _container.Resolve<CoroutinePerformer>().StartPerform(LoadConfig());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var configService = _container.Resolve<ConfigService>();
                Debug.Log($"{configService.GetConfig<TestConfig>().Damage}");
            }
        }

        public ResourceLoadService CreateLoadService(DIContainer c)
        {
            return new ResourceLoadService();
        }

        public CoroutinePerformer CreateCoroutinePerformer(DIContainer c)
        {
            ResourceLoadService loadService = c.Resolve<ResourceLoadService>();
    
            CoroutinePerformer performerPrefab = loadService.Load<CoroutinePerformer>("CoroutinePerformer");
            return Instantiate(performerPrefab);
        }

        public ConfigService CreateConfigService(DIContainer c)
        {
            ResourceLoadService loadService = c.Resolve<ResourceLoadService>();
            
             return new ConfigService(new ResourcesConfigLoader(loadService));
        }

        public IEnumerator LoadConfig()
        {
            ConfigService service = _container.Resolve<ConfigService>();

            yield return service.LoadAsync();
        }
    }
}