using Codebase.Logic.Entity.ProductionEntities.Production.Resource;

namespace Codebase.Logic.Upgrades
{
    public class MoneyRequirement : IRequirement
    {
        private readonly int _cost;
        private readonly IResourcesStorage _currentMoney;

        public MoneyRequirement(int cost,
            IResourcesStorage currentMoney)
        {
            _cost = cost;
            _currentMoney = currentMoney;
        }
        
        public bool IsSatisfied() => _currentMoney.HasResource(ResourceType.Money, _cost);
    }
}