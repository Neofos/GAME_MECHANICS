using System;

namespace TurnBasedBattle
{
    class Bone : Item
    {
        public Bone() : base(29, 4) { }

        public override string Name { get; } = "Bone (r)";

        public override string Description => "Gives you a boner. Increases\nattack for the next 2 turns";

        public override bool HasSubOptions { get; } = false;

        public override bool HasSuperOptions { get; } = true;

        private int TurnCounter { get; set; } = 0;

        private void IncreaseStrenght()
        {
            int currentStrenght = Player.Strenght;

            TurnCounter--;
            if (TurnCounter > 0)
                Player.Strenght = new Random().Next(10, 26);
            else
            {
                Player.Strenght -= currentStrenght;
                Player.OnTheEndOfTheTurn -= IncreaseStrenght;
            }
        }

        public override void ActivateEffect()
        {
            TurnCounter = 3;
            Player.OnTheEndOfTheTurn += IncreaseStrenght;
        }
    }
}
