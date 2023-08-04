namespace Codebase.Logic.ShopSystem
{
    public interface IShop
    {
        bool Sell(int amount);
        void Buy(int amount);
    }
}