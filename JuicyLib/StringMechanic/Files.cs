using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace JuicyLib.StringMechanic
{
    public static class Files
    {
        public static async Task WriteFileAsync(string filePath, List<string> values)
        {
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                foreach (string value in values)
                {
                    await sw.WriteLineAsync(value);
                }
            }
        }

        public static async Task WriteFileAsync(string filePath, string value)
        {
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                await sw.WriteLineAsync(value);
            }
        }

        public static int LineCount(string filePath)
        {
            return File.ReadAllLines(filePath).Length;
        }
    }
}
