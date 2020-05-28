using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace JuicyLib.StringMechanic
{
    public static class Files
    {
        public static async Task WriteFileAsync(string filePath, List<string> values, bool append = false)
        {
            lock_.EnterWriteLock();
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, append))
                {
                    foreach (string value in values)
                    {
                        sw.WriteLine(value);
                    }
                }
            }
            finally
            {
                if (lock_.IsWriteLockHeld)
                    lock_.ExitWriteLock();
            }
        }

        public static async Task WriteFileAsync(string filePath, string[] value, bool append = false)
        {
            lock_.EnterWriteLock();
            try
            {
                using StreamWriter sw = new StreamWriter(filePath, append);
                for(int iItem = 0; iItem < value.Length; iItem++)
                {
                    await sw.WriteLineAsync(value[iItem]);
                }
                sw.Close();
            }
            finally
            {
                if(lock_.IsWriteLockHeld)
                    lock_.ExitWriteLock();
            }
        }

        private static ReaderWriterLockSlim lock_ = new ReaderWriterLockSlim();
        public static async Task WriteFileAsync(string filePath, string value, bool append = false)
        {
            lock_.EnterWriteLock();
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, append))
                {
                    await sw.WriteLineAsync(value);
                }
            }
            finally
            {
                if (lock_.IsWriteLockHeld)
                    lock_.ExitWriteLock();
            }
        }

        public static int LineCount(string filePath)
        {
            return File.ReadAllLines(filePath).Length;
        }
    }
}
