using UnityEngine;

namespace Codebase.Utils.Raycast
{
    [CreateAssetMenu(fileName = "RaycastSettings", menuName = "RaycastSettings", order = 1)]
    public class RaycastSettings : ScriptableObject
    {
        [field: SerializeField] public LayerMask MasksToRaycast { get; private set; }
    }
}