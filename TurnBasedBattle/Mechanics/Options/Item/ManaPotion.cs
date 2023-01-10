using System;

namespace TurnBasedBattle
{
    class ManaPotion : Item
    {
        public ManaPotion() : base(17, 2) { OnHighlight += ShowReplenishedMana; }

        public override string Name { get; } = "M. Potion";

        public override string Description => "Replenishes some amount\nof your mana";

        public override bool HasSubOptions { get; } = false;

        public override bool HasSuperOptions { get; } = true;

        public double AmountToRegen { get; } = 45;

        public override void ActivateEffect()
        {
            Player.ReceiveMana(AmountToRegen);
        }

        private void ShowReplenishedMana()
        {
            Console.SetCursorPosition(Player.PlrMana.PosX, Player.PlrMana.PosY + 2);
            Console.Write("+45", Console.ForegroundColor = ConsoleColor.Blue);
        }
    }
}
