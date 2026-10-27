using System;
using System.Collections;
using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.CoroutineManagement
{
    public class CoroutinePerformer : MonoBehaviour, ICoroutinePerformer
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public Coroutine StartPerform(IEnumerator routine)
            => StartCoroutine(routine);

        public void StopPerform(Coroutine routine)
            => StopCoroutine(routine);
    }
}