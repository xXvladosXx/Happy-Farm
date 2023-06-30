namespace Codebase.Logic.QuestSystem.Core
{
    public class CollectResourcesMission : Mission
    {
        private readonly CollectResourcesMissionConfig _config;
        private readonly MissionRequires _missionRequires;

        private int _collectedResources;

        public CollectResourcesMission(CollectResourcesMissionConfig config,
            MissionRequires missionRequires) : base(config)
        {
            _config = config;
            _missionRequires = missionRequires;
        }

        protected override void OnStart()
        {
            _missionRequires.StorageUser.Inventory.OnContainerUpdated += OnResourcesAdded;
        }

        protected override void OnComplete()
        {
            _missionRequires.StorageUser.Inventory.OnContainerUpdated -= OnResourcesAdded;
        }

        protected override float GetProgress()
        {
            return (float) _collectedResources / _config.Amount;
        }

        private void OnResourcesAdded()
        {
            _collectedResources = _missionRequires.StorageUser.Inventory.FindItemAmount(_config.Item.ItemID);
            if (_collectedResources >= _config.Amount)
            {
                NotifyAboutStateChanged();
            }
        }
    }
}