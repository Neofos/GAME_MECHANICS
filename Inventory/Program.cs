/* -=-=-=-=-=-=-=-=-=-=-=-=-=-=-PURPOSE OF THE PROGRAM-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
   This program represents such game mechanic as INVENTORY, which allows you to store
   various collectable items in it and provides an access to them at any time during
   the playthrough. Controls: C - open the inventory, X - close the inventory, Z - 
   select an item, ARROW KEYS - navigate the inventory.
   -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= */

using System;
using System.Runtime.Versioning;

namespace Inventory
{
    class Program
    {
        // Property needed for control navigation purposes.
        private static ControllableGamePart CurrentPart { get; set; }

        [SupportedOSPlatform("windows")]
        private static void Main()
        {
            // Console window options.
            int consoleWidth = 100, consoleHeight = 40;
            Console.SetWindowSize(consoleWidth, consoleHeight);
            Console.SetBufferSize(consoleWidth, consoleHeight);
            Console.CursorVisible = false;

            // Game always starts in the game world.
            CurrentPart = ControllableGamePart.GAME_WORLD;

            // HACK: Adding items for testing.
            Inventory.AddItem(new CrystalShard());
            Inventory.AddItem(new HealthPotion());
            Inventory.AddItem(new HealthPotion());
            Inventory.AddItem(new HealthPotion());
            Inventory.AddItem(new ManaPotion());
            Inventory.AddItem(new ManaPotion());
            Inventory.AddItem(new ManaPotion());
            Inventory.AddItem(new CrystalShard());

            // Game cycle.
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                    Control(in pressedKey);
                }
            }
        }

        private static void Control(in ConsoleKeyInfo pressedKey)
        {
            switch (CurrentPart)
            {
                case ControllableGamePart.GAME_WORLD:
                    ControlGameWorld(in pressedKey);
                    break;
                case ControllableGamePart.INVENTORY_ITEMS:
                    ControlInventory(in pressedKey);
                    break;
                case ControllableGamePart.INVENTORY_ITEM_OPTIONS:
                    ControlInventoryOptions(in pressedKey);
                    break;
                default:
                    throw new Exception("CurrentPart property has an invalid value!");
            }

            // Narrowly focused controlling methods
            static void ControlGameWorld(in ConsoleKeyInfo pressedKey)
            {
                switch (pressedKey.Key)
                {
                    // Reserved controls for other mechanics
                    /*
                    case ConsoleKey.UpArrow:
                        break;
                    case ConsoleKey.DownArrow:
                        break;
                    case ConsoleKey.LeftArrow:
                        break;
                    case ConsoleKey.RightArrow:
                        break;
                    case ConsoleKey.Z:
                        break;
                    case ConsoleKey.X:
                        break;
                    */
                    case ConsoleKey.C:
                        Inventory.Open();
                        CurrentPart = ControllableGamePart.INVENTORY_ITEMS;
                        break;
                }
            }

            static void ControlInventory(in ConsoleKeyInfo pressedKey)
            {
                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        Inventory.MoveCursorUp();
                        break;
                    case ConsoleKey.DownArrow:
                        Inventory.MoveCursorDown();
                        break;
                    case ConsoleKey.Z:
                        bool partChangeNeeded = Inventory.ChooseItem();

                        if (partChangeNeeded)
                            CurrentPart = ControllableGamePart.INVENTORY_ITEM_OPTIONS;
                        break;
                    case ConsoleKey.X:
                        Inventory.Close();
                        CurrentPart = ControllableGamePart.GAME_WORLD;
                        break;
                }
            }

            static void ControlInventoryOptions(in ConsoleKeyInfo pressedKey)
            {
                switch (pressedKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        Inventory.ItemOptions.MoveHighlight();
                        break;
                    case ConsoleKey.RightArrow:
                        goto case ConsoleKey.LeftArrow;
                    case ConsoleKey.Z:
                        Inventory.ItemOptions.ChooseItemOption();
                        CurrentPart = ControllableGamePart.INVENTORY_ITEMS;
                        break;
                    case ConsoleKey.X:
                        Inventory.ItemOptions.ReturnToInventory();
                        CurrentPart = ControllableGamePart.INVENTORY_ITEMS;
                        break;
                }
            }
        }
    }
}
