using System;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public interface IConsumer
    {
        string ItemId { get; }
        int Amount { get; }
    }
}