namespace TurnBasedBattle
{
    class Freeze : Magic
    {
        public Freeze() : base(17, 2) { Mana = 35; }

        public override string Name { get; } = "Freeze";

        public override string Description => "Makes enemy skip it's turn\nYou recieve an extra turn";

        public override bool HasSubOptions { get; } = false;

        public override bool HasSuperOptions { get; } = true;

        private int TurnCount { get; set; } = 0;

        private void TurnReduce()
        {
            if (TurnCount == 0)
            {
                Player.OnTheEndOfTheTurn -= Player.StartTurn;
                Player.OnTheEndOfTheTurn -= TurnReduce;
            }
            else if (TurnCount == 1)
            {
                Player.OnTheEndOfTheTurn += Player.StartTurn;
            }

            TurnCount--;
        }

        public override void ActivateEffect()
        {
            Player.LoseMana(Mana);
            TurnCount = 1;
            Player.OnTheEndOfTheTurn += TurnReduce;
        }
    }
}
