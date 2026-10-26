using System.Collections;
using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.CoroutineManagement
{
    public interface ICoroutinePerformer
    {
        public Coroutine StartPerform(IEnumerator routine);
        public void StopPerform(Coroutine routine);
    }
}
