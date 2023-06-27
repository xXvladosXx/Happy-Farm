using System.Collections.Generic;
using System.Linq;
using Codebase.Utils.Raycast;
using UnityEngine;

namespace Codebase.Logic.Entity.Building
{
    public class Spawnable : IComponent
    {
        private readonly IBuildable _buildable;
        private readonly Dictionary<Upgrade, List<IRequirement>> _upgrades;
        private Upgrade _currentUpgrade;

        public Spawnable(Dictionary<Upgrade, List<IRequirement>> upgrades,
            IBuildable buildable)
        {
            _buildable = buildable;
            _upgrades = upgrades;
            _currentUpgrade = upgrades.Keys.First();
        }

        public void Interact(Transform transform)
        {
            if (_currentUpgrade == null)
                return;

            foreach (var requirement in _upgrades[_currentUpgrade])
            {
                if (!requirement.IsSatisfied())
                    return;
            }

            if (_buildable.IsSatisfied())
            {
                _buildable.Build(_currentUpgrade.BuildingTypeID, transform);
                Upgrade();
            }
        }

        public void Update()
        {
        }

        private void Upgrade() => 
            _currentUpgrade = _upgrades.Keys.FirstOrDefault(upgrade => upgrade.Level == _currentUpgrade.Level + 1);
    }
}