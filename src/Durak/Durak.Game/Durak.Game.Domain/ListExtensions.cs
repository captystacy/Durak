using System.Collections;

namespace Durak.Game.Domain
{
    public static class ListExtensions
    {
        private static readonly Random Random = new();

        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static int GetNextIndex(this IList list, int index)
        {
            return index == list.Count - 1
                ? 0
                : index + 1;
        }

        public static int GetPreviousIndex(this IList list, int index)
        {
            return index - 1 < 0
                ? list.Count - 1
                : index - 1;
        }

        public static T GetNextItem<T>(this IList<T> list, int index)
        {
            return index == list.Count - 1
                ? list[0]
                : list[index + 1];
        }

        public static T GetNextItem<T>(this IList<T> list, T item)
        {
            var index = list.IndexOf(item);

            return list.GetNextItem(index);
        }

        public static T GetPreviousItem<T>(this IList<T> list, int index)
        {
            return index - 1 < 0
                ? list[^1]
                : list[index - 1];
        }
    }
}
