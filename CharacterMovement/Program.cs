using System;
using System.Threading;

namespace CharacterMovement
{
    class Program
    {
        static object locker = new object();

        static void Main(string[] args)
        {
            Map.DrawMap();

            // Потоки нужны для того, чтобы получить возможность выполнять несколько процессов одновременно
            Thread enemyThread = new Thread(new ThreadStart(EnemyInit));
            enemyThread.Start();

            Thread characterThread = new Thread(new ThreadStart(CharacterInit));
            characterThread.Start();
        }

        public static void EnemyInit()
        {

            Being[] enemies = new EnemyCharacter[Map.EnemyPositionX.Length];
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new EnemyCharacter(Map.EnemyPositionX[i], Map.EnemyPositionY[i]); // Определение стартовых позиций врагов
            }

            lock (locker)
            {
                while (true)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        SetWindowSizeRight();
                        enemies[i].BeingMove();
                    }
                    Thread.Sleep(1000); // Скорость передвижения: 1 позиция в 1 секунду.
                }
            }
        }

        public static void CharacterInit()
        {
            Being mainCharacter = new MainCharacter();

            //lock (locker)
            {
                while (true)
                {
                    mainCharacter.BeingMove();
                }
            }
        }

        public static void SetWindowSizeRight() // Менять размер окна нельзя (за исключением полного экрана).
        {
            if (Console.WindowHeight != 30 || Console.WindowWidth != 120) // Защита от дебила.
            {
                Console.WindowHeight = 30;
                Console.WindowWidth = 120;
            }
        }
    }
}
