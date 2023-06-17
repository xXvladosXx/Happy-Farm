namespace Codebase.Logic.ShopSystem
{
    public interface IShop
    {
        void Sell(int amount);
        void Buy(int amount);
    }
}