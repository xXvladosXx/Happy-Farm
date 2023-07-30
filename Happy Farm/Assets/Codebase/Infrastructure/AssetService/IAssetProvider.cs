using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.Infrastructure.AssetService
{
    public interface IAssetProvider
    {
        Task<T> Load<T>(string address) where T : class;
        T GetAsset<T>(string path) where T : Object;
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        void Cleanup();
        Task<IList<T>> LoadAssets<T>(string address) where T : class;
    }
}