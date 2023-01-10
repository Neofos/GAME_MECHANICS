using System;
using System.Collections.Generic;

namespace TurnBasedBattle
{
    class Attack : Option
    {
        static Attack()
        {
            attacks = new List<Option>();
            attacks.Add(new Slice());
            attacks.Add(new Stab());
            attacks.Add(new Combo());
        }

        public Attack(int posX = 4, int posY = 2) : base(posX, posY) { OnHighlight += ShowDealtDamage; }

        public override string Name { get; } = "Attack";

        public override int PosX { get; set; }

        public override int PosY { get; set; }

        public override bool HasSubOptions { get; } = true;

        public override bool HasSuperOptions { get; } = false;

        protected int minDmg, maxDmg;

        private static List<Option> attacks;

        public static List<Option> Attacks { get => attacks; }

        public override string Description { get; }

        public override bool ActionIsAvailable => true;

        public override void ActivateEffect()
        {
            GameInterface.ChooseOption(Attack.Attacks);
        }

        private void ShowDealtDamage()
        {
            int minDamage = (minDmg + Player.Strenght - Enemy.Defence) > 0 ? minDmg + Player.Strenght - Enemy.Defence : 0,
                maxDamage = (maxDmg + Player.Strenght - Enemy.Defence) > 0 ? maxDmg + Player.Strenght - Enemy.Defence : 0;

            if (minDmg != 0 && maxDmg != 0)
            {
                Console.SetCursorPosition(Enemy.EnmHealth.PosX, Enemy.EnmHealth.PosY + 2);
                Console.Write("-" + minDamage + "-" + maxDamage, Console.ForegroundColor = ConsoleColor.Red);
            }
        }
    }
}
