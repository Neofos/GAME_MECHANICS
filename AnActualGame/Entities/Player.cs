using System;
using System.Collections.Generic;

namespace CubeAdventure
{
    static class Player
    {
        static readonly ConsoleColor color = ConsoleColor.Cyan;
        static readonly string appearance = "■";
        static int posX, posY;

        public static List<ICollectable> Inventory { get; set; } = new List<ICollectable>();

        public static int PosX
        {
            get => posX;

            set
            {
                posX = value;
            }
        }

        public static int PosY
        {
            get => posY;

            set
            {
                posY = value;
            }
        }

        public static string LastDirection { get; set; } = "Right";

        public static int MoveDelay { get; set; } = 4;

        public static Weapon PlrWeapon { get; } = new BasicPistol();

        public static void Controls(ConsoleKeyInfo pressedKey)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.LeftArrow when MainGame.PlrMoveTimer == MoveDelay && MovingLeftIsAvailable():
                    MoveLeft();
                    MainGame.PlrMoveTimer = 0;
                    break;
                case ConsoleKey.RightArrow when MainGame.PlrMoveTimer == MoveDelay && MovingRightIsAvailable():
                    MoveRight();
                    MainGame.PlrMoveTimer = 0;
                    break;
                case ConsoleKey.UpArrow when MainGame.PlrMoveTimer == MoveDelay && MovingUpIsAvailable():
                    MoveUp();
                    MainGame.PlrMoveTimer = 0;
                    break;
                case ConsoleKey.DownArrow when MainGame.PlrMoveTimer == MoveDelay && MovingDownIsAvailable():
                    MoveDown();
                    MainGame.PlrMoveTimer = 0;
                    break;
                case ConsoleKey.Z when MainGame.ShootingTimer == PlrWeapon.ShootingDelay:
                    bool shootingIsAvailable;
                    switch (LastDirection)
                    {
                        case "Right":
                            shootingIsAvailable = MovingRightIsAvailable();
                            break;
                        case "Left":
                            shootingIsAvailable = MovingLeftIsAvailable();
                            break;
                        case "Up":
                            shootingIsAvailable = MovingUpIsAvailable();
                            break;
                        case "Down":
                            shootingIsAvailable = MovingDownIsAvailable();
                            break;
                        default:
                            shootingIsAvailable = MovingRightIsAvailable();
                            break;
                    }
                    var itemIsNearAndWhat = ItemIsNear();
                    if (itemIsNearAndWhat.itemIsNear)
                    {
                        CollectThing((int)itemIsNearAndWhat.itemIndex);
                    }
                    else if (shootingIsAvailable)
                    {
                        CreateProjectile();
                        MainGame.ShootingTimer = 0;
                    }
                    break;
            }
        }

        public static void Fire()
        {
            PlrWeapon.Fire();
        }

        private static void CreateProjectile()
        {
            Array.Resize(ref PlrWeapon.projectiles, PlrWeapon.projectiles.Length + 1);
            PlrWeapon.projectiles[PlrWeapon.projectiles.Length - 1] =
                new Weapon.Projectile(PosX, PosY, LastDirection);
        }

        public static void WritePlayer()
        {
            Console.SetCursorPosition(PosX, PosY);
            Console.Write(appearance, Console.ForegroundColor = color);
            Console.SetCursorPosition(PosX, PosY);
        }

        #region Moving Methods
        private static void MoveLeft()
        {
            PosX--;

            WritePlayer();

            Console.SetCursorPosition(PosX + 1, PosY);
            Console.Write(' ');

            LastDirection = "Left";
        }

        private static void MoveRight()
        {
            PosX++;

            WritePlayer();

            Console.SetCursorPosition(PosX - 1, PosY);
            Console.Write(' ');

            LastDirection = "Right";
        }

        private static void MoveUp()
        {
            PosY--;

            WritePlayer();

            Console.SetCursorPosition(PosX, PosY + 1);
            Console.Write(' ');

            LastDirection = "Up";
        }

        private static void MoveDown()
        {
            PosY++;

            WritePlayer();

            Console.SetCursorPosition(PosX, PosY - 1);
            Console.Write(' ');

            LastDirection = "Down";
        }
        #endregion

        #region Moving Ableness Checking
        private static bool MovingRightIsAvailable()
        {
            bool wallIsMet = Map.GameLevelMap[posY, posX + 1] == '█';
            if (wallIsMet)
            {
                return false;
            }

            bool doorVerticalIsClosed = false;
            for (int i = 0; i < Map.verticalDoors.Length; i++)
            {
                if (PosX + 1 == Map.verticalDoors[i].DoorVerticalPosX)
                {
                    for (int j = 0; j < Map.verticalDoors[i].DoorVerticalPosY.Length; j++)
                    {
                        if (PosY == Map.verticalDoors[i].DoorVerticalPosY[j])
                        {
                            doorVerticalIsClosed = true;
                        }
                    }
                    break;
                }
                else
                {
                    doorVerticalIsClosed = false;
                }
            }

            if (doorVerticalIsClosed)
            {
                return false;
            }

            bool doorHorizontalIsClosed = false;
            for (int i = 0; i < Map.horizontalDoors.Length; i++)
            {
                if (PosY == Map.horizontalDoors[i].DoorHorizontalPosY)
                {
                    for (int j = 0; j < Map.horizontalDoors[i].DoorHorizontalPosX.Length; j++)
                    {
                        if (PosX + 1 == Map.horizontalDoors[i].DoorHorizontalPosX[j])
                        {
                            doorHorizontalIsClosed = true;
                        }
                    }
                    break;
                }
                else
                {
                    doorHorizontalIsClosed = false;
                }
            }

            if (doorHorizontalIsClosed)
            {
                return false;
            }

            bool itemIsNear = false;
            for (int i = 0; i < Map.Collectables.Count; i++)
            {
                if (PosX + 1 == Map.Collectables[i].PosX && PosY == Map.Collectables[i].PosY)
                {
                    itemIsNear = true;
                    break;
                }
            }

            if (itemIsNear)
            {
                return false;
            }

            return true;
        }

        private static bool MovingLeftIsAvailable()
        {
            bool wallIsMet = Map.GameLevelMap[posY, posX - 1] == '█';
            if (wallIsMet)
            {
                return false;
            }

            bool doorVerticalIsClosed = false;
            for (int i = 0; i < Map.verticalDoors.Length; i++)
            {
                if (PosX - 1 == Map.verticalDoors[i].DoorVerticalPosX)
                {
                    for (int j = 0; j < Map.verticalDoors[i].DoorVerticalPosY.Length; j++)
                    {
                        if (PosY == Map.verticalDoors[i].DoorVerticalPosY[j])
                        {
                            doorVerticalIsClosed = true;
                        }
                    }
                    break;
                }
                else
                {
                    doorVerticalIsClosed = false;
                }
            }

            if (doorVerticalIsClosed)
            {
                return false;
            }

            bool doorHorizontalIsClosed = false;
            for (int i = 0; i < Map.horizontalDoors.Length; i++)
            {
                if (PosY == Map.horizontalDoors[i].DoorHorizontalPosY)
                {
                    for (int j = 0; j < Map.horizontalDoors[i].DoorHorizontalPosX.Length; j++)
                    {
                        if (PosX - 1 == Map.horizontalDoors[i].DoorHorizontalPosX[j])
                        {
                            doorHorizontalIsClosed = true;
                        }
                    }
                    break;
                }
                else
                {
                    doorHorizontalIsClosed = false;
                }
            }

            if (doorHorizontalIsClosed)
            {
                return false;
            }

            bool itemIsNear = false;
            for (int i = 0; i < Map.Collectables.Count; i++)
            {
                if (PosX - 1 == Map.Collectables[i].PosX && PosY == Map.Collectables[i].PosY)
                {
                    itemIsNear = true;
                    break;
                }
            }

            if (itemIsNear)
            {
                return false;
            }

            return true;
        }

        private static bool MovingUpIsAvailable()
        {
            bool wallIsMet = Map.GameLevelMap[posY - 1, posX] == '█';
            if (wallIsMet)
            {
                return false;
            }

            bool doorVerticalIsClosed = false;
            for (int i = 0; i < Map.verticalDoors.Length; i++)
            {
                if (PosX == Map.verticalDoors[i].DoorVerticalPosX)
                {
                    for (int j = 0; j < Map.verticalDoors[i].DoorVerticalPosY.Length; j++)
                    {
                        if (PosY - 1 == Map.verticalDoors[i].DoorVerticalPosY[j])
                        {
                            doorVerticalIsClosed = true;
                        }
                    }
                    break;
                }
                else
                {
                    doorVerticalIsClosed = false;
                }
            }

            if (doorVerticalIsClosed)
            {
                return false;
            }

            bool doorHorizontalIsClosed = false;
            for (int i = 0; i < Map.horizontalDoors.Length; i++)
            {
                if (PosY - 1 == Map.horizontalDoors[i].DoorHorizontalPosY)
                {
                    for (int j = 0; j < Map.horizontalDoors[i].DoorHorizontalPosX.Length; j++)
                    {
                        if (PosX == Map.horizontalDoors[i].DoorHorizontalPosX[j])
                        {
                            doorHorizontalIsClosed = true;
                        }
                    }
                    break;
                }
                else
                {
                    doorHorizontalIsClosed = false;
                }
            }

            if (doorHorizontalIsClosed)
            {
                return false;
            }

            bool itemIsNear = false;
            for (int i = 0; i < Map.Collectables.Count; i++)
            {
                if (PosX == Map.Collectables[i].PosX && PosY - 1 == Map.Collectables[i].PosY)
                {
                    itemIsNear = true;
                    break;
                }
            }

            if (itemIsNear)
            {
                return false;
            }

            return true;
        }

        private static bool MovingDownIsAvailable()
        {
            bool wallIsMet = Map.GameLevelMap[posY + 1, posX] == '█';
            if (wallIsMet)
            {
                return false;
            }

            bool doorVerticalIsClosed = false;
            for (int i = 0; i < Map.verticalDoors.Length; i++)
            {
                if (PosX == Map.verticalDoors[i].DoorVerticalPosX)
                {
                    for (int j = 0; j < Map.verticalDoors[i].DoorVerticalPosY.Length; j++)
                    {
                        if (PosY + 1 == Map.verticalDoors[i].DoorVerticalPosY[j])
                        {
                            doorVerticalIsClosed = true;
                        }
                    }
                    break;
                }
                else
                {
                    doorVerticalIsClosed = false;
                }
            }

            if (doorVerticalIsClosed)
            {
                return false;
            }

            bool doorHorizontalIsClosed = false;
            for (int i = 0; i < Map.horizontalDoors.Length; i++)
            {
                if (PosY + 1 == Map.horizontalDoors[i].DoorHorizontalPosY)
                {
                    for (int j = 0; j < Map.horizontalDoors[i].DoorHorizontalPosX.Length; j++)
                    {
                        if (PosX == Map.horizontalDoors[i].DoorHorizontalPosX[j])
                        {
                            doorHorizontalIsClosed = true;
                        }
                    }
                    break;
                }
                else
                {
                    doorHorizontalIsClosed = false;
                }
            }

            if (doorHorizontalIsClosed)
            {
                return false;
            }

            bool itemIsNear = false;
            for (int i = 0; i < Map.Collectables.Count; i++)
            {
                if (PosX == Map.Collectables[i].PosX && PosY + 1 == Map.Collectables[i].PosY)
                {
                    itemIsNear = true;
                    break;
                }
            }

            if (itemIsNear)
            {
                return false;
            }

            return true;
        }
        #endregion

        public static void CollectThing(int index)
        {
            Inventory.Add(Map.Collectables[index]);
        }

        public static (bool itemIsNear, int? itemIndex) ItemIsNear()
        {
            for (int i = 0; i < Map.Collectables.Count; i++)
            {
                if (PosX + 1 == Map.Collectables[i].PosX && PosY == Map.Collectables[i].PosY)
                {
                    return (true, i);
                }
                else if (PosX - 1 == Map.Collectables[i].PosX && PosY == Map.Collectables[i].PosY)
                {
                    return (true, i);
                }
                else if (PosX == Map.Collectables[i].PosX && PosY + 1 == Map.Collectables[i].PosY)
                {
                    return (true, i);
                }
                else if (PosX == Map.Collectables[i].PosX && PosY - 1 == Map.Collectables[i].PosY)
                {
                    return (true, i);
                }
            }

            return (false, null);
        }
    }
}
