namespace TurnBasedBattle
{
    class Defend : Option
    {
        public Defend(int posX = 4, int posY = 5) : base(posX, posY) { }

        public override int PosX { get; set; }

        public override int PosY { get; set; }

        public override bool HasSubOptions { get; } = false;

        public override bool HasSuperOptions { get; } = false;

        public override string Name { get; } = "Defend";

        public override string Description { get; }

        public override bool ActionIsAvailable => true;

        private int TurnCounter { get; set; } = 0;

        private void ReduceCounter()
        {
            if (TurnCounter == 0)
            {
                Player.Defence -= 10;
                Player.OnTheEndOfTheTurn -= ReduceCounter;
            }

            TurnCounter--;
        }

        public override void ActivateEffect()
        {
            TurnCounter = 1;
            Player.Defence += 10;
            Player.OnTheEndOfTheTurn += ReduceCounter;
        }
    }
}
