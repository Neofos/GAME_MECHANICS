using System;

namespace Inventory
{
    internal class ManaPotion : ICollectableItem
    {
        public string Name => "Mana Potion";

        public string Description => "Restores 50 mana.";

        public int PosX { get; set; }

        public int PosY { get; set; }

        public Action OnUse { get; }
    }
}
