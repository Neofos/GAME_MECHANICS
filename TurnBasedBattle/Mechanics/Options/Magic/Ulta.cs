using System;

namespace TurnBasedBattle
{
    class Ulta : Magic
    {
        public Ulta() : base(17, 5) { Mana = 75; OnHighlight += ShowDealtDamage; }

        public override string Name { get; } = "***** Tearer";

        public override string Description => "Performs an ultimate spell that deals\nhigh amount of damage to the enemy";

        public override bool HasSubOptions { get; } = false;

        public override bool HasSuperOptions { get; } = true;

        public static bool VelcroPenisActive { get; set; } = false;

        public override bool ActionIsAvailable
        {
            get
            {
                return Player.PlrMana.Value >= Mana || VelcroPenisActive;
            }
        }

        public double Damage { get; } = new Random().Next(70, 101);

        public override void ActivateEffect()
        {
            if (VelcroPenisActive)
            {
                Player.DealDamage(Damage);
                Ulta.VelcroPenisActive = false;
            }
            else
            {
                Player.LoseMana(Mana);
                Player.DealDamage(Damage);
            }
        }

        private void ShowDealtDamage()
        {
            int minDamage = (70 + Player.Strenght - Enemy.Defence) > 0 ? 70 + Player.Strenght - Enemy.Defence : 0,
                maxDamage = (100 + Player.Strenght - Enemy.Defence) > 0 ? 100 + Player.Strenght - Enemy.Defence : 0;

            Console.SetCursorPosition(Enemy.EnmHealth.PosX, Enemy.EnmHealth.PosY + 2);
            Console.Write("-" + minDamage + "-" + maxDamage, Console.ForegroundColor = ConsoleColor.Red);
        }
    }
}
