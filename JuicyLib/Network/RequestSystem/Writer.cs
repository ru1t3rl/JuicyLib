using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JuicyLib
{
    public static class Writer
    {
        static ConsoleColor errorColor = ConsoleColor.DarkRed,
                            successColor = ConsoleColor.Green,
                            warningColor = ConsoleColor.DarkYellow,
                            statusColor = ConsoleColor.DarkGray;

        public static void WriteError(string error)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = errorColor;
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {error}");
            Console.ForegroundColor = currentColor;
        }

        public static void WriteSucces(string success)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = successColor;
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {success}");
            Console.ForegroundColor = currentColor;
        }

        public static void WriteWarning(string warning)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = warningColor;
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {warning}");
            Console.ForegroundColor = currentColor;
        }

        public static void WriteStatus(string status)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = statusColor;
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {status}");
            Console.ForegroundColor = currentColor;
        }

        public static void Write(string message)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {message}");
            Console.ForegroundColor = currentColor;
        }

        public static void WriteError(string id, string error)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = errorColor;
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {id}: {error}");
            Console.ForegroundColor = currentColor;
        }

        public static void WriteSucces(string id, string success)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = successColor;
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {id}: {success}");
            Console.ForegroundColor = currentColor;
        }

        public static void WriteWarning(string id, string warning)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = warningColor;
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {id}: {warning}");
            Console.ForegroundColor = currentColor;
        }

        public static void WriteStatus(string id, string status)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = statusColor;
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {id}: {status}");
            Console.ForegroundColor = currentColor;
        }

        public static void Write(string id, string message)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {id}: {message}");
            Console.ForegroundColor = currentColor;
        }

        public static void WriteDebug(string id, string message)
        {
#if !DEBUG
                return;
#endif

            ConsoleColor currentColor = Console.ForegroundColor;
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {id}: {message}");
            Console.ForegroundColor = currentColor;
        }

        public static void WriteDebug(string message)
        {
#if !DEBUG
                return;
#endif

            ConsoleColor currentColor = Console.ForegroundColor;
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {message}");
            Console.ForegroundColor = currentColor;
        }

        public static void Write(string message, LogType type)
        {
            ConsoleColor currentColor = Console.ForegroundColor;

            switch (type)
            {
                case LogType.Error:
                    Console.ForegroundColor = errorColor;
                    break;
                case LogType.Status:
                    Console.ForegroundColor = statusColor;
                    break;
                case LogType.Success:
                    Console.ForegroundColor = successColor;
                    break;
                case LogType.Warning:
                    Console.ForegroundColor = warningColor;
                    break;
            }

            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {message}");
            Console.ForegroundColor = currentColor;
        }

        public static void Write(string message, ConsoleColor color)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {message}");
            Console.ForegroundColor = currentColor;
        }



        public static void SetStatusColor(ConsoleColor color)
        {
            statusColor = color;
        }

        public static void SetWarningColor(ConsoleColor color)
        {
            warningColor = color;
        }

        public static void SetErrorColor(ConsoleColor color)
        {
            errorColor = color;
        }

        public static void SetSuccessColor(ConsoleColor color)
        {
            successColor = color;
        }
    }

    public enum LogType
    {
        Status,
        Error,
        Warning,
        Success
    }
}
