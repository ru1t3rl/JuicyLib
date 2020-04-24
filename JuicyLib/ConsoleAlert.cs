using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuicyLib
{
    public static class ConsoleMessage
    {
        /// <summary>
        ///     Write a message to the console.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="waitForInput">Should wait for user input before continuing.</param>
        public static void Show(string message, bool waitForInput = true)
        {
            ConsoleColor baseColor = Console.ForegroundColor;

            Console.WriteLine($"\n{message}");

            Console.ForegroundColor = baseColor;

            if (waitForInput)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(false);
            }
        }

        public static void Show(string message, MessageType type, bool waitForInput = true)
        {
            ConsoleColor baseColor = Console.ForegroundColor;
            Console.ForegroundColor = type switch
            {
                MessageType.Error => ConsoleColor.Red,
                MessageType.Info => ConsoleColor.Cyan,
                MessageType.Warning => ConsoleColor.DarkYellow
            };

            Console.WriteLine($"\n{message}");

            Console.ForegroundColor = baseColor;

            if (waitForInput)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(false);
            }
        }

        /// <summary>
        ///     Write a message to the console. (With error color of your choose)
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="color">The text color of the error message</param>
        /// <param name="waitForInput">Should wait for user input before continuing.</param>
        public static void Show(string message, ConsoleColor color, bool waitForInput = true)
        {
            ConsoleColor baseColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.WriteLine($"\n{message}");

            Console.ForegroundColor = baseColor;

            if (waitForInput)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(false);
            }
        }

        public enum MessageType
        {
            Warning,
            Error,
            Info
        }
    }
}
