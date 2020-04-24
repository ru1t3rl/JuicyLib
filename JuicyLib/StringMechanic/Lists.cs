using System.Collections.Generic;
using System.Threading.Tasks;

namespace JuicyLib.StringMechanic
{
    public static class Lists
    {
        public static List<string> RemoveDupes(List<string> og, List<string> toAdd)
        {
            foreach (string item in toAdd)
            {
                if (!og.Contains(item))
                {
                    og.Add(item);
                }
            }

            return og;
        }

        public static async Task<List<string>> RemoveDupesAsync(List<string> og, List<string> toAdd)
        {
            await foreach (string item in GetNamesAsync(toAdd))
            {
                if (!og.Contains(item))
                {
                    og.Add(item);
                }
            }

            return og;
        }

        public static List<string> RemoveDupes(List<string> list)
        {
            List<string> withoutDupes = new List<string>();

            foreach (string item in list)
            {
                if (!withoutDupes.Contains(item))
                {
                    withoutDupes.Add(item);
                }
            }

            return list;
        }

        public static async Task<List<string>> RemoveDupesAsync(List<string> list)
        {
            List<string> withoutDupes = new List<string>();

            await foreach (string item in GetNamesAsync(list))
            {
                if (!withoutDupes.Contains(item))
                {
                    withoutDupes.Add(item);
                }
            }

            return withoutDupes;
        }

        static async IAsyncEnumerable<string> GetNamesAsync(List<string> workers)
        {
            foreach (var name in workers)
            {
                yield return name;
            }
        }
    }
}
