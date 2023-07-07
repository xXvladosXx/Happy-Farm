namespace Codebase.Logic.Entity.ProductionEntities.Production.Consumers
{
    public interface IConsumer<T>
    {
        int MaxAmount { get; }
        T GetProduct();
        int Consume();
    }
}