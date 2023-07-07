using System;
using Codebase.Logic.Entity.Building;

namespace Codebase.Logic.Upgrades
{
    [Serializable]
    public class Upgrade
    {
        public int Level;
        public BuildingTypeID BuildingTypeID;
        public int Cost;
    }
}