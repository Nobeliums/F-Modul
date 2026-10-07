using System.Collections;
using UnityEngine;

namespace _Project.Develop.Runtime.CoroutineManagement
{
    public class CoroutinePerformer : MonoBehaviour, ICoroutinePerformer
    {
        public Coroutine StartPerform(IEnumerator routine)
            => StartCoroutine(routine);

        public void StopPerforrm(Coroutine routine)
            => StopCoroutine(routine);
    }
}