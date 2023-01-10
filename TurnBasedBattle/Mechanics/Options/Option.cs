using System;

namespace TurnBasedBattle
{
    abstract class Option
    {
        public Option(int posX, int posY)
        {
            PosX = posX;
            PosY = posY;

            OnHighlight += ShowDescription;
        }

        public Action OnHighlight { get; protected set; }

        private void ShowDescription()
        {
            GameInterface.ClearDisplayableInfo();
            Console.ForegroundColor = GameInterface.standartColor;

            if (Description != null)
            {
                string[] lines = Description.Split('\n');

                for (int i = 0; i < lines.Length; i++)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - (lines[i].Length / 2), 9 + i);
                    Console.Write(lines[i]);
                }
            }
        }

        public abstract int PosX { get; set; }

        public abstract int PosY { get; set; }

        public abstract bool HasSubOptions { get; }

        public abstract bool HasSuperOptions { get; }

        public abstract bool ActionIsAvailable { get; }

        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract void ActivateEffect();
    }
}
