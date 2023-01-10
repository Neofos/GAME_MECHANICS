using System;
using System.Text;

namespace CharacterMovement
{
    static class Map
    {
        private const int mapHeight = 30;
        private const int mapWidth = 81;

        private static char[,] gameLevelMap = new char[mapHeight, mapWidth]; // Массив хранит в себе всю карту
        public static char[,] GameLevelMap
        {
            get
            {
                return gameLevelMap;
            }
        } // Удобно обращаться по индексам.

        // Информация о координатах персонажей передаётся в конструкторы соответствующих производных классов
        private static int mainHeroPositionX, mainHeroPositionY;
        public static int MainHeroPositionX
        {
            get
            {
                return mainHeroPositionX;
            }

            set
            {
                mainHeroPositionX = value;
            }
        }
        public static int MainHeroPositionY
        {
            get
            {
                return mainHeroPositionY;
            }

            set
            {
                mainHeroPositionY = value;
            }
        }

        private static int[] enemyPositionX = new int[0], enemyPositionY = new int[0];
        public static int[] EnemyPositionX
        {
            get
            {
                return enemyPositionX;
            }

            set
            {
                enemyPositionX = value;
            }
        }
        public static int[] EnemyPositionY
        {
            get
            {
                return enemyPositionY;
            }

            set
            {
                enemyPositionY = value;
            }
        }
        private static int enemyCount = 0;

        private static void InitMap()
        {   /* ■ - главный герой, @ - враг, ? - секретный предмет, ▐ ▬ - закрытая дверь
               K - ключевой ключ, B - бонусный ключ, X - шип, O - цель */
            string map = "█████████████████████████████████████████████████████████████████████████████████" +
                         "██         ██         ██         X                     X          X     ██     ██" +
                         "██    ?    ██  K      ██  @                X                  @         ▐   O  ██" +
                         "██         ██                       @                 X              @  ▐      ██" +
                         "█████▬▬▬▬████         ██       X                          @             █████████" +
                         "██         ██         ██    X              @     X              X   @          ██" +
                         "██ B       ██         ██              X                    X             @   B ██" +
                         "██         ██         ██  X                 X       @           @   X          ██" +
                         "██         ██         ███████████████████████████████████████████████████████████" +
                         "██                                 ██       X     X      X       ██            ██" +
                         "██         ██                      ██   X       X    X       X              K  ██" +
                         "██         ██                             X       X      X       ██            ██" +
                         "████     ████████████████████████████ X       X           X  ████████     ███████" +
                         "██                                 ██    X       X   X  X    ██                ██" +
                         "██                                 ██      X  X    X      X  ██                ██" +
                         "██                                 ██  X       X       X     ██        @       ██" +
                         "█████████████████▬▬▬▬▬█████████████████████████████████████████                ██" +
                         "██                                   ██         ██                             ██" +
                         "██       @                   @       ██    ?    ▐      @                       ██" +
                         "██                 @                 ██         ██                             ██" +
                         "██          @             @          ██████████████████████████                ██" +
                         "██     @        @     @       @      ██████████████████████████   @        @   ██" +
                         "██                 K                 ██████████████████████████                ██" +
                         "███████████████████████████████████████████████████████████████                ██" +
                         "██                         ██                  ██                              ██" +
                         "██                         ██                  ██                              ██" +
                         "██  ■            ██        ██        ██        ██        ██                    ██" +
                         "██               ██                  ██                  ██                    ██" +
                         "██               ██                  ██                  ██                    ██" +
                         "█████████████████████████████████████████████████████████████████████████████████";

            char[] mapStringToArray = map.ToCharArray();

            int i = 0;
            for (int j = 0; j < gameLevelMap.GetLength(0); j++)
            {
                for (int k = 0; k < gameLevelMap.GetLength(1); k++)
                {
                    gameLevelMap[j, k] = mapStringToArray[i];

                    InitObjects(ref mapStringToArray, ref i, ref k, ref j);

                    if (i < mapStringToArray.Length)
                        i++;
                }
            }
        }

        private static void InitObjects(ref char[] mapStringToArray, ref int i, ref int k, ref int j)
        {
            if (mapStringToArray[i] == '■')
            {
                MainHeroPositionX = k;
                MainHeroPositionY = j;
            }
            else if (mapStringToArray[i] == '@')
            {
                enemyCount++;
                Array.Resize(ref enemyPositionX, enemyCount);
                EnemyPositionX[enemyCount - 1] = k;
                Array.Resize(ref enemyPositionY, enemyCount);
                EnemyPositionY[enemyCount - 1] = j;
            }
        }

        public static void DrawMap()
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.Unicode;

            InitMap();

            for (int i = 0; i < gameLevelMap.GetLength(0); i++)
            {
                for (int j = 0; j < gameLevelMap.GetLength(1); j++)
                {
                    Console.Write(gameLevelMap[i, j]);
                }

                if (i != gameLevelMap.GetLength(0) - 1) // Переходы на новую строку
                    Console.WriteLine();
            }
        }
    }
}
