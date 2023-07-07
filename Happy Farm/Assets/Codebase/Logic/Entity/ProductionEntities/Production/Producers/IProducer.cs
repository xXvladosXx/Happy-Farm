using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production.Producers
{
    public interface IProducer
    {
        bool InProduction { get; }
        int Amount { get; }
        UniTask Produce(int amount, Vector3 position);
    }
}