namespace TurnBasedBattle
{
    class Demotivator : Item
    {
        public Demotivator() : base(29, 3) { }

        public override string Name { get; } = "Demotivator";

        public override string Description => "Reduces enemy's damage\nand defense for 3 turns.";

        public override bool HasSubOptions { get; } = false;

        public override bool HasSuperOptions { get; } = true;

        private int TurnCounter { get; set; } = 0;

        private void DecreaseCounter()
        {
            TurnCounter--;

            if (TurnCounter == 0)
            {
                Enemy.Defence += 10;
                Enemy.Strenght += 10;
                Enemy.OnTheEndOfTheTurn -= DecreaseCounter;
            }
        }

        public override void ActivateEffect()
        {
            TurnCounter = 4;
            Enemy.Defence -= 10;
            Enemy.Strenght -= 10;
            Enemy.OnTheEndOfTheTurn += DecreaseCounter;
        }
    }
}
