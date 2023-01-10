namespace TurnBasedBattle
{
    class hjqhwuajk : Item
    {
        public hjqhwuajk() : base(17, 5) { }

        public override string Name { get; } = "hjqhwuajk";

        public override string Description => "Don't use it";

        public override bool HasSubOptions { get; } = false;

        public override bool HasSuperOptions { get; } = true;

        public override void ActivateEffect()
        {
            Player.GainDamage(9999);
        }
    }
}
