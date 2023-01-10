using System;

namespace CubeAdventure
{
    abstract class Key : ICollectable
    {
        public abstract string KeyName { get; set; }

        public abstract string KeyAppearance { get; }

        public abstract ConsoleColor KeyColor { get; set; }

        public abstract int PosX { get; set; }

        public abstract int PosY { get; set; }

        public void DrawKey()
        {
            Console.SetCursorPosition(PosX, PosY);
            Console.Write(KeyAppearance, Console.ForegroundColor = KeyColor);
        }
    }
}
