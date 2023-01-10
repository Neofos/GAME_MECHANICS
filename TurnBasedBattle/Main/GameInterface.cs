using System;
using System.Collections.Generic;

namespace TurnBasedBattle
{
    static class GameInterface
    {
        public const ConsoleColor standartColor = ConsoleColor.White;

        public static void DrawInterface()
        {
            Console.ForegroundColor = standartColor;

            void DrawBorders() // Вещи, которые остаются на протяжении всей игры
            {
                // Левая граница
                for (int i = Console.WindowTop; i < Console.WindowHeight; i++)
                {
                    Console.SetCursorPosition(Console.WindowLeft, i);
                    Console.WriteLine('|');
                }

                // Правая граница
                for (int i = Console.WindowTop - 1; i < Console.WindowHeight; i++)
                {
                    Console.SetCursorPosition(Console.WindowWidth - 1, i);
                    Console.WriteLine('|');
                }

                // Верхняя граница
                Console.SetCursorPosition(Console.WindowLeft + 1, Console.WindowTop - 1);
                Console.WriteLine(new string('—', Console.WindowWidth - 2));

                // Нижняя граница
                Console.SetCursorPosition(Console.WindowLeft + 1, Console.WindowHeight - 1);
                Console.WriteLine(new string('—', Console.WindowWidth - 2));

                // Внутренняя верхняя граница (часть игрока)
                Console.SetCursorPosition(Console.WindowLeft + 1, Console.WindowTop + 6);
                Console.WriteLine(new string('—', Console.WindowWidth - 2));

                // Внутренняя нижняя граница (часть врага)
                Console.SetCursorPosition(Console.WindowLeft + 1, Console.WindowHeight - 8);
                Console.WriteLine(new string('—', Console.WindowWidth - 2));

                // Внутренняя верхняя левая граница (для опций)
                for (int i = Console.WindowTop; i < 7; i++)
                {
                    Console.SetCursorPosition(Console.WindowLeft + 13, i);
                    Console.WriteLine('|');
                }
            }
            void PrintOptions() // "Фейковые" опции на случай, если враг ходит первым 
            {
                string[] firstEverOptions = { "Attack", "Magic", "Item", "Defend" };
                int optionsPosX = 4, firstOptionPosY = 2;
                for (int i = 0; i < firstEverOptions.Length; i++)
                {
                    Console.SetCursorPosition(optionsPosX, firstOptionPosY + i);
                    Console.WriteLine(firstEverOptions[i]);
                }
            }

            DrawBorders();
            PrintOptions();
            UpdatePlayerShownStats();
            UpdateEnemyShownStats();

            // Сообщает игроку, что ему нужно крутануть колесом мыши вверх, ибо консоль съезжает вначале
            Console.SetCursorPosition(Console.WindowLeft + 1, Console.WindowHeight);
            Console.WriteLine("ПРОКРУТИ ВВЕРХ!", Console.ForegroundColor = ConsoleColor.Red);
        }

        public static void ClearDisplayableInfo()
        {
            Console.SetCursorPosition(Player.PlrMana.PosX, Player.PlrMana.PosY + 2);
            Console.WriteLine(new string(' ', 3));

            Console.SetCursorPosition(Enemy.EnmHealth.PosX, Enemy.EnmHealth.PosY + 2);
            Console.WriteLine(new string(' ', 8));

            Console.SetCursorPosition(Player.PlrHealth.PosX, Player.PlrHealth.PosY + 2);
            Console.WriteLine(new string(' ', 10));

            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(1, 8 + i);
                Console.Write(new string(' ', Console.WindowWidth - 2));
            }
        }

