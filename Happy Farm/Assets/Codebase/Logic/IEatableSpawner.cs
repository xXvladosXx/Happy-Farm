using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic
{
    public interface IEatableSpawner
    {
        UniTask CreateFood(Vector3 position);
    }
}