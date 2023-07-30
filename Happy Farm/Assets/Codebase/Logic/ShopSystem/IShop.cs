namespace Codebase.Logic.ShopSystem
{
    public interface IShop
    {
        bool WasSold(int amount);
        void Buy(int amount);
    }
}