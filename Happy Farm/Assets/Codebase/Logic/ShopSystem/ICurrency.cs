namespace Codebase.Logic.ShopSystem
{
    public interface ICurrency
    {
        int CurrentAmount { get; }
        void Add(int amount);
        void Remove(int amount);
    }
}