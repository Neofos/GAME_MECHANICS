using System;
using System.Linq;

namespace CharacterMovement
{
    class MainCharacter : Being
    {
        public MainCharacter()
        {
            beingIcon = "■";
            beingColor = ConsoleColor.Cyan;

            beingPosX = Map.MainHeroPositionX;
            beingPosY = Map.MainHeroPositionY;

            impassableObjects = new char[] { '█', '▬', '▐' };
        }

        private static ConsoleKeyInfo keyPressed;

        public override void BeingMove()
        {
            Console.SetCursorPosition(beingPosX, beingPosY);
            Console.Write(beingIcon, Console.ForegroundColor = beingColor);
            Console.SetCursorPosition(beingPosX, beingPosY); // Затираем следы персонажа при его передвижении

            keyPressed = Console.ReadKey();
            switch (keyPressed.Key.ToString())
            {
                case "UpArrow":
                    {
                        beingPosY -= 1;
                        Console.SetCursorPosition(beingPosX, beingPosY + 1);
                        Console.Write(" "); // Затираем следы персонажа, оставляемые потоком передвижения врагов
                        if (impassableObjects.Contains(Map.GameLevelMap[beingPosY, beingPosX]))
                            beingPosY += 1;
                        break;
                    }
                case "DownArrow":
                    {
                        beingPosY += 1;
                        Console.SetCursorPosition(beingPosX, beingPosY - 1);
                        Console.Write(" ");
                        if (impassableObjects.Contains(Map.GameLevelMap[beingPosY, beingPosX])) // Проходить через стены нельзя
                            beingPosY -= 1;
                        break;
                    }
                case "LeftArrow":
                    {
                        beingPosX -= 1;
                        Console.SetCursorPosition(beingPosX + 1, beingPosY);
                        Console.Write(" ");
                        if (impassableObjects.Contains(Map.GameLevelMap[beingPosY, beingPosX]))
                            beingPosX += 1;
                        break;
                    }
                case "RightArrow":
                    {
                        beingPosX += 1;
                        Console.SetCursorPosition(beingPosX - 1, beingPosY);
                        Console.Write(" ");
                        if (impassableObjects.Contains(Map.GameLevelMap[beingPosY, beingPosX]))
                            beingPosX -= 1;
                        break;
                    }
                default: // Не позволим другим клавишам как-либо влиять на персонажа.
                    {
                        break;
                    }
            }
        }
    }
}
