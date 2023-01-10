using System;
using System.Threading;

namespace Dialogue
{
    public static class DialogueBox
    {
        private static readonly int dialogueWidth = Console.BufferWidth - 1,
            dialogueHeight = 6;

        private static int textStartPosX, textStartPosY;

        private static readonly char dialogueHorizontalLine = '-', dialogueVerticalLine = '|', dialogueCorner = '@';

        public static void ShowDialogueBox(string text, bool toBottom = false, bool avatarNeeded = false)
        {
            int bottomDialStartPosY = Console.WindowHeight - 7;
            int dialogueStartPosX = 0, dialogueStartPosY = toBottom ? bottomDialStartPosY : 0;
            int numberOfBorders = 2;

            // Отрисовка горизонтальных границ
            for (int n = 0; n < numberOfBorders; n++)
            {
                for (int i = dialogueStartPosX; i <= dialogueWidth; i++)
                {
                    bool cornerCharRequired = i == 0 || i == dialogueWidth;

                    Console.SetCursorPosition(i, dialogueStartPosY);
                    Console.Write(cornerCharRequired ? dialogueCorner : dialogueHorizontalLine);
                }

                dialogueStartPosX = 0;
                dialogueStartPosY = toBottom ? dialogueHeight + bottomDialStartPosY : dialogueHeight;
            }

            dialogueStartPosY = toBottom ? bottomDialStartPosY : 0; // Сброс значения

            // Отрисовка вертикальных границ
            for (int n = 0; n < numberOfBorders; n++)
            {
                for (int i = dialogueStartPosY + 1; i < dialogueHeight + (toBottom ? bottomDialStartPosY : 0); i++)
                {
                    Console.SetCursorPosition(dialogueStartPosX, i);
                    Console.Write(dialogueVerticalLine);
                }

                dialogueStartPosY = toBottom ? bottomDialStartPosY : 0;
                dialogueStartPosX = dialogueWidth;
            }

            // Отрисовка границы аватара
            if (avatarNeeded)
            {
                int avatarBorderPosX = 11;
                for (int i = dialogueStartPosY + 1; i < dialogueHeight + (toBottom ? bottomDialStartPosY : 0); i++)
                {
                    Console.SetCursorPosition(avatarBorderPosX, i);
                    Console.Write(dialogueVerticalLine);
                }
            }

            textStartPosX = avatarNeeded ? 14 : 3;
            textStartPosY = dialogueStartPosY + 2;

            Console.SetCursorPosition(textStartPosX, textStartPosY);
            Say(text);
        }

