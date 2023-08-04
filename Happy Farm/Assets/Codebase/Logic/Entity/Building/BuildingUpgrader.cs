using System.Collections.Generic;
using System.Linq;
using Codebase.Logic.Entity.Building.Constructions;
using Codebase.Logic.Upgrades;
using Codebase.Utils.Raycast;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.Building
{
    public class BuildingUpgrader : IComponent
    {
        private readonly IBuildable _buildable;
        private readonly Dictionary<Upgrade, List<IRequirement>> _upgrades;
        private Upgrade _currentUpgrade;
        private Transform _buildPoint;

        public BuildingUpgrader(Dictionary<Upgrade, List<IRequirement>> upgrades,
            IBuildable buildable)
        {
            _buildable = buildable;
            _upgrades = upgrades;
            _currentUpgrade = upgrades.Keys.First();
        }

        public async void Interact(Transform transform)
        {
            if (_currentUpgrade == null)
                return;

            foreach (var requirement in _upgrades[_currentUpgrade])
            {
                if (!requirement.IsSatisfied())
                    return;
            }

            await Upgrade(_buildPoint);
        }

        public async UniTask Upgrade(Transform transform)
        {
            _buildPoint = transform;
            if (_buildable.IsSatisfied())
            {
                await _buildable.Build(_currentUpgrade.BuildingTypeID, _buildPoint);
                Upgrade();
            }
        }

        private void Upgrade() => 
            _currentUpgrade = _upgrades.Keys.FirstOrDefault(upgrade => upgrade.Level == _currentUpgrade.Level + 1);
    }
}