        public static void UpdateOptions(List<Option> options, bool needClear = false) // needClear включает режим очистки
        {
            // Предварительное очищение лишних символов с экрана
            ClearDisplayableInfo();

            // Метод для поиска индекса длиннейшей опции
            int FindTheLongestOption(int startIndex)
            {
                Option optTemp = options[startIndex];
                for (int i = startIndex; i < options.Count; i++)
                {
                    if (optTemp.Name.Length < options[i].Name.Length)
                        optTemp = options[i];
                }

                return options.FindIndex(startIndex, (opt) => opt.Name.Length == optTemp.Name.Length);
            }

            Console.ForegroundColor = standartColor;

            // Проверяем, выбираем ли мы сейчас именно атаку
            if (options[0].HasSuperOptions)
            {
                // Убрали ману и здоровье с экрана
                UpdatePlayerShownStats(true);

                int longestOptionNameIndent = 3, index, healthPosX, manaPosX, nameIndent;

                if (options.Count <= 4)
                {
                    index = FindTheLongestOption(0);
                    nameIndent = options[index].PosX + options[index].Name.Length;

                    healthPosX = needClear ? Console.WindowLeft + 28 : Console.WindowLeft + nameIndent + longestOptionNameIndent +
                                 ((nameIndent) > 28 ? 6 : 9);
                    manaPosX = needClear ? Console.WindowWidth - 19 : Console.WindowWidth - ((nameIndent) > 28 ? 10 : 13);
                }
                else
                {
                    index = FindTheLongestOption(4);
                    nameIndent = options[index].PosX + options[index].Name.Length;

                    healthPosX = needClear ? Console.WindowLeft + 28 :
                        Console.WindowLeft + nameIndent + longestOptionNameIndent + 2;
                    manaPosX = needClear ? Console.WindowWidth - 19 : Console.WindowWidth - 6;
                }

                for (int i = Console.WindowTop + 1; i < 7; i++)
                {
                    Console.SetCursorPosition(nameIndent + longestOptionNameIndent, i);
                    Console.WriteLine((needClear ? " " : "|"), Console.ForegroundColor = standartColor);
                }

                Player.PlrHealth.PosX = healthPosX;
                Player.PlrMana.PosX = manaPosX;
            }

            for (int i = 0; i < options.Count; i++)
            {
                Console.SetCursorPosition(options[i].PosX, options[i].PosY);
                Console.WriteLine((needClear ? new string(' ', options[i].Name.Length) : options[i].Name),
                                  Console.ForegroundColor = standartColor);
            }

            // Вернули ману и здоровье на экран уже в новом положении
            UpdatePlayerShownStats();
        }

        public static void UpdatePlayerShownStats(bool needClear = false)
        {
            // Вывод здоровья
            Console.ForegroundColor = Player.PlrHealth.HealthColor;

            Console.SetCursorPosition(Player.PlrHealth.PosX, Player.PlrHealth.PosY);
            Console.WriteLine(needClear == false ? "Health" : new string(' ', 6)); // 6 пробелов очищают место

            Console.SetCursorPosition(Player.PlrHealth.PosX, Player.PlrHealth.PosY + 1);
            Console.WriteLine(new string(' ', 3));
            Console.SetCursorPosition(Player.PlrHealth.PosX, Player.PlrHealth.PosY + 1);
            Console.WriteLine(needClear == false ? Player.PlrHealth.Value : new string(' ', 3)); // 3 пробела очищают место

            // Вывод маны
            Console.ForegroundColor = Player.PlrMana.ManaColor;

            Console.SetCursorPosition(Player.PlrMana.PosX, Player.PlrMana.PosY);
            Console.WriteLine(needClear == false ? "Mana" : new string(' ', 4)); // 4 пробелов очищают место

            Console.SetCursorPosition(Player.PlrMana.PosX, Player.PlrMana.PosY + 1);
            Console.WriteLine(new string(' ', 3));
            Console.SetCursorPosition(Player.PlrMana.PosX, Player.PlrMana.PosY + 1);
            Console.WriteLine(needClear == false ? Player.PlrMana.Value : new string(' ', 3)); // 3 пробела очищают место
        }

