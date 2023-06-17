using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Codebase.Infrastructure.SceneManagement
{
    public interface ISceneLoader
    {
        string GetCurrentScene { get; }
        UniTask Load(string name, Action onLoaded = null);
    }
}