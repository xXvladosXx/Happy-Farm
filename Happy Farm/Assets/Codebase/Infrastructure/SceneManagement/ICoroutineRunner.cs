using System.Collections;
using UnityEngine;

namespace Codebase.Infrastructure.SceneManagement
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        public void StopCoroutine(Coroutine routine);
    }
}