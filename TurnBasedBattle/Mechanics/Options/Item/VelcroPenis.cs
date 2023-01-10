namespace TurnBasedBattle
{
    class VelcroPenis : Item
    {
        public VelcroPenis() : base(29, 5) { }

        public override string Name { get; } = "Velcro *****";

        public override string Description => "Allows you to use ***** Tearer\nfor an extra time";

        public override bool HasSubOptions { get; } = false;

        public override bool HasSuperOptions { get; } = true;

        public override void ActivateEffect()
        {
            Ulta.VelcroPenisActive = true;
        }
    }
}
