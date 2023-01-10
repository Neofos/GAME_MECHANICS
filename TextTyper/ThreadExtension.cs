using System.Threading;

namespace TextTyper
{
    static class ThreadExtension
    {
        /// <summary>
        /// Sleeps the amount of time depending on what the typed symbol is.
        /// </summary>
        /// <param name="thread">Current thread.</param>
        /// <param name="i">An index of the symbol that was just typed.</param>
        public static void Sleep(this Thread thread, in string text, in int i, in int delay)
        {
            int miliseconds;
            switch (text[i])
            {
                case '.':
                    bool forwardCheckingIsAvailable = i + 1 < text.Length;
                    if (forwardCheckingIsAvailable)
                    {
                        bool nextLetterIsBlank = text[i + 1] == ' ';
                        if (nextLetterIsBlank)
                        {
                            goto case '?';
                        }
                        else
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

            Thread.Sleep(miliseconds);
        }
    }
}
