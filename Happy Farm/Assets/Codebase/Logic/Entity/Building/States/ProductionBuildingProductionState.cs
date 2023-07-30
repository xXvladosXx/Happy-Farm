using Codebase.Logic.Entity.StateMachine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Codebase.Logic.Entity.Building.States
{
    public class ProductionBuildingProductionState: State<ProductionConstruction>
    {
        private Vector3 originalScale;
        private Sequence productionSequence;

        public ProductionBuildingProductionState(ProductionConstruction stateInitializer) : base(stateInitializer)
        {
        }

        public override void OnEnter()
        {
            originalScale = Initializer.Transform.localScale;
            
            Animate();
        }

        private void Animate()
        {
            productionSequence = DOTween.Sequence();
            productionSequence.Append(Initializer.Transform.DOScale(new Vector3(originalScale.x, originalScale.y*1.2f, originalScale.z), .25f))
                .SetEase(Ease.InOutSine);

            productionSequence.Append(Initializer.Transform.DOShakeRotation(.3f, 5f, 6, 45f))
                .SetEase(Ease.InOutSine);
            
            productionSequence.Append(Initializer.Transform.DOScale(new Vector3(originalScale.x * 1.2f, originalScale.y, originalScale.z * 1.2f), .25f))
                .SetEase(Ease.InOutSine);

            productionSequence.SetLoops(-1, LoopType.Yoyo);
            productionSequence.Play();
        }

        public override void OnUpdate()
        {
          
        }

        public override void OnExit()
        {
            productionSequence.OnKill(() => Initializer.Transform.DOScale(new Vector3(originalScale.x, originalScale.y, originalScale.z), .25f))
                .SetEase(Ease.InOutSine);
            productionSequence.Kill(true);
        }
    }
}