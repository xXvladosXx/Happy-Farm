using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public interface IProducer
    {
        bool InProduction { get; }
        UniTask Produce(Vector3 position);
        void StartProduction();
        void StopProduction();
    }
}