using System.Collections;
using UnityEngine;

namespace _Project.Develop.Runtime.CoroutineManagement
{
    public interface ICoroutinePerformer
    {
        public Coroutine StartPerform(IEnumerator routine);
        public void StopPerforrm(Coroutine routine);
    }
}
