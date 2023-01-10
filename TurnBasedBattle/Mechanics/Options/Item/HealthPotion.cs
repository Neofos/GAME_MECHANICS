using System;

namespace TurnBasedBattle
{
    class HealthPotion : Item, IHeal
    {
        public HealthPotion() : base(29, 2) { OnHighlight += ShowReplenishedHealth; }

        public override string Name { get; } = "H. Potion";

        public override string Description => "Replenishes some amount\nof your health";

        public override bool HasSubOptions { get; } = false;

        public override bool HasSuperOptions { get; } = true;

        public double AmountToHeal { get; } = 20;

        public override void ActivateEffect()
        {
            Player.ReceiveHealth(AmountToHeal);
        }

        private void ShowReplenishedHealth()
        {
            Console.SetCursorPosition(Player.PlrHealth.PosX, Player.PlrHealth.PosY + 2);
            Console.Write("+20", Console.ForegroundColor = ConsoleColor.Green);
        }
    }
}
