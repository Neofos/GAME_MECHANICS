using System;

namespace TurnBasedBattle
{
    class ThrowingKnife : Item
    {
        public ThrowingKnife() : base(17, 4) { OnHighlight += ShowDealtDamage; }

        public override string Name { get; } = "T. Knife";

        public override string Description => "Adds 15 damage to your\nattacks for the next turn";

        public override bool HasSubOptions { get; } = false;

        public override bool HasSuperOptions { get; } = true;

        public int Damage { get; } = 15;

        private int TurnCount { get; set; } = 0;

        private void TurnReduce()
        {
            TurnCount--;

            if (TurnCount == 0)
            {
                Player.Strenght -= 15;
                Player.OnTheEndOfTheTurn -= TurnReduce;
            }
        }

        public override void ActivateEffect()
        {
            TurnCount = 2;
            Player.Strenght += 15;

            Player.OnTheEndOfTheTurn += TurnReduce;
        }

        private void ShowDealtDamage()
        {
            int damage = (15 + Player.Strenght - Enemy.Defence) > 0 ? 15 + Player.Strenght - Enemy.Defence : 0;

            Console.SetCursorPosition(Enemy.EnmHealth.PosX, Enemy.EnmHealth.PosY + 2);
            Console.Write("-" + damage, Console.ForegroundColor = ConsoleColor.Red);
        }
    }
}
