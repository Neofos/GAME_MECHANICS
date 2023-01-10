using System;

namespace TurnBasedBattle
{
    class Slice : Attack
    {
        public Slice() : base(17, 2) { minDmg = 15; maxDmg = 30; Damage = new Random().Next(minDmg, maxDmg + 1); }

        public override string Name { get; } = "Slice";

        public override string Description => "Deals average damage and imposes\nthe bleeding effect to the enemy";

        public override bool HasSubOptions { get; } = false;

        public override bool HasSuperOptions { get; } = true;

        public double Damage { get; private set; }

        public override void ActivateEffect()
        {
            Player.DealDamage(Damage);
        }
    }
}
