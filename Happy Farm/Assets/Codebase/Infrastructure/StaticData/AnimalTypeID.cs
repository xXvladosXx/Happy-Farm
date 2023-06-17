using System;

namespace Codebase.Infrastructure.StaticData
{
    [Flags]
    public enum AnimalTypeID
    {
        Cow = (1 << 0),
        Chicken =  (1 << 1),
        Sheep = (1 << 2),
        Bear = (1 << 3)
    }
}