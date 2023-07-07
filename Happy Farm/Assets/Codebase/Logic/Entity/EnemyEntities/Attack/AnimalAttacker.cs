using System;
using Codebase.Logic.Collision;
using UnityEngine;

namespace Codebase.Logic.Entity.EnemyEntities.Attack
{
    public class AnimalAttacker : MonoBehaviour, IAttacker
    {
        [SerializeField] private TriggerObserver _attackTrigger;

        private void OnEnable()
        {
            _attackTrigger.OnTriggerEntered += Attack;
        }

        private void OnDisable()
        {
            _attackTrigger.OnTriggerEntered -= Attack;
        }

        public void Attack(Collider collider)
        {
            if(collider.TryGetComponent(out IAttackable attackable))
                attackable.TakeDamage();
        }
    }
}