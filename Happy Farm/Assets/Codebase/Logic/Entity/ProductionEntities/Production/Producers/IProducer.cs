using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production.Producers
{
    public interface IProducer
    {
        int Amount { get; }
        UniTask Produce(int amount, Vector3 position);
    }
}