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

        static ConsoleColor warning = ConsoleColor.DarkYellow,
            error = ConsoleColor.Red,
            succes = ConsoleColor.Green,
            defaultColor = ConsoleColor.White,
            info = ConsoleColor.Cyan,
            statusColor = ConsoleColor.Yellow;

        public static void Show(string message, bool waitForInput = false)
        {
            ConsoleColor baseColor = Console.ForegroundColor;

            Console.WriteLine($"{message}");

            Console.ForegroundColor = baseColor;

            if (waitForInput)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(false);
            }
        }

        public static void Show(string message, MessageType type, bool waitForInput = false)
        {
            ConsoleColor baseColor = Console.ForegroundColor;
            Console.ForegroundColor = type switch
            {
                MessageType.Error => error,
                MessageType.Info => info,
                MessageType.Success => succes,
                MessageType.Warning => warning,
                MessageType.Status => statusColor
            };

            switch (type)
            {
                case MessageType.Error: Console.Write("[ERROR]  "); break;
                case MessageType.Info: Console.Write("[INFO]   "); break;
                case MessageType.Success: Console.Write("[SUCCESS]"); break;
                case MessageType.Warning: Console.Write("[WARN]   "); break;
                case MessageType.Status: Console.Write("[STATS]  "); break;
            };

            Console.ForegroundColor = baseColor;
            Console.WriteLine($"  {message}");

            if (waitForInput)
            {
                Console.Write("Press any key to continue...");
                Console.ReadKey(false);
            }
        }

        /// <summary>
        ///     Write a message to the console. (With error color of your choose)
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="color">The text color of the error message</param>
        /// <param name="waitForInput">Should wait for user input before continuing.</param>
        public static void Show(string message, ConsoleColor color, bool waitForInput = false)
        {
            ConsoleColor baseColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.WriteLine($"{message}");

            Console.ForegroundColor = baseColor;

            if (waitForInput)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(false);
            }
        }

        public static void SetWarningColor(ConsoleColor color) { warning = color; }
        public static void SetErrorColor(ConsoleColor color) { error = color; }
        public static void SetSuccesColor(ConsoleColor color) { succes = color; }
        public static void SetDefaultColor(ConsoleColor color) { defaultColor = color; }
        public static void SetInfoColor(ConsoleColor color) { defaultColor = color; }
        public static void SetStatusColor(ConsoleColor color) { statusColor = color; }

        public enum MessageType
        {
            Warning,
            Error,
            Info,
            Success,
            Status
        }
    }
}
