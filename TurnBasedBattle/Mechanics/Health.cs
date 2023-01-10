using System;

namespace TurnBasedBattle
{
    class Health
    {
        public Health(int posX, int posY, int value)
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
                if (value > 1000)
                    this.value = 1000;
                else if (value < 0)
                    this.value = 0;
                else
                    this.value = value;

                if (this.value >= 80)
                    healthColor = ConsoleColor.DarkGreen;
                else if (this.value <= 80 && this.value > 60)
                    healthColor = ConsoleColor.Green;
                else if (this.value <= 60 && this.value > 40)
                    healthColor = ConsoleColor.DarkYellow;
                else if (this.value <= 40 && this.value > 20)
                    healthColor = ConsoleColor.Red;
                else if (this.value <= 20)
                    healthColor = ConsoleColor.DarkRed;
            }
        }

        private ConsoleColor healthColor;

        public ConsoleColor HealthColor
        {
            get => healthColor;
            set
            {
                if (value != Console.BackgroundColor)
                    healthColor = value;
            }
        }
    }
}
