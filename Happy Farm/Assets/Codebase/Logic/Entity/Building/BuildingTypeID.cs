using System;

namespace Codebase.Logic.Entity.Building
{
    [Flags]
    public enum BuildingTypeID
    {
        MilkFactory = (1 << 0),
        EggFactory =  (1 << 1),
        WoolFactoryFirst = (1 << 2),
        WoodFactorySecond = (1 << 3),
        WoodFactoryThird = (1 << 4),
        StorageFirst = (1 << 5),
        StorageSecond = (1 << 6),
        None = (1 << 7),
        FoodProductionFirst = (1 << 8),
        FoodProductionSecond = (1 << 9),
        FoodProductionThird = (1 << 10),
    }
}