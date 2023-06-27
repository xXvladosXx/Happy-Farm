using System;
using Codebase.Logic.Storage.Container;
using Codebase.Utils.Raycast;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public class ItemConsumer : IConsumer
    {
        public string ItemId { get; private set; }
        public int Amount { get; private set; }

        public ItemConsumer(string itemId,
            int amount)
        {
            ItemId = itemId;
            Amount = amount;
        }
    }
}