        private static void Say(string text, int delay = 30)
        {
            // Запоминание задержки, т. к. с ней могут производиться операции
            int startDelay = delay;

            void BreakLine(ref int symbolIndex)
            {
                int startIndex = symbolIndex;
                int indexOfLineBreak = symbolIndex;

                bool lesserIndexExists = indexOfLineBreak - 1 > 0;
                while (!char.IsWhiteSpace(text[indexOfLineBreak]) && lesserIndexExists)
                    indexOfLineBreak--;

                // Замена лишнего пробела на символ переноса строки
                text = text.Remove(indexOfLineBreak, 1).Insert(indexOfLineBreak, "\n");

                symbolIndex = indexOfLineBreak;

                // Удаление той части слова, которая уже успела вывестись
                int deletingStartPos = Console.CursorLeft - (startIndex - indexOfLineBreak),
                    deletingCount = startIndex - indexOfLineBreak;
                Console.SetCursorPosition(deletingStartPos, Console.CursorTop);
                Console.Write(new string(' ', deletingCount));

                // Осуществление переноса строки
                Console.Write(text[symbolIndex]);
                symbolIndex++;
                Console.SetCursorPosition(textStartPosX, Console.CursorTop);

                // Мгновенный вывод части слова, которая уже успела вывестись в прошлой строке
                for (; symbolIndex < startIndex; symbolIndex++)
                    Console.Write(text[symbolIndex]);

                // Вывод символа под инкрементированным индексом
                Console.Write(text[symbolIndex]);
            }
            void NewBox(ref int symbolIndex)
            {
                int startIndex = symbolIndex;
                int indexOfLineBreak = symbolIndex;

                bool lesserIndexExists = indexOfLineBreak - 1 > 0;
                while (!char.IsWhiteSpace(text[indexOfLineBreak]) && lesserIndexExists)
                    indexOfLineBreak--;

                // Удаление лишнего пробела
                text = text.Remove(indexOfLineBreak, 1);
                symbolIndex = indexOfLineBreak;

                // Ожидание клавиши продолжения.
                ConsoleKey contkey = ConsoleKey.NoName;
                while (contkey != ConsoleKey.Z)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                    if (pressedKey.Key == ConsoleKey.Z)
                        contkey = pressedKey.Key;
                }

                // Восстановление задержки, переданной в качестве параметра
                delay = startDelay;

                // Очищение пространства для продолжения диалога
                int deletingCount = (Console.BufferWidth - 3) - textStartPosX;
                for (int i = textStartPosY; i <= textStartPosY + 2; i++)
                {
                    Console.SetCursorPosition(textStartPosX, i);
                    Console.Write(new string(' ', deletingCount));
                }

                // Осуществление переноса строки
                Console.SetCursorPosition(textStartPosX, textStartPosY);

                // Мгновенный вывод части слова, которая уже успела вывестись в прошлой строке
                for (; symbolIndex < startIndex; symbolIndex++)
                    Console.Write(text[symbolIndex]);

                // Вывод символа под инкрементированным индексом
                Console.Write(text[symbolIndex]);
            }

            for (int i = 0; i < text.Length; i++)
            {
                // Если нажата клавиша X, отключить задержку
                if (Console.KeyAvailable)
                    if (Console.ReadKey(true).Key == ConsoleKey.X)
                        delay = 0;

                Console.Write(text[i]);

                bool textIsAboutToExitTheBoxX = Console.CursorLeft == Console.BufferWidth - 3,
                    textIsAboutToExitTheBoxY = Console.CursorTop == textStartPosY + 2;
                if (textIsAboutToExitTheBoxX)
                {
                    if (textIsAboutToExitTheBoxY)
                        NewBox(ref i);
                    else
                        BreakLine(ref i);
                }

                bool textShouldBeAlighned = text[i] == '\n';
                if (textShouldBeAlighned)
                    Console.SetCursorPosition(textStartPosX, Console.CursorTop);

                int miliseconds;
                switch (text[i])
                {
                    case '.':
                        bool forwardCheckingIsAvailable = i + 1 < text.Length;
                        if (forwardCheckingIsAvailable)
                        {
                            bool nextLetterIsBlank = text[i + 1] == ' ';
                            bool nextLetterIsN = text[i + 1] == '\n';
                            if (nextLetterIsBlank)
                            {
                                goto case '?';
                            }
                            else if (!nextLetterIsBlank && !nextLetterIsN)
                            {
                                goto default;
                            }
                        }

                        bool backwardCheckingIsAvailable = i - 2 >= 0 && i - 1 >= 0;
                        if (backwardCheckingIsAvailable)
                        {
                            bool ellipsisExists = text[i - 2] == '.' && text[i - 1] == '.';
                            if (ellipsisExists)
                            {
                                miliseconds = 1500;
                                break;
                            }
                        }
                        goto case '?';
                    case '!':
                        goto case '?';
                    case '?':
                        miliseconds = 600;
                        break;
                    case ',':
                        miliseconds = 400;
                        break;
                    case ':':
                        miliseconds = 500;
                        break;
                    case '-':
                        goto case ':';
                    default:
                        miliseconds = delay;
                        break;
                }

                if (delay != 0)
                    Thread.Sleep(miliseconds);
            }
        }
    }
}
