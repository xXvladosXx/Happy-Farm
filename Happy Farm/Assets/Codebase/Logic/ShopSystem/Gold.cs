namespace Codebase.Logic.ShopSystem
{
    public class Gold : ICurrency
    {
        public int CurrentAmount { get; private set; }

        public Gold(int amount)
        {
            CurrentAmount = amount;
        }

        public void Add(int amount)
        {
            CurrentAmount += amount;
        }

        public void Remove(int amount)
        {
            CurrentAmount -= amount;
        }
    }
}