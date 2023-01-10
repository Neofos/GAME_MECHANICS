using System;

namespace CubeAdventure
{
    class BonusKey : Key, ICollectable
    {
        public override string KeyName { get; set; }

        public override string KeyAppearance { get; } = "B";

        public override ConsoleColor KeyColor { get; set; }

        public override int PosX { get; set; }

        public override int PosY { get; set; }
    }
}
