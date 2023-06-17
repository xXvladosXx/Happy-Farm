using UnityEngine;

namespace Codebase.Logic.Animations
{
    [CreateAssetMenu(menuName = "Animator/AnimatorStateHasher")]
    public class AnimatorStateHasher : ScriptableObject
    {
        [field: SerializeField] public string WalkName { get; private set; } = "Walk";
        [field: SerializeField] public string InteractName { get; private set; } = "Interact";
        [field: SerializeField] public string IdleName { get; private set; } = "Idle";
        [field: SerializeField] public string SpeedName { get; private set; } = "Speed";
        

        public int WalkHash { get; private set; }
        public int InteractHash { get; private set; }
        public int IdleHash { get; private set; }
        public int SpeedHash { get; private set; }
        
        public void Init()
        {
            WalkHash = UnityEngine.Animator.StringToHash(WalkName);
            InteractHash = UnityEngine.Animator.StringToHash(InteractName);
            IdleHash = UnityEngine.Animator.StringToHash(IdleName);
            SpeedHash = UnityEngine.Animator.StringToHash(SpeedName);
        }
    }
}