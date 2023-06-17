using System;

namespace Codebase.Infrastructure.StaticData
{
    [Flags]
    public enum BuildingTypeID
    {
        MilkFactory = (1 << 0),
        EggFactory =  (1 << 1),
        WoolFactory = (1 << 2),
    }
}