using System;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public interface IConsumer
    {
        bool TryToConsume();
        event Action OnConsumed;
    }
}