using System;

namespace Inventory
{
    internal class HealthPotion : ICollectableItem
    {
        public string Name => "Health Potion";

        public string Description => "Heals you 50 HP.";

        public int PosX { get; set; }

        public int PosY { get; set; }

        public Action OnUse { get; }
    }
}
