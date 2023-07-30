using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Zenject;
using Object = UnityEngine.Object;

namespace Codebase.Infrastructure.AssetService
{
    public class AssetProvider : IAssetProvider, IInitializable
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCashe = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();

        public void Initialize()
        {
            Addressables.InitializeAsync();
        }
        
        public T GetAsset<T>(string path) where T : Object
        {
            var prefab = Resources.Load<T>(path);
            
            return prefab;
        }

        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (_completedCashe.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
                Addressables.LoadAssetAsync<T>(assetReference),  assetReference.AssetGUID);
        }

        public async Task<T> Load<T>(string address) where T : class
        {
            if (_completedCashe.TryGetValue(address, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;
      
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);
            
            return await RunWithCacheOnComplete(handle, address);
        }
        
        public async Task<IList<T>> LoadAssets<T>(string address) where T : class
        {
            if (_completedCashe.TryGetValue(address, out AsyncOperationHandle completedHandle))
                return new[] {completedHandle.Result as T};
      
            AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(address, null);
            
            return await RunWithCachesOnComplete(handle, address);
        }

        private async Task<T> RunWithCachesOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += completeHandle =>
            {
                _completedCashe[cacheKey] = completeHandle;
            };

            AddHandle<T>(cacheKey, handle);

            return await handle.Task;
        }


        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += completeHandle =>
            {
                _completedCashe[cacheKey] = completeHandle;
            };

            AddHandle<T>(cacheKey, handle);

            return await handle.Task;
        }

        public void Cleanup()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
            foreach (AsyncOperationHandle handle in resourceHandles)
                Addressables.Release(handle);
      
            _completedCashe.Clear();
            _handles.Clear();
        }

        private void AddHandle<T>(string key, AsyncOperationHandle handle) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }
    }
}