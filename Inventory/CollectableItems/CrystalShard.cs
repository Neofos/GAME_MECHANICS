using System;

namespace Inventory
{
    internal class CrystalShard : ICollectableItem
    {
        public string Name => "Crystal Shard";

        public string Description => "It's purpose is unknown.\nCan be sold.";

        public int PosX { get; set; }

        public int PosY { get; set; }

        public Action OnUse { get; }
    }
}
