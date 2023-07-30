using UnityEngine;

namespace Codebase.Utils.Raycast
{
    public interface IRaycastUser
    {
        public RaycastHit? RaycastHit { get; }
        RaycastHit? MakeRaycast(Vector2 pointFrom, LayerMask layerMask);
    }
}