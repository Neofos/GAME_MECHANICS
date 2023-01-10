using System;

namespace CubeAdventure
{
    class KeyKey : Key, ICollectable
    {
        public override string KeyName { get; set; }

        public override string KeyAppearance { get; } = "K";

        public override ConsoleColor KeyColor { get; set; }

        public override int PosX { get; set; }

        public override int PosY { get; set; }
    }
}
