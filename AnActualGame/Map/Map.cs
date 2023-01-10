using System;
using System.Collections.Generic;

namespace CubeAdventure
{
    static class Map
    {
        private const int MAP_HEIGHT = 30;
        private const int MAP_WIDTH = 81;

        private static char[,] gameLevelMap = new char[MAP_HEIGHT, MAP_WIDTH]; // Массив хранит в себе всю карту

        public static DoorVertical[] verticalDoors;
        public static DoorHorizontal[] horizontalDoors;
        public static KeyKey[] keyKeys;
        public static BonusKey[] bonusKeys;
        public static Enemy[] enemies;

        public static List<ICollectable> Collectables { get; set; } = new List<ICollectable>();

        public static char[,] GameLevelMap
        {
            get
            {
                return gameLevelMap;
            }
        }

        // Имей ввиду, что изменение карты потребует изменения настройки дверей и других объектов.
        private static void InitMap()
        {   /* ■ - главный герой, @ - враг, ? - секретный предмет, ▐ ▬ - закрытая дверь
               K - ключевой ключ, B - бонусный ключ, X - шип, O - цель */
            string map = "█████████████████████████████████████████████████████████████████████████████████" +
                         "██         ██         ██         X                     X          X     ██     ██" +
                         "██    ?    ██         ██  @                X                  @         ▐   O  ██" +
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

                    if (i < mapStringToArray.Length)
                        i++;
                }
            }
        }

        private static void InitObjects(int i, int j)
        {
            if (gameLevelMap[i, j] == '■')
            {
                Player.PosX = j;
                Player.PosY = i;
                Player.WritePlayer();
                Console.SetCursorPosition(Console.CursorLeft + 1, i);
            }
            else if (gameLevelMap[i, j] == '@')
            {
                if (enemies == null)
                {
                    enemies = new Enemy[1];
                }
                else
                {
                    Array.Resize(ref enemies, enemies.Length + 1);
                }

                enemies[^1] = new Enemy();
                enemies[^1].PosX = j;
                enemies[^1].PosY = i;
                enemies[^1].WriteEnemy();
                Console.SetCursorPosition(Console.CursorLeft + 1, i);
            }
            else if (gameLevelMap[i, j] == '▐')
            {
                if (verticalDoors == null)
                {
                    verticalDoors = new DoorVertical[1];
                    verticalDoors[^1] = new DoorVertical();
                    verticalDoors[^1].DoorVerticalPosY = new int[1];
                    verticalDoors[^1].DoorVerticalPosX = j;
                    verticalDoors[^1].DoorVerticalPosY[^1] = i;
                }
                else
                {
                    Array.Resize(ref verticalDoors, verticalDoors.Length + 1);
                    verticalDoors[^1] = new DoorVertical();

                    if (verticalDoors[^1].DoorVerticalPosY == null)
                    {
                        verticalDoors[^1].DoorVerticalPosY = new int[1];
                    }

                    verticalDoors[^1].DoorVerticalPosX = j;
                    verticalDoors[^1].DoorVerticalPosY[^1] = i;

                    bool IsSameDoor = false;

                    if (verticalDoors.Length - 2 >= 0)
                        IsSameDoor = verticalDoors[^1].DoorVerticalPosX == verticalDoors[^2].DoorVerticalPosX &&
                                      verticalDoors[^1].DoorVerticalPosY[^1] == verticalDoors[^2].DoorVerticalPosY[^1] + 1;

                    if (IsSameDoor)
                    {
                        Array.Resize(ref verticalDoors, verticalDoors.Length - 1);
                        int[] temp = verticalDoors[^1].DoorVerticalPosY;
                        Array.Resize(ref temp, verticalDoors[^1].DoorVerticalPosY.Length + 1);
                        verticalDoors[^1].DoorVerticalPosY = temp;
                        verticalDoors[^1].DoorVerticalPosY[^1] = i;
                    }
                }
            }
            else if (gameLevelMap[i, j] == '▬')
            {
                if (horizontalDoors == null)
                {
                    horizontalDoors = new DoorHorizontal[1];
                    horizontalDoors[^1] = new DoorHorizontal();
                    horizontalDoors[^1].DoorHorizontalPosX = new int[1];
                    horizontalDoors[^1].DoorHorizontalPosY = i;
                    horizontalDoors[^1].DoorHorizontalPosX[^1] = j;
                }
                else
                {
                    Array.Resize(ref horizontalDoors, horizontalDoors.Length + 1);
                    horizontalDoors[^1] = new DoorHorizontal();

                    if (horizontalDoors[^1].DoorHorizontalPosX == null)
                    {
                        horizontalDoors[^1].DoorHorizontalPosX = new int[1];
                    }

                    horizontalDoors[^1].DoorHorizontalPosY = i;
                    horizontalDoors[^1].DoorHorizontalPosX[^1] = j;

                    bool IsSameDoor = false;

                    if (horizontalDoors.Length - 2 >= 0)
                        IsSameDoor = horizontalDoors[^1].DoorHorizontalPosY == horizontalDoors[^2].DoorHorizontalPosY &&
                                      horizontalDoors[^1].DoorHorizontalPosX[^1] == horizontalDoors[^2].DoorHorizontalPosX[^1] + 1;

                    if (IsSameDoor)
                    {
                        Array.Resize(ref horizontalDoors, horizontalDoors.Length - 1);
                        int[] temp = horizontalDoors[^1].DoorHorizontalPosX;
                        Array.Resize(ref temp, horizontalDoors[^1].DoorHorizontalPosX.Length + 1);
                        horizontalDoors[^1].DoorHorizontalPosX = temp;
                        horizontalDoors[^1].DoorHorizontalPosX[^1] = j;
                    }
                }
            }
            else if (gameLevelMap[i, j] == 'K')
            {
                if (keyKeys == null)
                {
                    keyKeys = new KeyKey[1];
                }
                else
                {
                    Array.Resize(ref keyKeys, keyKeys.Length + 1);
                }

                keyKeys[^1] = new KeyKey();
                keyKeys[^1].PosX = j;
                keyKeys[^1].PosY = i;
            }
            else if (gameLevelMap[i, j] == 'B')
            {
                if (bonusKeys == null)
                {
                    bonusKeys = new BonusKey[1];
                }
                else
                {
                    Array.Resize(ref bonusKeys, bonusKeys.Length + 1);
                }

                bonusKeys[^1] = new BonusKey();
                bonusKeys[^1].PosX = j;
                bonusKeys[^1].PosY = i;

            }
            else if (gameLevelMap[i, j] == 'O')
            {
                Goal.PosX = j;
                Goal.PosY = i;
                Goal.WriteGoal();
                Console.SetCursorPosition(Console.CursorLeft + 1, i);
            }

            if (gameLevelMap[i, j] != '█' && gameLevelMap[i, j] != ' ')
            {
                gameLevelMap[i, j] = ' ';
            }
        }

        public static void DrawMap()
        {
            InitMap();

            for (int i = 0; i < gameLevelMap.GetLength(0); i++)
            {
                for (int j = 0; j < gameLevelMap.GetLength(1); j++)
                {
                    switch (gameLevelMap[i, j])
                    {
                        case '█':
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                    }

                    Console.Write(gameLevelMap[i, j]);
                    InitObjects(i, j);
                }

                if (i != gameLevelMap.GetLength(0) - 1) // Переходы на новую строку
                    Console.WriteLine();
            }

            ConfigureKeys();
            ConfigureVerticalDoors();
            ConfigureHorizontalDoors();
        }

        private static void ConfigureKeys()
        {
            keyKeys[0].KeyName = "GoalKeyKey";
            keyKeys[0].KeyColor = ConsoleColor.DarkYellow;
            Collectables.Add(keyKeys[0]);

            keyKeys[1].KeyName = "GoalKey";
            keyKeys[1].KeyColor = ConsoleColor.DarkYellow;
            Collectables.Add(keyKeys[1]);

            for (int i = 0; i < keyKeys.Length; i++)
                keyKeys[i].DrawKey();

            bonusKeys[0].KeyName = "SecretDownKey";
            bonusKeys[0].KeyColor = ConsoleColor.Blue;
            Collectables.Add(bonusKeys[0]);

            bonusKeys[1].KeyName = "SecretUpKey";
            bonusKeys[1].KeyColor = ConsoleColor.Red;
            Collectables.Add(bonusKeys[1]);

            for (int i = 0; i < bonusKeys.Length; i++)
                bonusKeys[i].DrawKey();
        }

        private static void ConfigureVerticalDoors()
        {
            verticalDoors[0].DoorVerticalName = "GoalDoor";
            verticalDoors[0].DoorVerticalColor = ConsoleColor.DarkYellow;
            verticalDoors[0].OpeningKey = keyKeys[1];

            verticalDoors[1].DoorVerticalName = "SecretDownDoor";
            verticalDoors[1].DoorVerticalColor = ConsoleColor.Blue;
            verticalDoors[1].OpeningKey = bonusKeys[0];

            for (int i = 0; i < verticalDoors.Length; i++)
            {
                verticalDoors[i].DrawDoor();
            }
        }
        private static void ConfigureHorizontalDoors()
        {
            horizontalDoors[0].DoorHorizontalName = "SecretUpDoor";
            horizontalDoors[0].DoorHorizontalColor = ConsoleColor.Red;
            horizontalDoors[0].OpeningKey = bonusKeys[1];

            horizontalDoors[1].DoorHorizontalName = "GoalGoalDoor";
            horizontalDoors[1].DoorHorizontalColor = ConsoleColor.DarkYellow;
            horizontalDoors[1].OpeningKey = keyKeys[0];

            for (int i = 0; i < horizontalDoors.Length; i++)
            {
                horizontalDoors[i].DrawDoor();
            }
        }

        public static void CheckPlayerInventory()
        {
            for (int i = 0; i < verticalDoors.Length; i++)
            {
                if (Player.Inventory.Contains(verticalDoors[i].OpeningKey))
                {
                    Console.SetCursorPosition(verticalDoors[i].OpeningKey.PosX, verticalDoors[i].OpeningKey.PosY);
                    Console.Write(' ');
                    DestroyDoorVertical(i);
                }
            }

            for (int i = 0; i < horizontalDoors.Length; i++)
            {
                if (Player.Inventory.Contains(horizontalDoors[i].OpeningKey))
                {
                    Console.SetCursorPosition(horizontalDoors[i].OpeningKey.PosX, horizontalDoors[i].OpeningKey.PosY);
                    Console.Write(' ');
                    DestroyDoorHorizontal(i);
                }
            }
        }

        public static void DestroyDoorVertical(int index)
        {
            verticalDoors[index].OpenDoor();

            Collectables.Remove(verticalDoors[index].OpeningKey);
            Player.Inventory.Remove(verticalDoors[index].OpeningKey);

            if (verticalDoors[index].OpeningKey is KeyKey)
            {
                Predicate<KeyKey> pred = (KeyKey key) => key == verticalDoors[index].OpeningKey;
                int keyIndex = Array.FindIndex(keyKeys, pred);
                KeyKey[] tempKeyArray = new KeyKey[keyKeys.Length - 1];
                Array.Copy(keyKeys, 0, tempKeyArray, 0, keyIndex);
                Array.Copy(keyKeys, keyIndex + 1, tempKeyArray, keyIndex, keyKeys.Length - 1 - keyIndex);
                keyKeys = tempKeyArray;
            }
            else if (verticalDoors[index].OpeningKey is BonusKey)
            {
                Predicate<BonusKey> pred = (BonusKey key) => key == verticalDoors[index].OpeningKey;
                int keyIndex = Array.FindIndex(bonusKeys, pred);
                BonusKey[] tempKeyArray = new BonusKey[bonusKeys.Length - 1];
                Array.Copy(bonusKeys, 0, tempKeyArray, 0, keyIndex);
                Array.Copy(bonusKeys, keyIndex + 1, tempKeyArray, keyIndex, bonusKeys.Length - 1 - keyIndex);
                bonusKeys = tempKeyArray;
            }

            DoorVertical[] tempDoorArray = new DoorVertical[verticalDoors.Length - 1];
            Array.Copy(verticalDoors, 0, tempDoorArray, 0, index);
            Array.Copy(verticalDoors, index + 1, tempDoorArray, index, verticalDoors.Length - 1 - index);
            verticalDoors = tempDoorArray;
        }

        public static void DestroyDoorHorizontal(int index)
        {
            horizontalDoors[index].OpenDoor();

            Collectables.Remove(horizontalDoors[index].OpeningKey);
            Player.Inventory.Remove(horizontalDoors[index].OpeningKey);

            if (horizontalDoors[index].OpeningKey is KeyKey)
            {
                Predicate<KeyKey> pred = (KeyKey key) => key == horizontalDoors[index].OpeningKey;
                int keyIndex = Array.FindIndex(keyKeys, pred);
                KeyKey[] tempKeyArray = new KeyKey[keyKeys.Length - 1];
                Array.Copy(keyKeys, 0, tempKeyArray, 0, keyIndex);
                Array.Copy(keyKeys, keyIndex + 1, tempKeyArray, keyIndex, keyKeys.Length - 1 - keyIndex);
                keyKeys = tempKeyArray;
            }
            else if (horizontalDoors[index].OpeningKey is BonusKey)
            {
                Predicate<BonusKey> pred = (BonusKey key) => key == horizontalDoors[index].OpeningKey;
                int keyIndex = Array.FindIndex(bonusKeys, pred);
                BonusKey[] tempKeyArray = new BonusKey[bonusKeys.Length - 1];
                Array.Copy(bonusKeys, 0, tempKeyArray, 0, keyIndex);
                Array.Copy(bonusKeys, keyIndex + 1, tempKeyArray, keyIndex, bonusKeys.Length - 1 - keyIndex);
                bonusKeys = tempKeyArray;
            }

            DoorHorizontal[] tempDoorArray = new DoorHorizontal[horizontalDoors.Length - 1];
            Array.Copy(horizontalDoors, 0, tempDoorArray, 0, index);
            Array.Copy(horizontalDoors, index + 1, tempDoorArray, index, horizontalDoors.Length - 1 - index);
            horizontalDoors = tempDoorArray;
        }
    }
}
