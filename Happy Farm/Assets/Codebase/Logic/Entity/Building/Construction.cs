using Codebase.Logic.Stats;
using UnityEngine;

namespace Codebase.Logic.Entity.Building
{
    public class Construction : IGameBehaviour
    {
        public BuildingTypeID BuildingTypeID { get; }
        public IDestroyable Destroyable { get; }
        public Transform Transform { get; }

        public Construction(BuildingTypeID buildingTypeID,
            IDestroyable destroyable,
            Transform transform)
        {
            BuildingTypeID = buildingTypeID;
            Destroyable = destroyable;
            Transform = transform;
        }

        public virtual bool GameUpdate() => Transform != null;

        public void Recycle()
        {
            Destroyable.Destroy();
        }
    }
}