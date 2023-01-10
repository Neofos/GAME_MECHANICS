using System;

namespace CubeAdventure
{
    static class Goal
    {
        public static string GoalAppearance = "O";

        public static ConsoleColor GoalColor = ConsoleColor.Green;

        public static int PosX { get; set; }

        public static int PosY { get; set; }

        public static void WriteGoal()
        {
            Console.SetCursorPosition(PosX, PosY);
            Console.Write(GoalAppearance, Console.ForegroundColor = GoalColor);
            Console.SetCursorPosition(PosX, PosY);
        }
    }
}
