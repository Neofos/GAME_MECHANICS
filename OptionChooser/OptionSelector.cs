using System;
using System.Text;

namespace OptionChooser
{
    static class OptionSelector
    {
        static private string selectionCursor = "->";

        static public string SelectionCursor
        {
            set
            {
                Console.OutputEncoding = Encoding.Unicode;
                if (value != null)
                    selectionCursor = value;
            }
        }

        static public string ChooseOption(string option1, string option2, string option3 = null, string option4 = null)
        {
            string[] availableOptions;
            if (option3 != null && option4 == null)
                availableOptions = new string[3] { option1, option2, option3 };
            else if (option4 != null && option3 != null)
                availableOptions = new string[4] { option1, option2, option3, option4 };
            else
                availableOptions = new string[2] { option1, option2 };

            Console.CursorVisible = false;
            Console.WriteLine();
            int cursorLeft = Console.CursorLeft + 4, cursorTop = Console.CursorTop;

            for (int j = 0; j < availableOptions.Length; j++)
            {
                Console.SetCursorPosition(cursorLeft, cursorTop + j);
                Console.Write(availableOptions[j]);
            }

            int i = 0;
            bool optionIsChoosed = false;
            do
            {
                Console.SetCursorPosition(cursorLeft - selectionCursor.Length - 1, cursorTop + i);
                Console.Write(selectionCursor + " " + availableOptions[i], Console.ForegroundColor = ConsoleColor.Yellow);
                Console.ResetColor();
                Console.SetCursorPosition(cursorLeft - selectionCursor.Length - 1, cursorTop + i);

                ConsoleKeyInfo keyPressed = Console.ReadKey();
                Console.Write(new string(' ', selectionCursor.Length) + availableOptions[i]);
                switch (keyPressed.Key)
                {
                    case ConsoleKey.UpArrow:
                        i--;
                        break;
                    case ConsoleKey.DownArrow:
                        i++;
                        break;
                    case ConsoleKey.Z:
                        optionIsChoosed = true;
                        break;
                }
                if (i >= availableOptions.Length)
                    i = 0;
                else if (i < 0)
                    i = availableOptions.GetUpperBound(0);
                else if (optionIsChoosed == true)
                {
                    Console.SetCursorPosition(cursorLeft - selectionCursor.Length - 1, cursorTop + i);
                    Console.Write(new string(' ', selectionCursor.Length + 1) + availableOptions[i],
                                  Console.ForegroundColor = ConsoleColor.Yellow);
                    Console.SetCursorPosition(Console.WindowLeft, Console.CursorTop + availableOptions.Length - i + 1);
                    Console.ResetColor();
                    return availableOptions[i];
                }

            }
            while (optionIsChoosed == false);
            return null; // This is kinda impossible
        }
    }
}
