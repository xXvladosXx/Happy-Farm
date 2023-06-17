using UnityEngine;

namespace Codebase.Utils.Raycast
{
    public interface IRaycastUser
    {
        public RaycastHit? RaycastHit { get; }
        void Tick();
        RaycastHit? MakeRaycast(Vector2 pointFrom, LayerMask layerMask);
    }
}