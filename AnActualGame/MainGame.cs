using System;
using System.Text;
using System.Threading;
using WMPLib;

namespace CubeAdventure
{
    class MainGame
    {
        public static int ShootingTimer { get; set; } = Player.PlrWeapon.ShootingDelay;

        public static int PlrMoveTimer { get; set; } = Player.MoveDelay;

        public static bool GameIsOver = false;

        static void TimerTick()
        {
            if (Console.KeyAvailable)
            {
                Player.Controls(Console.ReadKey(true));
            }

            for (int i = 0; i < Map.enemies.Length; i++)
            {
                Map.enemies[i].Controls();
            }

            if (Player.PlrWeapon != null && Player.PlrWeapon.projectiles.Length != 0)
            {
                Player.Fire();
            }

            Map.CheckPlayerInventory();

            if (Player.PosX == Goal.PosX && Player.PosY == Goal.PosY)
            {
                GameIsOver = true;
                RollCredits();
                return;
            }

            Thread.Sleep(1);

            if (Player.PlrWeapon != null && ShootingTimer != Player.PlrWeapon.ShootingDelay)
                ShootingTimer++;

            if (PlrMoveTimer != Player.MoveDelay)
                PlrMoveTimer++;

            Console.CursorVisible = false;
        }

        static void RollCredits()
        {
            WindowsMediaPlayer wplayer = new WindowsMediaPlayer();
            wplayer.URL = AppContext.BaseDirectory + @"Sound\FUCKYOU.mp3";
            wplayer.controls.play();

            Console.Clear();
            string finalText = "You Win!";

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

                Console.ForegroundColor = textColor;
                Console.Write(finalText);

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

        static void Main(string[] args)
        {
            //-======== БАЗОВЫЕ ОПЦИИ ========-//
            Console.Title = "Cube Adventure";
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.Unicode;
            //-===============================-//

            Map.DrawMap();

            WindowsMediaPlayer wplayer = new WindowsMediaPlayer();
            wplayer.URL = AppContext.BaseDirectory + @"Sound\MuzikaKazaxa.mp3";
            wplayer.controls.play();

            while (!GameIsOver)
            {
                TimerTick();
            }
        }
    }
}
