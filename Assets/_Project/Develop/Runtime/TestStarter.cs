using System;
using _Project.Develop.Runtime.ConfigManagement;
using _Project.Develop.Runtime.CoroutineManagement;
using _Project.Develop.Runtime.ResourcesManagement;
using UnityEngine;

namespace _Project.Develop.Runtime
{
    public class TestStarter : MonoBehaviour
    {
        private CoroutinePerformer  _coroutinePerformer;
        private ResourceLoadService _loadService;
        private ConfigService _configService;

        private void Awake()
        {
            CreateLoadService();
            CreateCoroutinePerformer();
            CreateConfigService();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Debug.Log($"{_configService.GetConfig<TestConfig>().Damage}");
        }

        public void CreateLoadService()
        {
            _loadService = new ResourceLoadService();
        }

        public void CreateCoroutinePerformer()
        {
            CoroutinePerformer performerPrefab = _loadService.Load<CoroutinePerformer>("CoroutinePerformer");
            _coroutinePerformer = Instantiate(performerPrefab);
        }

        public void CreateConfigService()
        {
             _configService = new ConfigService(new ResourcesConfigLoader(_loadService));
             _coroutinePerformer.StartPerform(_configService.LoadAsync());
        }
    }
}