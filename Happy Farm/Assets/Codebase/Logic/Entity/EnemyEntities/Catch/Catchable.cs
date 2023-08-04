using Codebase.Utils.Raycast;
using UnityEngine;

namespace Codebase.Logic.Entity.EnemyEntities.Catch
{
    public class Catchable : ICatchable, IComponent
    {
        [field: SerializeField] public bool IsCaught { get; private set; }
        [field: SerializeField] public float MaxTimeToWaitCaught { get; private set; }
        
        private readonly int _clickAmountToCatch;
        private readonly float _timeToCatch;

        private float _timeSinceClicked;
        public int CurrentAmountToCatch { get; private set; }

        public float CurrentTimeToWaitCaught { get; private set; }

        public Catchable(int clickAmountToCatch,
            float timeToCatch,
            float maxTimeToWaitCaught)
        {
            _clickAmountToCatch = clickAmountToCatch;
            _timeToCatch = timeToCatch;
            MaxTimeToWaitCaught = maxTimeToWaitCaught;
        }

        public void Update()
        {
            if (IsCaught)
            {
                CurrentTimeToWaitCaught += Time.deltaTime;
                
                Debug.Log("Is caught");
                return;
            }

            _timeSinceClicked += Time.deltaTime;
            if (_timeSinceClicked > _timeToCatch)
            {
                CurrentAmountToCatch--;
                CurrentAmountToCatch = Mathf.Clamp(CurrentAmountToCatch, 0, _clickAmountToCatch);
                _timeSinceClicked = 0;
            }
        }


        public void Interact(Transform transform)
        {
            Catch();
        }

        public void Catch()
        {
            if(CurrentAmountToCatch >= _clickAmountToCatch)
                IsCaught = true;
            
            CurrentAmountToCatch++;
            _timeSinceClicked = 0;
        }
    }
}