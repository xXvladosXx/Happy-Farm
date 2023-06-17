using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Codebase.Infrastructure.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public string GetCurrentScene => SceneManager.GetActiveScene().name;

        public async UniTask Load(string name, Action onLoaded = null)
        {
            if (name == GetCurrentScene)
            {
                await Addressables.LoadSceneAsync("Boot");
            }   
            
            await UniTask.Delay(300);
            
            await Addressables.LoadSceneAsync(name);
            
            onLoaded?.Invoke();
        }
    }
}