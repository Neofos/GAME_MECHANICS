using System;
using System.Collections.Generic;
using System.Threading;

namespace TurnBasedBattle
{
    static class Player
    {
        static Player()
        {
            PlrHealth = new Health(Console.WindowLeft + 28, Console.WindowTop + 2, 100);
            PlrHealth.HealthColor = ConsoleColor.DarkGreen;

            PlrMana = new Mana(Console.WindowWidth - 19, Console.WindowTop + 2, 100);
            PlrMana.ManaColor = ConsoleColor.DarkCyan;

            IsAlive = true;

            options = new List<Option>();
            options.Add(new Attack());
            options.Add(new Magic());
            options.Add(new Item());
            options.Add(new Defend());

            OnTheStartOfTheTurn += () => PlrMana.Value += 20;
        }

        public static Action OnTheEndOfTheTurn { get; set; }

        public static Action OnTheStartOfTheTurn { get; set; }

        public static Health PlrHealth { get; }

        public static Mana PlrMana { get; }

        public static int Strenght { get; set; } = 0;

        public static int Defence { get; set; } = 0;

        public static void DealDamage(double damage)
        {
            Enemy.GainDamage(damage + Strenght);
        }

        public static void GainDamage(double damage)
        {
            double currentValue = PlrHealth.Value;
            damage = damage - Defence >= 0 ? damage - Defence : 0;

            // Цикл плавности убывания здоровья
            for (; PlrHealth.Value > currentValue - damage && PlrHealth.Value > 0; --PlrHealth.Value)
            {
                GameInterface.UpdatePlayerShownStats();
                Thread.Sleep(10);
            }

            GameInterface.UpdatePlayerShownStats();
        }

        public static void ReceiveHealth(double health)
        {
            double currentValue = PlrHealth.Value;

            // Цикл плавности убывания здоровья
            for (; PlrHealth.Value < currentValue + health; ++PlrHealth.Value)
            {
                GameInterface.UpdatePlayerShownStats();
                Thread.Sleep(10);
            }

            GameInterface.UpdatePlayerShownStats();
        }

        public static void ReceiveMana(double mana)
        {
            double currentValue = PlrMana.Value;

            // Цикл плавности убывания здоровья
            for (; PlrMana.Value < currentValue + mana; ++PlrMana.Value)
            {
                GameInterface.UpdatePlayerShownStats();
                Thread.Sleep(10);
            }

            GameInterface.UpdatePlayerShownStats();
        }

        public static void LoseMana(double mana)
        {
            double currentValue = PlrMana.Value;

            // Цикл плавности убывания маны
            for (; PlrMana.Value > currentValue - mana; --PlrMana.Value)
            {
                GameInterface.UpdatePlayerShownStats();
                Thread.Sleep(10);
            }

            GameInterface.UpdatePlayerShownStats();
        }

        public static bool IsAlive { get; set; }

        public static bool ActionWasPerformed { get; set; }

        private static List<Option> options;

        public static List<Option> Options { get => options; }

        public static void StartTurn()
        {
            ActionWasPerformed = false;
            OnTheStartOfTheTurn?.Invoke();
            GameInterface.ChooseOption(options);
        }

        public static void EndTurn()
        {
            OnTheEndOfTheTurn?.Invoke();
        }
    }
}
