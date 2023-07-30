namespace Codebase.Logic.Entity.ProductionEntities.Production.Consumers
{
    public interface IConsumer<T, T1>
    {
        T1 MaxAmount { get; }
        T GetProduct();
        bool CanConsume(T1 amount);
        T1 Consume(T1 amount);
    }
}