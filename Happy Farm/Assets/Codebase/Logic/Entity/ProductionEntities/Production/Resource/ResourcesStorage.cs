using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree.Util;
using UnityEngine;
using Zenject;

namespace Codebase.Logic.Entity.ProductionEntities.Production.Resource
{
    public class ResourcesStorage : IResourcesStorage, IInitializable, IDisposable
    {
        public readonly Dictionary<ResourceType, PossibleResource> Resources = new();
        public event Action<ResourceType, int, int> OnResourceChanged; 

        public ResourcesStorage()
        {
            var moneyResource = new PossibleResource(ResourceType.Money, 0, 100000);
            var starsResource = new PossibleResource(ResourceType.Stars, 0,0);
            var foodResource = new PossibleResource(ResourceType.Food, 0, 0);
            
            Resources.Add(moneyResource.Type, moneyResource);
            Resources.Add(starsResource.Type, starsResource);
            Resources.Add(foodResource.Type, foodResource);
        }
        
        public void Initialize()
        {
            foreach (var resource in Resources.Values)
            {
                resource.OnChanged += ResourceChanged(resource);
            }
        }

        public void Dispose()
        {
            foreach (var resource in Resources.Values)
            {
                resource.OnChanged -= ResourceChanged(resource);
            }
        }

        private Action<int, int> ResourceChanged(PossibleResource resource)
        {
            return delegate(int oldValue, int newValue)
            {
                OnResourceChanged?.Invoke(resource.Type, oldValue, newValue);
            };
        }

        public void IncreaseMaxAmount(ResourceType type, int amount)
        {
            var resource = Resources[type];
            resource.MaxAmount = amount;
        }
        
        public void Add(ResourceType type, int amount)
        {
            var resource = Resources[type];
            resource.Amount += amount;
            
            Debug.Log($"Added {amount} {type}");
        }
        
        public void Remove(ResourceType type, int amount)
        {
            var resource = Resources[type];
            resource.Amount -= amount;
            
            Debug.Log($"Removed {amount} {type}");
        }
        
        public bool HasResource(ResourceType type, int amount)
        {
            var resource = Resources[type];
            return resource.Amount >= amount;
        }
    }
}