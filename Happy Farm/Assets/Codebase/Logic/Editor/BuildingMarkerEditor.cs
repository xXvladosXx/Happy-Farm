using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity.Building;
using UnityEditor;
using UnityEngine;

namespace Codebase.Logic.Editor
{
    [CustomEditor(typeof(BuildingMarker))]
    public class BuildingMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.NonSelected | GizmoType.Pickable)]
        public static void RenderCustomGizmo(BuildingMarker enemySpawner,
            GizmoType gizmoType)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(enemySpawner.transform.position, .5f);
        }
    }
}