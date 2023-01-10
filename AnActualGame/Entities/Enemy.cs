using System;

namespace CubeAdventure
{
    class Enemy
    {
        private string Appearance { get; set; } = "@";

        private ConsoleColor Color { get; set; } = ConsoleColor.Magenta;

        private Random random;

        public int PosX { get; set; }

        public int PosY { get; set; }

        public Enemy()
        {
            random = new Random(this.GetHashCode());
        }

        const int MOVE_DELAY = 10;

        public int MoveDelay { get; set; } = MOVE_DELAY;

        public void Controls()
        {
            int actionNumber = random.Next(1, 250);

            switch (actionNumber)
            {
                case 1 when MoveDelay == MOVE_DELAY && MovingLeftIsAvailable():
                    MoveLeft();
                    MoveDelay = 0;
                    break;
                case 2 when MoveDelay == MOVE_DELAY && MovingRightIsAvailable():
                    MoveRight();
                    MoveDelay = 0;
                    break;
                case 3 when MoveDelay == MOVE_DELAY && MovingUpIsAvailable():
                    MoveUp();
                    MoveDelay = 0;
                    break;
                case 4 when MoveDelay == MOVE_DELAY && MovingDownIsAvailable():
                    MoveDown();
                    MoveDelay = 0;
                    break;
                default: // Do nothing
                    break;
            }

            if (MoveDelay != MOVE_DELAY)
                MoveDelay++;
        }

        #region Moving Methods
        private void MoveLeft()
        {
            PosX--;

            WriteEnemy();

            Console.SetCursorPosition(PosX + 1, PosY);
            Console.Write(' ');
        }

        private void MoveRight()
        {
            PosX++;

            WriteEnemy();

            Console.SetCursorPosition(PosX - 1, PosY);
            Console.Write(' ');
        }

        private void MoveUp()
        {
            PosY--;

            WriteEnemy();

            Console.SetCursorPosition(PosX, PosY + 1);
            Console.Write(' ');
        }

        private void MoveDown()
        {
            PosY++;

            WriteEnemy();

            Console.SetCursorPosition(PosX, PosY - 1);
            Console.Write(' ');
        }
        #endregion

        #region Checking For Moving Ableness
        private bool MovingRightIsAvailable()
        {
            #region Wall
            bool wallIsMet = Map.GameLevelMap[PosY, PosX + 1] == '█';
            if (wallIsMet)
            {
                return false;
            }
            #endregion

            #region Vertical Door
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
            #endregion

            #region Horizontal Door
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
            #endregion

            #region Item
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
            #endregion

            #region Enemy
            bool enemyIsNear = false;
            for (int i = 0; i < Map.enemies.Length; i++)
            {
                if (PosX + 1 == Map.enemies[i].PosX && PosY == Map.enemies[i].PosY)
                {
                    enemyIsNear = true;
                    break;
                }
            }

            if (enemyIsNear)
            {
                return false;
            }
            #endregion

            #region Goal
            bool goalIsMet = PosX + 1 == Goal.PosX && PosY == Goal.PosY;

            if (goalIsMet)
            {
                return false;
            }
            #endregion

            return true;
        }

        private bool MovingLeftIsAvailable()
        {
            #region Wall
            bool wallIsMet = Map.GameLevelMap[PosY, PosX - 1] == '█';
            if (wallIsMet)
            {
                return false;
            }
            #endregion

            #region Vertical Door
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
            #endregion

            #region Horizontal Door
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
            #endregion

            #region Item
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
            #endregion

            #region Enemy
            bool enemyIsNear = false;
            for (int i = 0; i < Map.enemies.Length; i++)
            {
                if (PosX - 1 == Map.enemies[i].PosX && PosY == Map.enemies[i].PosY)
                {
                    enemyIsNear = true;
                    break;
                }
            }

            if (enemyIsNear)
            {
                return false;
            }
            #endregion

            #region Goal
            bool goalIsMet = PosX - 1 == Goal.PosX && PosY == Goal.PosY;

            if (goalIsMet)
            {
                return false;
            }
            #endregion

            return true;
        }

        private bool MovingUpIsAvailable()
        {
            #region Wall
            bool wallIsMet = Map.GameLevelMap[PosY - 1, PosX] == '█';
            if (wallIsMet)
            {
                return false;
            }
            #endregion

            #region Vertical Door
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
            #endregion

            #region Horizontal Door
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
            #endregion

            #region Item
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
            #endregion

            #region Enemy
            bool enemyIsNear = false;
            for (int i = 0; i < Map.enemies.Length; i++)
            {
                if (PosX == Map.enemies[i].PosX && PosY - 1 == Map.enemies[i].PosY)
                {
                    enemyIsNear = true;
                    break;
                }
            }

            if (enemyIsNear)
            {
                return false;
            }
            #endregion

            #region Goal
            bool goalIsMet = PosX == Goal.PosX && PosY - 1 == Goal.PosY;

            if (goalIsMet)
            {
                return false;
            }
            #endregion

            return true;
        }

        private bool MovingDownIsAvailable()
        {
            #region Wall
            bool wallIsMet = Map.GameLevelMap[PosY + 1, PosX] == '█';
            if (wallIsMet)
            {
                return false;
            }
            #endregion

            #region Vertical Door
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
            #endregion

            #region Horizontal Door
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
            #endregion

            #region Item
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
            #endregion

            #region Enemy
            bool enemyIsNear = false;
            for (int i = 0; i < Map.enemies.Length; i++)
            {
                if (PosX == Map.enemies[i].PosX && PosY + 1 == Map.enemies[i].PosY)
                {
                    enemyIsNear = true;
                    break;
                }
            }

            if (enemyIsNear)
            {
                return false;
            }
            #endregion

            #region Goal
            bool goalIsMet = PosX == Goal.PosX && PosY + 1 == Goal.PosY;

            if (goalIsMet)
            {
                return false;
            }
            #endregion
            return true;
        }
        #endregion

        public void WriteEnemy()
        {
            Console.SetCursorPosition(PosX, PosY);
            Console.Write(Appearance, Console.ForegroundColor = Color);
            Console.SetCursorPosition(PosX, PosY);
        }
    }
}
