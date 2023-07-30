using System;
using ModestTree.Util;

namespace Codebase.Logic.Entity.ProductionEntities.Production.Resource
{
    public interface IResource<T>
    {
        public event Action<T, T> OnChanged;
        public T Amount { get; }
        public ResourceType Type { get; }
    }

    public enum ResourceType
    {
        Money,
        Stars,
        Food,
    }
}