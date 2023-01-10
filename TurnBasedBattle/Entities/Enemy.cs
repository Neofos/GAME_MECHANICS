using System;
using System.Threading;

namespace TurnBasedBattle
{
    static class Enemy
    {
        static Enemy()
        {
            EnmHealth = new Health(Console.WindowLeft + 21, Console.WindowHeight - 5, 400);
            EnmHealth.HealthColor = ConsoleColor.DarkGreen;

            EnmMana = new Mana(Console.WindowWidth - 25, Console.WindowHeight - 5, 100);
            EnmMana.ManaColor = ConsoleColor.DarkCyan;

            IsAlive = true;
        }

        public static Action OnTheEndOfTheTurn { get; set; }

        public static Health EnmHealth { get; }

        public static Mana EnmMana { get; }

        public static int Strenght { get; set; } = 0;

        public static int Defence { get; set; } = 0;

        public static void GainDamage(double damage)
        {
            double currentValue = EnmHealth.Value;
            damage = damage - Defence >= 0 ? damage - Defence : 0;

            // Цикл плавности убывания здоровья
            for (; EnmHealth.Value > currentValue - damage && EnmHealth.Value > 0; EnmHealth.Value--)
            {
                GameInterface.UpdateEnemyShownStats();
                Thread.Sleep(10);
            }

            GameInterface.UpdateEnemyShownStats();
        }

        public static void LoseMana(double mana)
        {
            double currentValue = EnmMana.Value;

            // Цикл плавности убывания маны
            for (; EnmMana.Value > currentValue - mana; EnmMana.Value--)
            {
                GameInterface.UpdateEnemyShownStats();
                Thread.Sleep(10);
            }

            GameInterface.UpdateEnemyShownStats();
        }

        public static bool IsAlive { get; set; }

        public static bool ActionWasPerformed { get; set; }

        public static void StartTurn()
        {
            DealDamage(new Random().Next(10, 31));
        }

        public static void EndTurn()
        {
            OnTheEndOfTheTurn?.Invoke();
        }

        public static void DealDamage(double damage)
        {
            Player.GainDamage(damage + Strenght);
        }
    }
}
