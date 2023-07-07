using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public class FoodRegistry
    {
        public int Amount { get; private set; }
        private int _maxAmount;

        public void Upgrade(int maxAmount) =>
            _maxAmount = maxAmount;
        
        public void Add(int amount)
        {
            Amount += amount;
            Amount = Mathf.Clamp(Amount, 0, _maxAmount);
        }

        public void Remove(int amount)
        {
            Amount -= amount;
            Amount = Mathf.Clamp(Amount, 0, _maxAmount);
        }
    }
}