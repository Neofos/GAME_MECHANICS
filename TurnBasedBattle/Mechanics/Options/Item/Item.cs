using System;
using System.Collections.Generic;

namespace TurnBasedBattle
{
    class Item : Option
    {
        static Item()
        {
            items = new List<Option>();
            items.Add(new ManaPotion());
            items.Add(new Apple());
            items.Add(new ThrowingKnife());
            items.Add(new hjqhwuajk());
            items.Add(new HealthPotion());
            items.Add(new Demotivator());
            items.Add(new Bone());
            items.Add(new VelcroPenis());
        }

        public Item(int posX = 4, int posY = 4) : base(posX, posY) { }

        public override int PosX { get; set; }

        public override int PosY { get; set; }

        public override bool HasSubOptions { get; } = true;

        public override bool HasSuperOptions { get; } = false;

        public override string Name { get; } = "Item";

        public override string Description { get; }

        private static List<Option> items;

        public static List<Option> Items { get => items; }

        public override bool ActionIsAvailable => true;

        public override void ActivateEffect()
        {
            if (Items.Count > 0)
                GameInterface.ChooseOption(Item.Items);
        }

        public void DeleteItem()
        {
            Items.Remove(this);
            Console.SetCursorPosition(PosX, PosY);
            Console.WriteLine(new string(' ', Name.Length));

            // Предотвращаем визуальные глюки
            GameInterface.UpdatePlayerShownStats();
        }
    }
}
