using System;
using System.Collections.Generic;

namespace Inventory
{
    internal static class Inventory
    {
        static Inventory()
        {
            itemNavigator.Cursor = ">>";
            itemNavigator.CurrentOptionColor = ConsoleColor.Yellow;
        }

        private const ConsoleColor DEFAULT_COLOR = ConsoleColor.White,
            UNAVAILABLE_COLOR = ConsoleColor.DarkGray;

        private const int INVENTORY_CAPACITY = 8, SPACE_BETWEEN_ITEMS = 2;

        private const string
            HORIZONTAL_UPPER_BORDER = "|-------INVENTORY---------------------|",
            HORIZONTAL_DESCRIPTION_BORDER = "|-------------------------------------|",
            HORIZONTAL_OPTIONS_BORDER = "|------------------|------------------|",

            VERTICAL_LONG_BORDER = "|||||||||||||||||",
            VERTICAL_MEDIUM_BORDER = "|||||",
            VERTICAL_SHORT_BORDER = "|||",

            USE_OPTION_NAME = "USE ITEM",
            TOSS_OPTION_NAME = "TOSS ITEM";

        private static readonly int
            inventoryStartX = Console.WindowLeft,
            inventoryStartY = Console.WindowTop,
            inventoryEndX = inventoryStartX + 38,

            descriptionDefaultStartX = inventoryStartX + 5,
            descriptionDefaultStartY = inventoryStartY + 2,

            horizontalDescriptionBorderX = inventoryStartX,
            horizontalDescriptionBorderY = descriptionDefaultStartY + 4,

            verticalMediumBorderLeftX = inventoryStartX,
            verticalMediumBorderLeftY = inventoryStartY + 1,

            verticalMediumBorderRightX = inventoryEndX,
            verticalMediumBorderRightY = inventoryStartY + 1,

            verticalLongBorderLeftX = inventoryStartX,
            verticalLongBorderLeftY = horizontalDescriptionBorderY + 1,

            verticalLongBorderRightX = inventoryEndX,
            verticalLongBorderRightY = horizontalDescriptionBorderY + 1,

            horizontalOptionsBorderUpperX = inventoryStartX,
            horizontalOptionsBorderUpperY = verticalLongBorderLeftY + 17,

            horizontalOptionsBorderBottomX = inventoryStartX,
            horizontalOptionsBorderBottomY = horizontalOptionsBorderUpperY + 4,

            verticalShortBorderLeftX = inventoryStartX,
            verticalShortBorderLeftY = horizontalOptionsBorderUpperY + 1,

            verticalShortBorderCenterX = verticalShortBorderLeftX + 19,
            verticalShortBorderCenterY = verticalShortBorderLeftY,

            verticalShortBorderRightX = inventoryEndX,
            verticalShortBorderRightY = verticalShortBorderLeftY,

            firstItemX = inventoryStartX + 7,
            firstItemY = horizontalDescriptionBorderY + 2,

            useOptionPosX = verticalShortBorderLeftX + 6,
            useOptionPosY = horizontalOptionsBorderUpperY + 2,

            tossOptionPosX = verticalShortBorderCenterX + 5,
            tossOptionPosY = horizontalOptionsBorderUpperY + 2;

        private static readonly List<ICollectableItem> items = new List<ICollectableItem>(INVENTORY_CAPACITY);

        private static OptionNavigator itemNavigator;

        public static void AddItem(ICollectableItem item)
        {
            if (items.Count < 8)
                items.Add(item);
            // TODO: Add the message about the lack of opportunity to add more than 8 items.
            // else { }
        }

