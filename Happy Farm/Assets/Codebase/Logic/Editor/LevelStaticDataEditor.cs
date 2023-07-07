using System.Linq;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity.Building;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Codebase.Logic.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var levelData = (LevelStaticData)target;
            if (GUILayout.Button("Generate Building Spawners"))
            {
                levelData.BuildingSpawners = FindObjectsOfType<BuildingMarker>()
                    .Select(x => new BuildingSpawnerData(x.BuildingTypeID, x.transform.position))
                    .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}