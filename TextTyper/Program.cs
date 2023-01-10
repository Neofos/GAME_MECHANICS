using System;

namespace TextTyper
{
    class Program
    {
        static void Main(string[] args)
        {
            NPC sans = new NPC() { Name = "Sans", Race = "Skeleton" };
            sans.Say("it's a beautiful day outside...", 25);
            sans.Say("birds are singing, flowers are blooming.", 25);
            sans.Say("tell me, kid: what are the species you're from?", 25);
            sans.Say("human - this is your species, isn't it?", 25);
            sans.Say("check your NPC.cs file, kid.", 25);
            sans.Say(".........");
            sans.Say("i know what is happening.", 200, false);

            Console.WriteLine();

            sans.Say("Унылая пора! Очей очарованье!\n" +
                "Приятна мне твоя прощальная краса -\n" +
                "Люблю я пышное природы увяданье,\n" +
                "В багрец и в золото одетые леса,\n" +
                "В их сенях ветра шум и свежее дыханье,\n" +
                "И мглой волнистою покрыты небеса,\n" +
                "И редкий солнца луч, и первые морозы,\n" +
                "И отдаленные седой зимы угрозы.");

            Console.ReadKey();
        }
    }
}
