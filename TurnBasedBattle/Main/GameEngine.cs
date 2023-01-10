using System;
using System.Runtime.Versioning;
using System.Threading;

namespace TurnBasedBattle
{
    static class GameEngine
    {
        [SupportedOSPlatform("windows")]
        static void Main()
        {
            // -------------------= ПЕРВОНАЧАЛЬНАЯ НАСТРОЙКА =------------------- //
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.WindowWidth = 60;
            Console.WindowHeight = 20;
            // -------------------============================------------------- //

            // Рисование всего интерфейса игры
            GameInterface.DrawInterface();

            bool gameMustGoOn = Player.IsAlive && Enemy.IsAlive;
            // Весь цикл игры
            while (gameMustGoOn)
            {
                Player.StartTurn();
                Player.EndTurn();

                Enemy.StartTurn();
                Enemy.EndTurn();

                gameMustGoOn = Player.PlrHealth.Value > 0 && Enemy.EnmHealth.Value > 0;
            }

            Win();
        }

        static void Win()
        {
            Console.Clear();
            string finalText = Player.PlrHealth.Value > 0 ? "You Win!" : "Enemy Win!";

            ConsoleColor textColor = (ConsoleColor)1;

            ConsoleKeyInfo key = default;
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey(true);
            }

            while (key.Key != ConsoleKey.Enter)
            {
                Console.SetCursorPosition((Console.WindowWidth / 2) - (finalText.Length / 2),
                                          (Console.WindowHeight / 2) - (1 / 2) - 1);

                Console.Write(finalText, Console.ForegroundColor = (finalText == "You Win!" ? textColor : ConsoleColor.DarkRed));

                if ((int)textColor + 1 > 15)
                    textColor = (ConsoleColor)1;
                else
                    textColor++;

                Thread.Sleep(50);

                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true);
                }
            }
        }
    }
}
