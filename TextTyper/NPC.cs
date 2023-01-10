using System;
using System.Threading;
using System.Windows.Media;

namespace TextTyper
{
    class NPC
    {
        MediaPlayer player;

        public NPC()
        {
            player = new MediaPlayer();
            player.Open(new Uri(AppContext.BaseDirectory + "\\SFX\\voice_sans.wav", UriKind.Absolute));
            Thread.Sleep(1000); // Take time to load the .wav file
        }

        public string Name { get; set; }
        public string Race { get; set; }

        /// <summary>
        /// Types the text smoothly, letter by letter.
        /// </summary>
        /// <param name="text">A text to type</param>
        /// <param name="delay">A delay between letters</param>
        public void Say(string text, int delay = 30, bool hasVoice = true)
        {
            string charsToIgnore = "!?.'-:,";

            Console.CursorVisible = false;
            for (int i = 0; i < text.Length; i++)
            {
                if (hasVoice && !charsToIgnore.Contains(text[i].ToString()) && i % 2 != 0)
                    PlayVoice();

                Console.Write(text[i]);
                Thread.CurrentThread.Sleep(text, i, delay);
            }
            Console.WriteLine();
        }

        private void PlayVoice()
        {
            player.Open(new Uri(AppContext.BaseDirectory + "\\SFX\\voice_sans.wav", UriKind.Absolute));
            player.Play();
        }
    }
}
