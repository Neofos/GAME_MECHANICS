using System;

namespace TurnBasedBattle
{
    class Apple : Item, IHeal
    {
        public Apple() : base(17, 3) { OnHighlight += ShowReplenishedHealth; }

        public override string Name { get; } = "Apple";

        public override string Description => "Replenishes a small amount\nof your health";

        public override bool HasSubOptions { get; } = false;

        public override bool HasSuperOptions { get; } = true;

        public double AmountToHeal { get; } = 5;

        public override void ActivateEffect()
        {
            Player.ReceiveHealth(AmountToHeal);
        }

        private void ShowReplenishedHealth()
        {
            Console.SetCursorPosition(Player.PlrHealth.PosX, Player.PlrHealth.PosY + 2);
            Console.Write("+5", Console.ForegroundColor = ConsoleColor.Green);
        }
    }
}
