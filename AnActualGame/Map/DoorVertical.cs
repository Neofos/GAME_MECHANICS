using System;

namespace CubeAdventure
{
    class DoorVertical
    {
        public string DoorVerticalName { get; set; }

        public static string DoorVerticalAppearance { get; } = "▐";

        public ConsoleColor DoorVerticalColor { get; set; }

        public int DoorVerticalPosX { get; set; }

        public int[] DoorVerticalPosY { get; set; }

        public Key OpeningKey { get; set; }

        public void DrawDoor()
        {
            for (int i = 0; i < DoorVerticalPosY.Length; i++)
            {
                Console.SetCursorPosition(DoorVerticalPosX, DoorVerticalPosY[i]);
                Console.Write(DoorVerticalAppearance, Console.ForegroundColor = DoorVerticalColor);
            }
        }

        public void OpenDoor()
        {
            for (int i = 0; i < DoorVerticalPosY.Length; i++)
            {
                Console.SetCursorPosition(DoorVerticalPosX, DoorVerticalPosY[i]);
                Console.Write(' ');
            }
        }
    }
}
