using System;

namespace TurnBasedBattle
{
    class Heal : Magic, IHeal
    {
        public Heal() : base(17, 4) { Mana = 50; OnHighlight += ShowReplenishedHealth; }

        public override string Name { get; } = "Heal";

        public override string Description => "Replenishes a great amount\nof your health";

        public override bool HasSubOptions { get; } = false;

        public override bool HasSuperOptions { get; } = true;

        public double AmountToHeal { get; } = new Random().Next(30, 51);

        public override void ActivateEffect()
        {
            Player.LoseMana(Mana);
            Player.ReceiveHealth(AmountToHeal);
        }

        private void ShowReplenishedHealth()
        {
            Console.SetCursorPosition(Player.PlrHealth.PosX, Player.PlrHealth.PosY + 2);
            Console.Write("+30-50", Console.ForegroundColor = ConsoleColor.Green);
        }
    }
}
