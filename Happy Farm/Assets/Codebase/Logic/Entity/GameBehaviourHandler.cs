using System;
using System.Collections.Generic;
using Zenject;

namespace Codebase.Logic.Entity
{
    public class GameBehaviourHandler : ITickable, IDisposable
    {
        private readonly List<IGameBehaviour> _gameBehaviours = new();
        public event Action<IGameBehaviour> OnGameBehaviourAdded;

        public T GetBehaviour<T>() where T : IGameBehaviour
        {
            var gameBehaviour = _gameBehaviours.Find(x => x is T);
            return (T) gameBehaviour;
        }

        public void Add(IGameBehaviour gameBehaviour)
        {
            _gameBehaviours.Add(gameBehaviour);
            OnGameBehaviourAdded?.Invoke(gameBehaviour);
        }
        
        public void Tick()
        {
            for (int i = 0; i < _gameBehaviours.Count; i++)
            {
                if (!_gameBehaviours[i].GameUpdate())
                {
                    int lastIndex = _gameBehaviours.Count - 1;
                    _gameBehaviours[i] = _gameBehaviours[lastIndex];
                    _gameBehaviours.RemoveAt(lastIndex);
                    i -= 1;
                }
            }   
        }

        public void Dispose()
        {
            _gameBehaviours.Clear();
        }

        public void Clear()
        {
            for (int i = 0; i < _gameBehaviours.Count; i++)
            {
                _gameBehaviours[i].Recycle();
            }
            
            _gameBehaviours.Clear();
        }
    }
}