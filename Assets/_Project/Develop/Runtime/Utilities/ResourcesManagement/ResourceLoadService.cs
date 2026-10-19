using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.ResourcesManagement
{
    public class ResourceLoadService
    {
        public T Load<T>(string path) where T : UnityEngine.Object
            => Resources.Load<T>(path);
    }
}