        public static void RemoveItem(ICollectableItem item)
        {
            int itemIndex = items.IndexOf(item);

            // If the item isn't last...
            if (itemIndex != items.Count - 1)
            {
                // Erasing the items' names.
                string itemNameEraser;

                for (int i = 0; i < items.Count; i++)
                {
                    itemNameEraser = new string(' ', items[i].Name.Length);

                    Console.SetCursorPosition(items[i].PosX, items[i].PosY);
                    Console.Write(itemNameEraser);
                }

                // Updating the positions.
                int newPosTransmitterIndex = itemIndex,
                    newPosRecieverIndex = newPosTransmitterIndex + 1;

                for (; newPosRecieverIndex < items.Count; newPosRecieverIndex++)
                {
                    // Value exchange.
                    (items[newPosRecieverIndex].PosX, items[newPosTransmitterIndex].PosX) =
                        (items[newPosTransmitterIndex].PosX, items[newPosRecieverIndex].PosX);

                    (items[newPosRecieverIndex].PosY, items[newPosTransmitterIndex].PosY) =
                        (items[newPosTransmitterIndex].PosY, items[newPosRecieverIndex].PosY);
                }
            }
            else
            {
                // Erasing the item's name.
                Console.SetCursorPosition(item.PosX, item.PosY);
                Console.Write(new string(' ', item.Name.Length));

                // Erasing the navigation cursor.
                Console.SetCursorPosition(itemNavigator.PosX, itemNavigator.PosY);
                Console.Write(new string(' ', 2));

                if (items.Count > 1)
                {
                    itemNavigator.PosX = items[^2].PosX - 4;
                    itemNavigator.PosY = items[^2].PosY;
                }
            }

            // Removing the item from the collection.
            items.Remove(item);

            EraseDescription();

            Console.ForegroundColor = DEFAULT_COLOR;

            // Outputing items.
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    int indentY = i * SPACE_BETWEEN_ITEMS;

                    items[i].PosX = firstItemX;
                    items[i].PosY = firstItemY + indentY;

                    Console.SetCursorPosition(items[i].PosX, items[i].PosY);
                    Console.Write(items[i].Name);
                }

                // Highlighting the current option.
                Console.ForegroundColor = itemNavigator.CurrentOptionColor;

                ICollectableItem currItem = items.Find(item => item.PosY == itemNavigator.PosY);

                Console.SetCursorPosition(currItem.PosX, currItem.PosY);
                Console.Write(currItem.Name);

                // Returning the selection cursor.
                Console.SetCursorPosition(itemNavigator.PosX, itemNavigator.PosY);
                Console.Write(itemNavigator.Cursor);

