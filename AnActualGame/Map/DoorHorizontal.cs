using System;

namespace CubeAdventure
{
    class DoorHorizontal
    {
        public string DoorHorizontalName { get; set; }

        public static string DoorHorizontalAppearance { get; } = "▬";

        public ConsoleColor DoorHorizontalColor { get; set; }

        public int[] DoorHorizontalPosX { get; set; }

        public int DoorHorizontalPosY { get; set; }

        public Key OpeningKey { get; set; }

        public void DrawDoor()
        {
            for (int i = 0; i < DoorHorizontalPosX.Length; i++)
            {
                Console.SetCursorPosition(DoorHorizontalPosX[i], DoorHorizontalPosY);
                Console.Write(DoorHorizontalAppearance, Console.ForegroundColor = DoorHorizontalColor);
            }
        }

        public void OpenDoor()
        {
            for (int i = 0; i < DoorHorizontalPosX.Length; i++)
            {
                Console.SetCursorPosition(DoorHorizontalPosX[i], DoorHorizontalPosY);
                Console.Write(' ');
            }
        }
    }
}
