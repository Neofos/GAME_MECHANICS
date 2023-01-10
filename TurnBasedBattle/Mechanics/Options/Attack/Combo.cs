using System;

namespace TurnBasedBattle
{
    class Combo : Attack
    {
        public Combo() : base(17, 4)
        {
            minDmg = 40;
            maxDmg = 50;
            Damage = new Random().Next(minDmg, maxDmg + 1);
            OnHighlight += ShowExpendedMana;
        }

        public override string Name { get; } = "Combo";

        public override string Description { get; } = "Deals great damage to the enemy\nRequires mana to be performed";

        public override bool HasSubOptions { get; } = false;

        public override bool HasSuperOptions { get; } = true;

        public override bool ActionIsAvailable => Player.PlrMana.Value >= Mana;

        public double Damage { get; private set; }

        public double Mana { get; } = 45;

        public override void ActivateEffect()
        {
            Player.LoseMana(Mana);
            Player.DealDamage(Damage);
        }

        private void ShowExpendedMana()
        {
            Console.SetCursorPosition(Player.PlrMana.PosX, Player.PlrMana.PosY + 2);
            Console.Write("-" + Mana, Console.ForegroundColor = ConsoleColor.Red);
        }
    }
}