        public static void UpdateEnemyShownStats(bool needClear = false)
        {
            // Вывод здоровья
            Console.ForegroundColor = Enemy.EnmHealth.HealthColor;

            Console.SetCursorPosition(Enemy.EnmHealth.PosX, Enemy.EnmHealth.PosY);
            Console.WriteLine(needClear == false ? "Health" : new string(' ', 6)); // 6 пробелов очищают место

            Console.SetCursorPosition(Enemy.EnmHealth.PosX, Enemy.EnmHealth.PosY + 1);
            Console.WriteLine(new string(' ', 4));
            Console.SetCursorPosition(Enemy.EnmHealth.PosX, Enemy.EnmHealth.PosY + 1);
            Console.WriteLine(needClear == false ? Enemy.EnmHealth.Value : new string(' ', 4)); // 3 пробела очищают место

            // Вывод маны
            Console.ForegroundColor = Enemy.EnmMana.ManaColor;

            Console.SetCursorPosition(Enemy.EnmMana.PosX, Enemy.EnmMana.PosY);
            Console.WriteLine(needClear == false ? "Mana" : new string(' ', 4)); // 4 пробелов очищают место

            Console.SetCursorPosition(Enemy.EnmMana.PosX, Enemy.EnmMana.PosY + 1);
            Console.WriteLine(new string(' ', 4));
            Console.SetCursorPosition(Enemy.EnmMana.PosX, Enemy.EnmMana.PosY + 1);
            Console.WriteLine(needClear == false ? Enemy.EnmMana.Value : new string(' ', 4)); // 3 пробела очищают место
        }

        public static void ChooseOption(List<Option> options)
        {
            UpdateOptions(options);

            int i = 0; // Счётчик
            while (!Player.ActionWasPerformed)
            {
                options[i].OnHighlight?.Invoke();

                // Обновляем цвет опции, на которой стоит курсор
                Console.SetCursorPosition(options[i].PosX, options[i].PosY);
                Console.Write(options[i].Name, Console.ForegroundColor = ConsoleColor.DarkYellow);

                ConsoleKeyInfo pressedKey = Console.ReadKey();

                // Если мы выбрали опцию/опция имеет подопции, цвет не обновляем
                bool choosedOptionHasSuboptions = pressedKey.Key != ConsoleKey.Z ||
                                                  (pressedKey.Key == ConsoleKey.Z && !options[i].HasSubOptions);
                if (choosedOptionHasSuboptions)
                    Console.ForegroundColor = ConsoleColor.White;

                // Предохраняемся от лишних введённых символов
                Console.SetCursorPosition(options[i].PosX, options[i].PosY);
                Console.Write(options[i].Name + ' ');

                switch (pressedKey.Key)
                {
                    case (ConsoleKey.UpArrow):
                        if (i - 1 == -1)
                            i = options.Count - 1;
                        else
                            i--;
                        break;
                    case (ConsoleKey.DownArrow):
                        if (i + 1 == options.Count)
                            i = 0;
                        else
                            i++;
                        break;
                    case (ConsoleKey.LeftArrow):
                        // Проверяем на количество опций
                        if (options.Count > 4)
                        {
                            if (i - 4 < options.Count && i + 4 < options.Count)
                                i += 4;
                            else if (i - 4 >= 0)
                                i -= 4;
                        }
                        break;
                    case (ConsoleKey.RightArrow):
                        if (options.Count > 4)
                        {
                            if (i + 4 >= options.Count && i - 4 >= 0)
                                i -= 4;
                            else if (i + 4 < options.Count)
                                i += 4;
                        }
                        break;
                    case (ConsoleKey.Z):
                        // Действие должно быть доступно к выполнению
                        if (options[i].ActionIsAvailable)
                        {
                            // Если выбранное действие вложено, то мы принимаем его как конечное и закрываем окошко
                            // Если нет, то убираем выделение с опций первого уровня (если действие не выбрано, цикл не завершится)
                            if (options[i].HasSuperOptions)
                                UpdateOptions(options, true);
                            else
                                UpdateOptions(options);

                            // В случе выбора предмета, следующий за ним предмет занимает место потраченного
                            if (options[i] is Item && options[i].HasSuperOptions)
                            {
                                for (int j = options.Count - 2; j >= i; j--)
                                {
                                    options[j + 1].PosX = options[j].PosX;
                                    options[j + 1].PosY = options[j].PosY;
                                }
                            }

                            ManageOptions(options[i]);
                        }
                        break;
                    case (ConsoleKey.X):
                        // Если опция является подопцией, то мы закрываем менюшку
                        if (options[i].HasSuperOptions)
                            UpdateOptions(options, true);
                        else
                            continue;
                        return;
                }
            }
        }

        private static void ManageOptions(Option option)
        {
            option.ActivateEffect();

            if (option is Item item && option.HasSuperOptions)
                item.DeleteItem();

            if (!option.HasSubOptions)
            {
                Player.ActionWasPerformed = true;
            }
        }
    }
}
