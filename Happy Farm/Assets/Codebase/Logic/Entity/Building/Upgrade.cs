using System;
using Codebase.Infrastructure.StaticData;

namespace Codebase.Logic.Entity.Building
{
    [Serializable]
    public class Upgrade
    {
        public int Level;
        public BuildingTypeID BuildingTypeID;
        public int Cost;
    }
}