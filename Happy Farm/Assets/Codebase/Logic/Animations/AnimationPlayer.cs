using Codebase.Utils.Raycast;
using UnityEngine;

namespace Codebase.Logic.Animations
{
    public class AnimationPlayer : IComponent
    {
        private readonly IAnimatorStateReader _animatorStateReader;

        public AnimationPlayer(IAnimatorStateReader animatorStateReader)
        {
            _animatorStateReader = animatorStateReader;
        }
        
        public void Interact(Transform transform)
        {
            _animatorStateReader.PlayAnimation(1);
        }

        public void Update()
        {
        }
    }
}