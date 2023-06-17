namespace Codebase.Logic.ShopSystem
{
    public class Shop : IShop
    {
        private ICurrency _currency;

        public Shop()
        {
            _currency = new Gold(100);
        }
        
        public void Sell(int amount)
        {
            _currency.Add(amount);
        }

        public void Buy(int amount)
        {
            if(_currency.CurrentAmount >= amount)
                _currency.Remove(amount);
        }
    }
}