using System;
using System.Linq;
using System.Threading;

namespace CharacterMovement
{
    class EnemyCharacter : Being
    {
        public EnemyCharacter(int characterPosX, int characterPosY)
        {
            beingIcon = "@";
            beingColor = ConsoleColor.Magenta;
            impassableObjects = new char[] { '█', '@', '■', 'K', 'B', 'O', '▬', '▐', 'X' };

            beingPosX = characterPosX;
            beingPosY = characterPosY;

            Thread randomThread = new Thread(new ThreadStart(GenerateRandom));
            randomThread.Start();
        }

        private static Random random;

        private static void GenerateRandom() // Обеспечивает случайность передвижения врагов.
        {
            while (true)
            {
                Random seed = new Random();
                random = new Random(seed.Next(0, 10000000));
                Thread.Sleep(200);
            }
        }

        public override void BeingMove() // Класс отвечает за движение врагов
        {
            int randomDirection;

            Console.SetCursorPosition(beingPosX, beingPosY);
            Console.Write(" "); // Курсор консоли автоматически сдвигается на позицию вправо, поэтому нужно сразу стирать следы.

            randomDirection = random.Next(1, 5);
            switch (randomDirection)
            {
                case 1:
                    {
                        beingPosX += 1;
                        if (impassableObjects.Contains(Map.GameLevelMap[beingPosY, beingPosX])) // Невозможность прохода через стены
                            beingPosX -= 1;
                        break;
                    }
                case 2:
                    {
                        beingPosX -= 1;
                        if (impassableObjects.Contains(Map.GameLevelMap[beingPosY, beingPosX]))
                            beingPosX += 1;
                        break;
                    }
                case 3:
                    {
                        beingPosY += 1;
                        if (impassableObjects.Contains(Map.GameLevelMap[beingPosY, beingPosX]))
                            beingPosY -= 1;
                        break;
                    }
                case 4:
                    {
                        beingPosY -= 1;
                        if (impassableObjects.Contains(Map.GameLevelMap[beingPosY, beingPosX]))
                            beingPosY += 1;
                        break;
                    }
            }
            Console.SetCursorPosition(beingPosX, beingPosY);
            Console.Write(beingIcon, Console.ForegroundColor = beingColor);
        }
    }
}