                ShowDescription(currItem);
            }
            else
            {
                // Erasing the cursor.
                Console.SetCursorPosition(itemNavigator.PosX, itemNavigator.PosY);
                Console.Write(new string(' ', 2));

                string noItemsMessage = "You have no items.";

                int descriptionStartY = descriptionDefaultStartY + 1,
                    // descriptionStartX = (39 / 2) - (noItemsMessage.Length / 2)
                    descriptionStartX = 10;

                Console.SetCursorPosition(descriptionStartX, descriptionStartY);
                Console.Write(noItemsMessage);
            }
        }

        public static void RemoveItem(int itemIndex)
        {
            if (itemIndex >= 0 && itemIndex < 8)
                RemoveItem(items[itemIndex]);
        }

        public static void Open()
        {
            // TODO: You need to remember the previous state of the screen beforehand.

            Console.ForegroundColor = DEFAULT_COLOR;

            Console.SetCursorPosition(inventoryStartX, inventoryStartY);
            Console.Write(HORIZONTAL_UPPER_BORDER);

            Console.SetCursorPosition(horizontalDescriptionBorderX, horizontalDescriptionBorderY);
            Console.Write(HORIZONTAL_DESCRIPTION_BORDER);

            // Loops needed for the correct output of vertical borders.
            for (int i = 0; i < VERTICAL_MEDIUM_BORDER.Length; i++)
            {
                Console.SetCursorPosition(verticalMediumBorderLeftX, verticalMediumBorderLeftY + i);
                Console.Write(VERTICAL_MEDIUM_BORDER[i]);

                Console.SetCursorPosition(verticalMediumBorderRightX, verticalMediumBorderRightY + i);
                Console.Write(VERTICAL_MEDIUM_BORDER[i]);
            }

            for (int i = 0; i < VERTICAL_LONG_BORDER.Length; i++)
            {
                Console.SetCursorPosition(verticalLongBorderLeftX, verticalLongBorderLeftY + i);
                Console.Write(VERTICAL_LONG_BORDER[i]);

                Console.SetCursorPosition(verticalLongBorderRightX, verticalLongBorderRightY + i);
                Console.Write(VERTICAL_LONG_BORDER[i]);
            }

            Console.SetCursorPosition(horizontalOptionsBorderUpperX, horizontalOptionsBorderUpperY);
            Console.Write(HORIZONTAL_OPTIONS_BORDER);

            Console.SetCursorPosition(horizontalOptionsBorderBottomX, horizontalOptionsBorderBottomY);
            Console.Write(HORIZONTAL_OPTIONS_BORDER);

            for (int i = 0; i < VERTICAL_SHORT_BORDER.Length; i++)
            {
                Console.SetCursorPosition(verticalShortBorderLeftX, verticalShortBorderLeftY + i);
                Console.Write(VERTICAL_SHORT_BORDER[i]);

                Console.SetCursorPosition(verticalShortBorderCenterX, verticalShortBorderCenterY + i);
                Console.Write(VERTICAL_SHORT_BORDER[i]);

                Console.SetCursorPosition(verticalShortBorderRightX, verticalShortBorderRightY + i);
                Console.Write(VERTICAL_SHORT_BORDER[i]);
            }

            // Outputing items.
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    int indentY = i * SPACE_BETWEEN_ITEMS;

                    items[i].PosX = firstItemX;
                    items[i].PosY = firstItemY + indentY;

                    Console.SetCursorPosition(items[i].PosX, items[i].PosY);
                    Console.Write(items[i].Name);
                }

                // Highlighting the first option.
                Console.ForegroundColor = itemNavigator.CurrentOptionColor;

                itemNavigator.PosX = items[0].PosX - 4;
                itemNavigator.PosY = items[0].PosY;

                Console.SetCursorPosition(itemNavigator.PosX, itemNavigator.PosY);
                Console.Write(itemNavigator.Cursor);

                Console.SetCursorPosition(items[0].PosX, items[0].PosY);
                Console.Write(items[0].Name);

                ShowDescription(items[0]);
            }
            else
            {
                string noItemsMessage = "You have no items.";

                int descriptionStartY = descriptionDefaultStartY + 1,
                    // descriptionStartX = (39 / 2) - (noItemsMessage.Length / 2)
                    descriptionStartX = 10;

                Console.SetCursorPosition(descriptionStartX, descriptionStartY);
                Console.Write(noItemsMessage);
            }

            // Graying out item options.
            Console.ForegroundColor = UNAVAILABLE_COLOR;

            Console.SetCursorPosition(useOptionPosX, useOptionPosY);
            Console.Write(USE_OPTION_NAME);

            Console.SetCursorPosition(tossOptionPosX, tossOptionPosY);
            Console.Write(TOSS_OPTION_NAME);

            Console.ForegroundColor = DEFAULT_COLOR;
        }

        public static void Close()
        {
            // 38 - Vertical borders' length
            string blankHorizontalString = new string(' ', 39);

            Console.SetCursorPosition(inventoryStartX, inventoryStartY);
            Console.Write(blankHorizontalString);

            Console.SetCursorPosition(horizontalDescriptionBorderX, horizontalDescriptionBorderY);
            Console.Write(blankHorizontalString);

            string blankVerticalString = new string(' ', VERTICAL_MEDIUM_BORDER.Length);

            // Loops needed for the correct vertical borders' erasure.
            for (int i = 0; i < VERTICAL_MEDIUM_BORDER.Length; i++)
            {
                Console.SetCursorPosition(verticalMediumBorderLeftX, verticalMediumBorderLeftY + i);
                Console.Write(blankVerticalString[i]);

                Console.SetCursorPosition(verticalMediumBorderRightX, verticalMediumBorderRightY + i);
                Console.Write(blankVerticalString[i]);
            }

            blankVerticalString = new string(' ', VERTICAL_LONG_BORDER.Length);

            for (int i = 0; i < VERTICAL_LONG_BORDER.Length; i++)
            {
                Console.SetCursorPosition(verticalLongBorderLeftX, verticalLongBorderLeftY + i);
                Console.Write(blankVerticalString[i]);

                Console.SetCursorPosition(verticalLongBorderRightX, verticalLongBorderRightY + i);
                Console.Write(blankVerticalString[i]);
            }

            Console.SetCursorPosition(horizontalOptionsBorderUpperX, horizontalOptionsBorderUpperY);
            Console.Write(blankHorizontalString);

            Console.SetCursorPosition(horizontalOptionsBorderBottomX, horizontalOptionsBorderBottomY);
            Console.Write(blankHorizontalString);

            blankVerticalString = new string(' ', VERTICAL_SHORT_BORDER.Length);

            for (int i = 0; i < VERTICAL_SHORT_BORDER.Length; i++)
            {
                Console.SetCursorPosition(verticalShortBorderLeftX, verticalShortBorderLeftY + i);
                Console.Write(blankVerticalString[i]);

                Console.SetCursorPosition(verticalShortBorderCenterX, verticalShortBorderCenterY + i);
                Console.Write(blankVerticalString[i]);

                Console.SetCursorPosition(verticalShortBorderRightX, verticalShortBorderRightY + i);
                Console.Write(blankVerticalString[i]);
            }

            if (items.Count > 0)
            {
                string itemNameEraser;

                for (int i = 0; i < items.Count; i++)
                {
                    itemNameEraser = new string(' ', items[i].Name.Length);

                    Console.SetCursorPosition(items[i].PosX, items[i].PosY);
                    Console.Write(itemNameEraser);
                }

                // Erasing the navigation cursor.
                Console.SetCursorPosition(itemNavigator.PosX, itemNavigator.PosY);
                Console.Write(new string(' ', 2));
            }

            EraseDescription();

            Console.SetCursorPosition(useOptionPosX, useOptionPosY);
            Console.Write(new string(' ', USE_OPTION_NAME.Length));

            Console.SetCursorPosition(tossOptionPosX, tossOptionPosY);
            Console.Write(new string(' ', TOSS_OPTION_NAME.Length));
        }

        public static void MoveCursorDown()
        {
            // No reason to do anything if there's nowhere to move.
            if (items.Count < 1)
                return;

            ICollectableItem prevOption = items.Find(x => x.PosY == itemNavigator.PosY);

            if (prevOption != null) // Failsafe
            {
                Console.SetCursorPosition(prevOption.PosX, prevOption.PosY);
                Console.Write(prevOption.Name, Console.ForegroundColor = DEFAULT_COLOR);
            }

            // Erasing the cursor from the previous position.
            Console.SetCursorPosition(itemNavigator.PosX, itemNavigator.PosY);
            Console.Write(new string(' ', 2));

            bool cursorIsOnTheLastOption = itemNavigator.PosY == items[^1].PosY;

            if (cursorIsOnTheLastOption)
                itemNavigator.PosY = items[0].PosY;
            else
                itemNavigator.PosY += 2;

            Console.SetCursorPosition(itemNavigator.PosX, itemNavigator.PosY);
            Console.Write(itemNavigator.Cursor, Console.ForegroundColor = itemNavigator.CurrentOptionColor);

            ICollectableItem currOption = items.Find(item => item.PosY == itemNavigator.PosY);

            if (currOption != null) // Failsafe
            {
                Console.SetCursorPosition(currOption.PosX, currOption.PosY);
                Console.Write(currOption.Name);
            }

            Console.ForegroundColor = DEFAULT_COLOR;

            ShowDescription(currOption);
        }

        public static void MoveCursorUp()
        {
            // No reason to do anything if there's nowhere to move.
            if (items.Count < 1)
                return;

            ICollectableItem prevOption = items.Find(x => x.PosY == itemNavigator.PosY);

            if (prevOption != null) // Failsafe
            {
                Console.SetCursorPosition(prevOption.PosX, prevOption.PosY);
                Console.Write(prevOption.Name, Console.ForegroundColor = DEFAULT_COLOR);
            }

            // Erasing the cursor from the previous position.
            Console.SetCursorPosition(itemNavigator.PosX, itemNavigator.PosY);
            Console.Write(new string(' ', 2));

            bool cursorIsOnTheFirstOption = itemNavigator.PosY == items[0].PosY;

            if (cursorIsOnTheFirstOption)
                itemNavigator.PosY = items[^1].PosY;
            else
                itemNavigator.PosY -= 2;

            Console.SetCursorPosition(itemNavigator.PosX, itemNavigator.PosY);
            Console.Write(itemNavigator.Cursor, Console.ForegroundColor = itemNavigator.CurrentOptionColor);

            ICollectableItem currOption = items.Find(item => item.PosY == itemNavigator.PosY);

            if (currOption != null) // Failsafe
            {
                Console.SetCursorPosition(currOption.PosX, currOption.PosY);
                Console.Write(currOption.Name);
            }

            Console.ForegroundColor = DEFAULT_COLOR;

            ShowDescription(currOption);
        }

        /// <returns>If the option has been chosen succesfully.</returns>
        public static bool ChooseItem()
        {
            if (items.Count == 0)
                return false;

            // Erasing the cursor.
            Console.SetCursorPosition(itemNavigator.PosX, itemNavigator.PosY);
            Console.Write(new string(' ', 2));

            // Graying out all the items.
            Console.ForegroundColor = UNAVAILABLE_COLOR;

            for (int i = 0; i < items.Count; i++)
            {
                int indentY = i * SPACE_BETWEEN_ITEMS;

                items[i].PosX = firstItemX;
                items[i].PosY = firstItemY + indentY;

                Console.SetCursorPosition(items[i].PosX, items[i].PosY);
                Console.Write(items[i].Name);
            }

            // Highlighting the current option.
            Console.ForegroundColor = itemNavigator.CurrentOptionColor;

            ICollectableItem currItem = items.Find(item => item.PosY == itemNavigator.PosY);

            Console.SetCursorPosition(currItem.PosX, currItem.PosY);
            Console.Write(currItem.Name);

            // Highlighting the item options.
            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.SetCursorPosition(useOptionPosX, useOptionPosY);
            Console.Write(USE_OPTION_NAME, Console.BackgroundColor = ConsoleColor.DarkYellow);

            Console.ForegroundColor = DEFAULT_COLOR;

            Console.SetCursorPosition(tossOptionPosX, tossOptionPosY);
            Console.Write(TOSS_OPTION_NAME, Console.BackgroundColor = default);

            return true;
        }

        private static void ShowDescription(ICollectableItem item)
        {
            // Erasing previous description beforehand.
            EraseDescription();

            Console.ForegroundColor = DEFAULT_COLOR;

            string[] descriptionStrings = item.Description.Split('\n');

            int descriptionStartY = descriptionDefaultStartY;

            switch (descriptionStrings.Length)
            {
                case 1:
                    descriptionStartY = descriptionDefaultStartY + 1;
                    break;
                case 4:
                    descriptionStartY = descriptionDefaultStartY - 1;
                    break;
                case 5:
                    goto case 4;
            }

            int descriptionStartX;

            for (int i = 0; i < descriptionStrings.Length; i++, descriptionStartY++)
            {
                // 19 = 39 / 2, where 39 - inventory length.
                descriptionStartX = 19 - (descriptionStrings[i].Length / 2);

                Console.SetCursorPosition(descriptionStartX, descriptionStartY);
                Console.Write(descriptionStrings[i]);
            }
        }

        private static void EraseDescription()
        {
            // 37 - description window's width.
            string blankString = new string(' ', 37);

            for (int i = 1; i <= 5; i++) // 5 - description window's height.
            {
                Console.SetCursorPosition(inventoryStartX + 1, inventoryStartY + i);
                Console.Write(blankString);
            }
        }

        public static class ItemOptions
        {
            // USE ITEM is the default option
            private static AvailableOptions currOption = AvailableOptions.USE_ITEM;

            private static void UseItem()
            {
                ICollectableItem usableItem = items.Find(item => item.PosY == itemNavigator.PosY);

                if (usableItem.OnUse != null)
                    usableItem.OnUse.Invoke();

                ReturnToInventory();
                RemoveItem(usableItem);
            }

            private static void TossItem()
            {
                ICollectableItem usableItem = items.Find(item => item.PosY == itemNavigator.PosY);

                ReturnToInventory();
                RemoveItem(usableItem);
            }

            public static void MoveHighlight()
            {
                Console.ForegroundColor = DEFAULT_COLOR;

                int prevOptionPosX, prevOptionPosY,
                    nextOptionPosX, nextOptionPosY;

                string prevOptionName, nextOptionName;

                AvailableOptions newOption;

                if (currOption == AvailableOptions.USE_ITEM)
                {
                    prevOptionName = USE_OPTION_NAME;
                    prevOptionPosX = useOptionPosX;
                    prevOptionPosY = useOptionPosY;

                    nextOptionName = TOSS_OPTION_NAME;
                    nextOptionPosX = tossOptionPosX;
                    nextOptionPosY = tossOptionPosY;

                    newOption = AvailableOptions.TOSS_ITEM;
                }
                else
                {
                    prevOptionName = TOSS_OPTION_NAME;
                    prevOptionPosX = tossOptionPosX;
                    prevOptionPosY = tossOptionPosY;

                    nextOptionName = USE_OPTION_NAME;
                    nextOptionPosX = useOptionPosX;
                    nextOptionPosY = useOptionPosY;

                    newOption = AvailableOptions.USE_ITEM;
                }

                Console.SetCursorPosition(prevOptionPosX, prevOptionPosY);
                Console.Write(prevOptionName, Console.BackgroundColor = default);

                Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.SetCursorPosition(nextOptionPosX, nextOptionPosY);
                Console.Write(nextOptionName, Console.BackgroundColor = ConsoleColor.DarkYellow);

                currOption = newOption;

                Console.ForegroundColor = DEFAULT_COLOR;
                Console.BackgroundColor = default;
            }

            public static void ChooseItemOption()
            {
                if (currOption == AvailableOptions.USE_ITEM)
                    UseItem();
                else
                    TossItem();
            }

            public static void ReturnToInventory()
            {
                // Restoring the default option.
                currOption = AvailableOptions.USE_ITEM;

                // Graying out item options.
                Console.ForegroundColor = UNAVAILABLE_COLOR;
                Console.BackgroundColor = default;

                Console.SetCursorPosition(useOptionPosX, useOptionPosY);
                Console.Write(USE_OPTION_NAME);

                Console.SetCursorPosition(tossOptionPosX, tossOptionPosY);
                Console.Write(TOSS_OPTION_NAME);

                // Highlighting all the items
                Console.ForegroundColor = DEFAULT_COLOR;

                for (int i = 0; i < items.Count; i++)
                {
                    int indentY = i * SPACE_BETWEEN_ITEMS;

                    items[i].PosX = firstItemX;
                    items[i].PosY = firstItemY + indentY;

                    Console.SetCursorPosition(items[i].PosX, items[i].PosY);
                    Console.Write(items[i].Name);
                }

                // Highlighting the current option.
                Console.ForegroundColor = itemNavigator.CurrentOptionColor;

                ICollectableItem currItem = items.Find(item => item.PosY == itemNavigator.PosY);

                Console.SetCursorPosition(currItem.PosX, currItem.PosY);
                Console.Write(currItem.Name);

                // Returning the selection cursor.
                Console.SetCursorPosition(itemNavigator.PosX, itemNavigator.PosY);
                Console.Write(itemNavigator.Cursor);
            }

            private enum AvailableOptions
            {
                USE_ITEM,
                TOSS_ITEM
            }
        }
    }
}