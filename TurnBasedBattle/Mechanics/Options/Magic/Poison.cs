using System;

namespace TurnBasedBattle
{
    class Poison : Magic, IPoison
    {
        public Poison() : base(17, 3) { Mana = 40; OnHighlight += ShowDealtDamage; }

        public override string Name { get; } = "Poison";

        public override string Description => "Enemy loses 10 health in the end\nof it's turn 3 times";

        public override bool HasSubOptions { get; } = false;

        public override bool HasSuperOptions { get; } = true;

        public double Delta { get; } = 10;

        private int TurnCounter { get; set; } = 0;

        private void PoisonEnemy()
        {
            if (TurnCounter != 0)
            {
                Enemy.GainDamage(10 + Enemy.Defence);
            }
            else
            {
                Enemy.OnTheEndOfTheTurn -= PoisonEnemy;
            }

            TurnCounter--;
        }

        public override void ActivateEffect()
        {
            TurnCounter = 3;
            Enemy.OnTheEndOfTheTurn += PoisonEnemy;
        }

        private void ShowDealtDamage()
        {
            Console.SetCursorPosition(Enemy.EnmHealth.PosX, Enemy.EnmHealth.PosY + 2);
            Console.Write("-" + Delta + " x3", Console.ForegroundColor = ConsoleColor.Red);
        }
    }
}
