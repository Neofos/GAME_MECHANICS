using System;

namespace CubeAdventure
{
    abstract class Weapon
    {
        public abstract int ShootingDelay { get; }

        public abstract void Fire();

        public void Kill()
        {
            bool enemyIsKilled = false;
            for (int i = 0; i < Map.enemies.Length; i++)
            {
                for (int j = 0; j < projectiles.Length; j++)
                {
                    if (projectiles[j].ProjectilePosX == Map.enemies[i].PosX &&
                        projectiles[j].ProjectilePosY == Map.enemies[i].PosY)
                    {
                        Console.SetCursorPosition(Map.enemies[i].PosX, Map.enemies[i].PosY);
                        Console.Write(' ');

                        Enemy[] newEnemies = new Enemy[Map.enemies.Length - 1];
                        Array.Copy(Map.enemies, 0, newEnemies, 0, i);
                        Array.Copy(Map.enemies, i + 1, newEnemies, i, Map.enemies.Length - 1 - i);
                        Map.enemies = newEnemies;

                        Console.SetCursorPosition(projectiles[j].ProjectilePosX, projectiles[j].ProjectilePosY);
                        Console.Write(' ');

                        Projectile[] newProjectiles = new Projectile[projectiles.Length - 1];
                        Array.Copy(projectiles, 0, newProjectiles, 0, j);
                        Array.Copy(projectiles, j + 1, newProjectiles, j, projectiles.Length - 1 - j);
                        projectiles = newProjectiles;

                        enemyIsKilled = true;

                        break;
                    }
                }
                if (enemyIsKilled)
                    break;
            }
        }

        public Projectile[] projectiles = new Projectile[0];

        public class Projectile
        {
            public Projectile(int posX, int posY, string direction)
            {
                ProjectilePosX = posX;
                ProjectilePosY = posY;
                Direction = direction;
            }

            public string Direction { get; } = "Right";

            public static ConsoleColor ProjectileColor { get; } = ConsoleColor.White;

            public static string ProjectileIcon { get; } = "▪";

            public int ProjectilePosX { get; set; }

            public int ProjectilePosY { get; set; }

            public const int FLYING_DELAY_LEFTRIGHT = 2;
            public const int FLYING_DELAY_UPDOWN = 4;

            public int FlyingDelay { get; set; } = FLYING_DELAY_LEFTRIGHT;
        }

