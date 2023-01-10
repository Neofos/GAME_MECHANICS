using System;

namespace Inventory
{
    internal struct OptionNavigator
    {
        public string Cursor { get; set; }

        public ConsoleColor CurrentOptionColor { get; set; }

        public int PosX { get; set; }

        public int PosY { get; set; }
    }
}