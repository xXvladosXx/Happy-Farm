using System;
using System.Collections.Generic;

namespace Codebase.Logic.TimeManagement
{
    public class PauseEnabler : IPauseHandler
    {
        private readonly List<IPauseHandler> _handlers = new List<IPauseHandler>();
        public bool IsPaused { get; private set; }

        public event Action OnHandlerDestroyed;
        
        public void Register(IPauseHandler handler)
        {
            _handlers.Add(handler);
        }
        
        public void Unregister(IPauseHandler handler)
        {
            _handlers.Remove(handler);
        }

        public void SetPaused(bool isPaused)
        {
            IsPaused = isPaused;
            foreach (var pauseHandler in _handlers)
            {
                pauseHandler.SetPaused(isPaused);
            }   
        }
    }
}