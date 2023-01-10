using System;

namespace Inventory
{
    internal interface ICollectableItem
    {
        string Name { get; }

        string Description { get; }

        int PosX { get; set; }

        int PosY { get; set; }

        Action OnUse { get; }
    }
}