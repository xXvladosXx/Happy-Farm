using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public interface IProducer
    {
        Transform Transform { get; }
        bool InProduction { get; }
        int Amount { get; }
        UniTask Produce(int amount, Vector3 position);
    }
}