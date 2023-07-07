using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.Building.Constructions
{
    public interface IBuildable
    {
        bool IsSatisfied();
        UniTask Build(BuildingTypeID buildingTypeID, Transform parent);
    }
}