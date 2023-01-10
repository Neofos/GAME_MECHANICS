using System;

namespace OptionChooser
{
    class Program
    {
        static void Main(string[] args)
        {
            OptionSelector.SelectionCursor = "»";

            Console.WriteLine("Итак, чертила, на западе гора, а на горе Дима, и его надо чпокнуть.");

            string option1 = "Лады, сделаем это", option2 = "Мне впадлу, чел",
                   option3 = "Я что, мистик какой-то, чтобы таким заниматься?", option4 = "*проигнорировать*";

            string userAnswer = OptionSelector.ChooseOption(option1, option2, option3, option4);

            if (userAnswer == option1)
                Console.WriteLine("Ой пасиба!");
            else if (userAnswer == option2)
                Console.WriteLine("Ну ты и тварь!");
            else if (userAnswer == option3)
                Console.WriteLine("Ладно, справедливо, прости пожалуйста.");
            else if (userAnswer == option4)
                Console.WriteLine("...");
        }
    }
}