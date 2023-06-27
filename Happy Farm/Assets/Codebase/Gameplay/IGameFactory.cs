using System.Threading.Tasks;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity.Building;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Gameplay
{
    public interface IGameFactory
    {
        void RegisterSavable(GameObject entity);
        Task CreateUI();
        UniTask<ProductionConstruction> CreateProductionConstruction(BuildingTypeID buildingTypeID, Vector3 position);
    }
}