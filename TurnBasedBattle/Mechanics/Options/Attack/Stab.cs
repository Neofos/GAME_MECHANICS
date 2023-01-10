using System;

namespace TurnBasedBattle
{
    class Stab : Attack
    {
        public Stab() : base(17, 3) { minDmg = 20; maxDmg = 40; Damage = new Random().Next(minDmg, maxDmg + 1); }

        public override string Name { get; } = "Stab";

        public override string Description => "Deals average damage to the enemy";

        public override bool HasSubOptions { get; } = false;

        public override bool HasSuperOptions { get; } = true;

        public double Damage { get; private set; }

        public override void ActivateEffect()
        {
            Player.DealDamage(Damage);
        }
    }
}
