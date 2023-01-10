using System;
using System.Collections.Generic;

namespace TurnBasedBattle
{
    class Magic : Option
    {
        static Magic()
        {
            spells = new List<Option>();
            spells.Add(new Freeze());
            spells.Add(new Poison());
            spells.Add(new Heal());
            spells.Add(new Ulta());
        }

        public Magic(int posX = 4, int posY = 3) : base(posX, posY) { OnHighlight += ShowExpendedMana; }

        public override int PosX { get; set; }

        public override int PosY { get; set; }

        public override bool HasSubOptions { get; } = true;

        public override bool HasSuperOptions { get; } = false;

        public int Mana { get; protected set; }

        public override string Name { get; } = "Magic";

        public override string Description { get; }

        private static List<Option> spells;

        public static List<Option> Spells { get => spells; }

        public override bool ActionIsAvailable => Player.PlrMana.Value >= Mana;

        public override void ActivateEffect()
        {
            GameInterface.ChooseOption(Magic.Spells);
        }

        private void ShowExpendedMana()
        {
            if (Mana != 0)
            {
                Console.SetCursorPosition(Player.PlrMana.PosX, Player.PlrMana.PosY + 2);
                Console.Write("-" + Mana, Console.ForegroundColor = ConsoleColor.Red);
            }
        }
    }
}
