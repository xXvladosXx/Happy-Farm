namespace Codebase.Logic.Storage.Container
{
    public interface ISlot
    {
        bool IsFull { get; }
        bool IsEmpty { get; }
        int CurrentAmount { get; }
        int Capacity { get; }
        IItem Item { get; }
        void Clear();
        void SetItem(IItem item, int amount);
        void RemoveItem(int number);
        void UpdateAmount(int amount);
    }
}