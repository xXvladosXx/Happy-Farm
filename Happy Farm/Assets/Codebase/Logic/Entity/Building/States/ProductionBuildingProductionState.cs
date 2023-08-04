using Codebase.Logic.Entity.StateMachine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Codebase.Logic.Entity.Building.States
{
    public class ProductionBuildingProductionState: State<ProductionConstruction>
    {
        private Vector3 _originalScale;
        private Sequence _productionSequence;

        public ProductionBuildingProductionState(ProductionConstruction stateInitializer) : base(stateInitializer)
        {
        }

        public override void OnEnter()
        {
            _originalScale = Initializer.Transform.localScale;
            
            Animate();
        }

        private void Animate()
        {
            _productionSequence = DOTween.Sequence();
            _productionSequence.Append(Initializer.Transform.DOScale(new Vector3(_originalScale.x, _originalScale.y*1.2f, _originalScale.z), .25f))
                .SetEase(Ease.InOutSine);

            _productionSequence.Append(Initializer.Transform.DOShakeRotation(.3f, 5f, 6, 45f))
                .SetEase(Ease.InOutSine);
            
            _productionSequence.Append(Initializer.Transform.DOScale(new Vector3(_originalScale.x * 1.2f, _originalScale.y, _originalScale.z * 1.2f), .25f))
                .SetEase(Ease.InOutSine);

            _productionSequence.SetLoops(-1, LoopType.Yoyo);
            _productionSequence.Play();
        }

        public override void OnUpdate()
        {
          
        }

        public override void OnExit()
        {
            _productionSequence.OnKill(() => Initializer.Transform.DOScale(new Vector3(_originalScale.x, _originalScale.y, _originalScale.z), .25f))
                .SetEase(Ease.InOutSine);
            _productionSequence.Kill(true);
        }
    }
}