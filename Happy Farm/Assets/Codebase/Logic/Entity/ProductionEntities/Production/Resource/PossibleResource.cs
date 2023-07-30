using System;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production.Resource
{
    public class PossibleResource : IResource<int>
    {
        public int Amount
        {
            get => _amount;
            set
            {
                var oldAmount = _amount;
                _amount = value;
                _amount = Mathf.Clamp(_amount, 0, MaxAmount);
                OnChanged?.Invoke(oldAmount, _amount);
            }
        }

        public ResourceType Type { get; }
        public int MaxAmount { get; set; }
        private int _amount;
        public event Action<int, int> OnChanged;

        public PossibleResource(ResourceType type, int amount, int maxAmount)
        {
            _amount = amount;
            Type = type;
            MaxAmount = maxAmount;
        }
    }
}