        public bool ShootingRightIsAvailable(int projIndex)
        {
            int i = projIndex;
            bool wallIsMet = Map.GameLevelMap[projectiles[i].ProjectilePosY, projectiles[i].ProjectilePosX + 1] == '█';

            if (wallIsMet)
            {
                return false;
            }

            bool keyKeyIsMet = false;
            for (int j = 0; j < Map.keyKeys.Length; j++)
            {
                if (projectiles[i].ProjectilePosX + 1 == Map.keyKeys[j].PosX)
                {
                    if (projectiles[i].ProjectilePosY == Map.keyKeys[j].PosY)
                    {
                        keyKeyIsMet = true;
                        break;
                    }
                    else
                    {
                        keyKeyIsMet = false;
                    }
                }
                else
                {
                    keyKeyIsMet = false;
                }
            }

            if (keyKeyIsMet)
            {
                return false;
            }

            bool bonusKeyIsMet = false;
            for (int j = 0; j < Map.bonusKeys.Length; j++)
            {
                if (projectiles[i].ProjectilePosX + 1 == Map.bonusKeys[j].PosX)
                {
                    if (projectiles[i].ProjectilePosY == Map.bonusKeys[j].PosY)
                    {
                        bonusKeyIsMet = true;
                        break;
                    }
                    else
                    {
                        bonusKeyIsMet = false;
                    }
                }
                else
                {
                    bonusKeyIsMet = false;
                }
            }

            if (bonusKeyIsMet)
            {
                return false;
            }

            bool doorVerticalIsClosed = false;
            for (int j = 0; j < Map.verticalDoors.Length; j++)
            {
                if (projectiles[i].ProjectilePosX + 1 == Map.verticalDoors[j].DoorVerticalPosX)
                {
                    for (int k = 0; k < Map.verticalDoors[j].DoorVerticalPosY.Length; k++)
                    {
                        if (projectiles[i].ProjectilePosY == Map.verticalDoors[j].DoorVerticalPosY[k])
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
            for (int j = 0; j < Map.horizontalDoors.Length; j++)
            {
                if (projectiles[i].ProjectilePosY == Map.horizontalDoors[j].DoorHorizontalPosY)
                {
                    for (int k = 0; k < Map.horizontalDoors[j].DoorHorizontalPosX.Length; k++)
                    {
                        if (projectiles[i].ProjectilePosX + 1 == Map.horizontalDoors[j].DoorHorizontalPosX[k])
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

            bool goalIsMet = projectiles[i].ProjectilePosX + 1 == Goal.PosX && projectiles[i].ProjectilePosY == Goal.PosY;

            if (goalIsMet)
            {
                return false;
            }

            return true;
        }

        public bool ShootingLeftIsAvailable(int projIndex)
        {
            int i = projIndex;
            bool wallIsMet = Map.GameLevelMap[projectiles[i].ProjectilePosY, projectiles[i].ProjectilePosX - 1] == '█';

            if (wallIsMet)
            {
                return false;
            }

            bool keyKeyIsMet = false;
            for (int j = 0; j < Map.keyKeys.Length; j++)
            {
                if (projectiles[i].ProjectilePosX - 1 == Map.keyKeys[j].PosX)
                {
                    if (projectiles[i].ProjectilePosY == Map.keyKeys[j].PosY)
                    {
                        keyKeyIsMet = true;
                        break;
                    }
                    else
                    {
                        keyKeyIsMet = false;
                    }
                }
                else
                {
                    keyKeyIsMet = false;
                }
            }

            if (keyKeyIsMet)
            {
                return false;
            }

            bool bonusKeyIsMet = false;
            for (int j = 0; j < Map.bonusKeys.Length; j++)
            {
                if (projectiles[i].ProjectilePosX - 1 == Map.bonusKeys[j].PosX)
                {
                    if (projectiles[i].ProjectilePosY == Map.bonusKeys[j].PosY)
                    {
                        bonusKeyIsMet = true;
                        break;
                    }
                    else
                    {
                        bonusKeyIsMet = false;
                    }
                }
                else
                {
                    bonusKeyIsMet = false;
                }
            }

            if (bonusKeyIsMet)
            {
                return false;
            }

            bool doorVerticalIsClosed = false;
            for (int j = 0; j < Map.verticalDoors.Length; j++)
            {
                if (projectiles[i].ProjectilePosX - 1 == Map.verticalDoors[j].DoorVerticalPosX)
                {
                    for (int k = 0; k < Map.verticalDoors[j].DoorVerticalPosY.Length; k++)
                    {
                        if (projectiles[i].ProjectilePosY == Map.verticalDoors[j].DoorVerticalPosY[k])
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
            for (int j = 0; j < Map.horizontalDoors.Length; j++)
            {
                if (projectiles[i].ProjectilePosY == Map.horizontalDoors[j].DoorHorizontalPosY)
                {
                    for (int k = 0; k < Map.horizontalDoors[j].DoorHorizontalPosX.Length; k++)
                    {
                        if (projectiles[i].ProjectilePosX - 1 == Map.horizontalDoors[j].DoorHorizontalPosX[k])
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

            bool goalIsMet = projectiles[i].ProjectilePosX - 1 == Goal.PosX && projectiles[i].ProjectilePosY == Goal.PosY;

            if (goalIsMet)
            {
                return false;
            }

            return true;
        }

        public bool ShootingUpIsAvailable(int projIndex)
        {
            int i = projIndex;
            bool wallIsMet = Map.GameLevelMap[projectiles[i].ProjectilePosY - 1, projectiles[i].ProjectilePosX] == '█';

            if (wallIsMet)
            {
                return false;
            }

            bool keyKeyIsMet = false;
            for (int j = 0; j < Map.keyKeys.Length; j++)
            {
                if (projectiles[i].ProjectilePosX == Map.keyKeys[j].PosX)
                {
                    if (projectiles[i].ProjectilePosY - 1 == Map.keyKeys[j].PosY)
                    {
                        keyKeyIsMet = true;
                        break;
                    }
                    else
                    {
                        keyKeyIsMet = false;
                    }
                }
                else
                {
                    keyKeyIsMet = false;
                }
            }

            if (keyKeyIsMet)
            {
                return false;
            }

            bool bonusKeyIsMet = false;
            for (int j = 0; j < Map.bonusKeys.Length; j++)
            {
                if (projectiles[i].ProjectilePosX == Map.bonusKeys[j].PosX)
                {
                    if (projectiles[i].ProjectilePosY - 1 == Map.bonusKeys[j].PosY)
                    {
                        bonusKeyIsMet = true;
                        break;
                    }
                    else
                    {
                        bonusKeyIsMet = false;
                    }
                }
                else
                {
                    bonusKeyIsMet = false;
                }
            }

            if (bonusKeyIsMet)
            {
                return false;
            }

            bool doorVerticalIsClosed = false;
            for (int j = 0; j < Map.verticalDoors.Length; j++)
            {
                if (projectiles[i].ProjectilePosX == Map.verticalDoors[j].DoorVerticalPosX)
                {
                    for (int k = 0; k < Map.verticalDoors[j].DoorVerticalPosY.Length; k++)
                    {
                        if (projectiles[i].ProjectilePosY - 1 == Map.verticalDoors[j].DoorVerticalPosY[k])
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
            for (int j = 0; j < Map.horizontalDoors.Length; j++)
            {
                if (projectiles[i].ProjectilePosY - 1 == Map.horizontalDoors[j].DoorHorizontalPosY)
                {
                    for (int k = 0; k < Map.horizontalDoors[j].DoorHorizontalPosX.Length; k++)
                    {
                        if (projectiles[i].ProjectilePosX == Map.horizontalDoors[j].DoorHorizontalPosX[k])
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

            bool goalIsMet = projectiles[i].ProjectilePosX == Goal.PosX && projectiles[i].ProjectilePosY - 1 == Goal.PosY;

            if (goalIsMet)
            {
                return false;
            }

            return true;
        }

        public bool ShootingDownIsAvailable(int projIndex)
        {
            int i = projIndex;
            bool wallIsMet = Map.GameLevelMap[projectiles[i].ProjectilePosY + 1, projectiles[i].ProjectilePosX] == '█';

            if (wallIsMet)
            {
                return false;
            }

            bool keyKeyIsMet = false;
            for (int j = 0; j < Map.keyKeys.Length; j++)
            {
                if (projectiles[i].ProjectilePosX == Map.keyKeys[j].PosX)
                {
                    if (projectiles[i].ProjectilePosY + 1 == Map.keyKeys[j].PosY)
                    {
                        keyKeyIsMet = true;
                        break;
                    }
                    else
                    {
                        keyKeyIsMet = false;
                    }
                }
                else
                {
                    keyKeyIsMet = false;
                }
            }

            if (keyKeyIsMet)
            {
                return false;
            }

            bool bonusKeyIsMet = false;
            for (int j = 0; j < Map.bonusKeys.Length; j++)
            {
                if (projectiles[i].ProjectilePosX == Map.bonusKeys[j].PosX)
                {
                    if (projectiles[i].ProjectilePosY + 1 == Map.bonusKeys[j].PosY)
                    {
                        bonusKeyIsMet = true;
                        break;
                    }
                    else
                    {
                        bonusKeyIsMet = false;
                    }
                }
                else
                {
                    bonusKeyIsMet = false;
                }
            }

            if (bonusKeyIsMet)
            {
                return false;
            }

            bool doorVerticalIsClosed = false;
            for (int j = 0; j < Map.verticalDoors.Length; j++)
            {
                if (projectiles[i].ProjectilePosX == Map.verticalDoors[j].DoorVerticalPosX)
                {
                    for (int k = 0; k < Map.verticalDoors[j].DoorVerticalPosY.Length; k++)
                    {
                        if (projectiles[i].ProjectilePosY + 1 == Map.verticalDoors[j].DoorVerticalPosY[k])
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
            for (int j = 0; j < Map.horizontalDoors.Length; j++)
            {
                if (projectiles[i].ProjectilePosY + 1 == Map.horizontalDoors[j].DoorHorizontalPosY)
                {
                    for (int k = 0; k < Map.horizontalDoors[j].DoorHorizontalPosX.Length; k++)
                    {
                        if (projectiles[i].ProjectilePosX == Map.horizontalDoors[j].DoorHorizontalPosX[k])
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

            bool goalIsMet = projectiles[i].ProjectilePosX == Goal.PosX && projectiles[i].ProjectilePosY + 1 == Goal.PosY;

            if (goalIsMet)
            {
                return false;
            }

            return true;
        }
    }
}
