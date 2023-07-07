namespace Codebase.Logic.Upgrades
{
    public class MoneyRequirement : IRequirement
    {
        private readonly int _cost;
        private readonly int _currentMoney;

        public MoneyRequirement(int cost, int currentMoney)
        {
            _cost = cost;
            _currentMoney = currentMoney;
        }
        
        public bool IsSatisfied() => 
            !(_currentMoney < _cost);
    }
}