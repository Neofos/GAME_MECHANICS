using System;
using System.Runtime.Versioning;

namespace Dialogue
{
    class Program
    {
        [SupportedOSPlatform("windows")]
        static void Main()
        {
            // =-----------------------MAIN OPTIONS-----------------------=
            int consoleWidth = 75, consoleHeight = 30;
            Console.SetWindowSize(consoleWidth, consoleHeight);
            Console.SetBufferSize(consoleWidth, consoleHeight);
            // =----------------------------------------------------------=

            DialogueBox.ShowDialogueBox("Я говно, я конченное ничтожество, я никчемная тварь блять, " +
                "уебищная, но я, блять, красив в этом, я уникален и в этом моя суть, " +
                "и в этом моё место. Да я говно. Да, я ничтожество. Да, я срань этого мира, но блять я " +
                "истинная срань, я блять уникальная срань, я такой, да я конченное говно, я ничтожество, " +
                "я мудак, я тварь, я хуйня, но блять я такой, какой я есть, я исполняю комбинацию этого мира, " +
                "и я мним и являюсь такой какой я есть.", true, true);

            // Delay
            Console.ReadKey(true);
        }
    }
}

