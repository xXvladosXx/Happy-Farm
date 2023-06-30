using Codebase.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.Building
{
    public interface IBuildable
    {
        bool IsSatisfied();
        UniTask Build(BuildingTypeID buildingTypeID, Transform parent);
    }
}