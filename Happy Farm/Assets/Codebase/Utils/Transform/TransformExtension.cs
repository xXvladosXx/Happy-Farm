namespace Codebase.Utils.Transform
{
    public static class TransformExtension
    {
        public static bool IsNear(this UnityEngine.Transform transform, UnityEngine.Vector3 target, float minDistance = .5f)
        {
            var isNear = UnityEngine.Vector3.Distance(transform.position, target) < minDistance;
            return isNear;
        }
    }
}