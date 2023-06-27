using System;

namespace Codebase.Infrastructure.StaticData
{
    [Flags]
    public enum ProductionAnimalTypeID
    {
        Cow = (1 << 0),
        Chicken =  (1 << 1),
        Sheep = (1 << 2),
    }
    
    [Flags]
    public enum EnemyAnimalTypeID
    {
        Bear = (1 << 0)
    }
}