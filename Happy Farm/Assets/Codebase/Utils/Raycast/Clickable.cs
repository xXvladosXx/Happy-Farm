using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Codebase.Utils.Raycast
{
    public class Clickable : SerializedMonoBehaviour, IClickable
    {
        private readonly HashSet<IComponent> _components = new HashSet<IComponent>();

        public void Construct(params IComponent[] components)
        {
            AddComponents(components);
        }

        public void Interact()
        {
            foreach (var component in _components)
            {
                component.Interact(transform);
            }
        }

        private void AddComponents(IComponent[] components)
        {
            foreach (var component in components)
            {
                if (component == null)
                {
                    throw new NullReferenceException(nameof(component));
                }

                _components.Add(component);
            }
        }
    }
}