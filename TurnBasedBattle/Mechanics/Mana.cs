using System;

namespace TurnBasedBattle
{
    class Mana
    {
        public Mana(int posX, int posY, int value)
        {
            PosX = posX;
            PosY = posY;
            Value = value;
        }

        public int PosX { get; set; }

        public int PosY { get; set; }

        private double value;

        public double Value
        {
            get => value;
            set
            {
                if (value > 100)
                    this.value = 100;
                else if (value < 0)
                    this.value = 0;
                else
                    this.value = value;
            }
        }

        private ConsoleColor manaColor;

        public ConsoleColor ManaColor
        {
            get => manaColor;
            set
            {
                if (value != Console.BackgroundColor)
                    manaColor = value;
            }
        }
    }
}
