using System;

namespace CubeAdventure
{
    class BasicPistol : Weapon
    {
        public override int ShootingDelay { get; } = 20;

        public override void Fire()
        {
            for (int i = 0; i < projectiles.Length; i++)
            {
                switch (projectiles[i].Direction)
                {
                    case "Right":
                        if (projectiles[i].FlyingDelay == Projectile.FLYING_DELAY_LEFTRIGHT)
                        {
                            FireRight(i);
                            if (i >= 0 && i < projectiles.Length)
                                projectiles[i].FlyingDelay = 0;
                        }
                        break;
                    case "Left":
                        if (projectiles[i].FlyingDelay == Projectile.FLYING_DELAY_LEFTRIGHT)
                        {
                            FireLeft(i);
                            if (i >= 0 && i < projectiles.Length)
                                projectiles[i].FlyingDelay = 0;
                        }
                        break;
                    case "Up":
                        if (projectiles[i].FlyingDelay == Projectile.FLYING_DELAY_UPDOWN)
                        {
                            FireUp(i);
                            if (i >= 0 && i < projectiles.Length)
                                projectiles[i].FlyingDelay = 0;
                        }
                        break;
                    case "Down":
                        if (projectiles[i].FlyingDelay == Projectile.FLYING_DELAY_UPDOWN)
                        {
                            FireDown(i);
                            if (i >= 0 && i < projectiles.Length)
                                projectiles[i].FlyingDelay = 0;
                        }
                        break;
                    default:
                        goto case "Right";
                }

                Kill();

                if (i >= 0 && i < projectiles.Length)
                {
                    if (projectiles[i].Direction == "Right" || projectiles[i].Direction == "Left")
                    {
                        if (projectiles[i].FlyingDelay != Weapon.Projectile.FLYING_DELAY_LEFTRIGHT)
                            projectiles[i].FlyingDelay++;
                    }
                    else if (projectiles[i].Direction == "Up" || projectiles[i].Direction == "Down")
                    {
                        if (projectiles[i].FlyingDelay != Weapon.Projectile.FLYING_DELAY_UPDOWN)
                            projectiles[i].FlyingDelay++;
                    }
                }
            }
        }

        private void FireRight(int i)
        {
            projectiles[i].ProjectilePosX++;
            Console.SetCursorPosition(projectiles[i].ProjectilePosX, projectiles[i].ProjectilePosY);
            Console.Write(Projectile.ProjectileIcon, Console.ForegroundColor = Projectile.ProjectileColor);

            if (projectiles[i].ProjectilePosX - 1 != Player.PosX)
            {
                Console.SetCursorPosition(projectiles[i].ProjectilePosX - 1, projectiles[i].ProjectilePosY);
                Console.Write(' ');
            }

            if (!ShootingRightIsAvailable(i))
            {
                Console.SetCursorPosition(projectiles[i].ProjectilePosX, projectiles[i].ProjectilePosY);
                Console.Write(' ');

                Projectile[] newProjectiles = new Projectile[projectiles.Length - 1];
                Array.Copy(projectiles, 0, newProjectiles, 0, i);
                Array.Copy(projectiles, i + 1, newProjectiles, i, projectiles.Length - 1 - i);
                projectiles = newProjectiles;
            }
        }

        private void FireLeft(int i)
        {
            projectiles[i].ProjectilePosX--;
            Console.SetCursorPosition(projectiles[i].ProjectilePosX, projectiles[i].ProjectilePosY);
            Console.Write(Projectile.ProjectileIcon, Console.ForegroundColor = Projectile.ProjectileColor);

            if (projectiles[i].ProjectilePosX + 1 != Player.PosX)
            {
                Console.SetCursorPosition(projectiles[i].ProjectilePosX + 1, projectiles[i].ProjectilePosY);
                Console.Write(' ');
            }

            if (!ShootingLeftIsAvailable(i))
            {
                Console.SetCursorPosition(projectiles[i].ProjectilePosX, projectiles[i].ProjectilePosY);
                Console.Write(' ');

                Projectile[] newProjectiles = new Projectile[projectiles.Length - 1];
                Array.Copy(projectiles, 0, newProjectiles, 0, i);
                Array.Copy(projectiles, i + 1, newProjectiles, i, projectiles.Length - 1 - i);
                projectiles = newProjectiles;
            }
        }

        private void FireUp(int i)
        {
            projectiles[i].ProjectilePosY--;
            Console.SetCursorPosition(projectiles[i].ProjectilePosX, projectiles[i].ProjectilePosY);
            Console.Write(Projectile.ProjectileIcon, Console.ForegroundColor = Projectile.ProjectileColor);

            if (projectiles[i].ProjectilePosY + 1 != Player.PosY)
            {
                Console.SetCursorPosition(projectiles[i].ProjectilePosX, projectiles[i].ProjectilePosY + 1);
                Console.Write(' ');
            }

            if (!ShootingUpIsAvailable(i))
            {
                Console.SetCursorPosition(projectiles[i].ProjectilePosX, projectiles[i].ProjectilePosY);
                Console.Write(' ');

                Projectile[] newProjectiles = new Projectile[projectiles.Length - 1];
                Array.Copy(projectiles, 0, newProjectiles, 0, i);
                Array.Copy(projectiles, i + 1, newProjectiles, i, projectiles.Length - 1 - i);
                projectiles = newProjectiles;
            }
        }

        private void FireDown(int i)
        {
            projectiles[i].ProjectilePosY++;
            Console.SetCursorPosition(projectiles[i].ProjectilePosX, projectiles[i].ProjectilePosY);
            Console.Write(Projectile.ProjectileIcon, Console.ForegroundColor = Projectile.ProjectileColor);

            if (projectiles[i].ProjectilePosY - 1 != Player.PosY)
            {
                Console.SetCursorPosition(projectiles[i].ProjectilePosX, projectiles[i].ProjectilePosY - 1);
                Console.Write(' ');
            }

            if (!ShootingDownIsAvailable(i))
            {
                Console.SetCursorPosition(projectiles[i].ProjectilePosX, projectiles[i].ProjectilePosY);
                Console.Write(' ');

                Projectile[] newProjectiles = new Projectile[projectiles.Length - 1];
                Array.Copy(projectiles, 0, newProjectiles, 0, i);
                Array.Copy(projectiles, i + 1, newProjectiles, i, projectiles.Length - 1 - i);
                projectiles = newProjectiles;
            }
        }
    }
}
