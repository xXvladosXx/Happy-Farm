using System;
using UnityEngine;

namespace Codebase.Logic.QuestSystem.Core
{
    public abstract class Mission
    {
        private readonly MissionConfig _config;
        
        public string Id => _config.Id;
        public string Title => _config.Title;
        public Sprite Icon => _config.Icon;
        public bool IsCompleted { get; private set; }

        public override string ToString() => _config.Description;

        public event Action<Mission> OnStarted;
        public event Action<Mission> OnCompleted; 
        public event Action<Mission> OnStateChanged; 

        public Mission(MissionConfig config)
        {
            _config = config;
        }

        public void Start()
        {
            OnStarted?.Invoke(this);

            if (GetProgress() >= 1.0f)
            {
                Complete();
                return;
            }

            OnStart();
        }
    
        protected abstract float GetProgress(); 
        protected abstract void OnStart(); 
        protected abstract void OnComplete(); 
    
        protected void NotifyAboutStateChanged()
        {
            if (GetProgress() >= 1.0f)
            {
                Complete();
            }
            else
            {
                OnStateChanged?.Invoke(this);
            }
        }

        private void Complete()
        {
            OnComplete();
            IsCompleted = true;
            OnCompleted?.Invoke(this);
        }
    }
}