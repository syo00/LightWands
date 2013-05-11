using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kirinji.LightWands
{
    public static class IListExtensions
    {
        public static bool RemoveFirst<T>(this IList<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");

            int? firstIndex = source.FirstIndex(predicate);
            if (firstIndex.HasValue)
            {
                source.RemoveAt(firstIndex.Value);
                return true;
            }
            else
                return false;
        }

        public static bool RemoveFirst<T>(this IList<T> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            if (source.Count == 0)
                return false;
            else
            {
                source.RemoveAt(0);
                return true;
            }
        }

        public static bool RemoveLast<T>(this IList<T> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            if (source.Count == 0)
                return false;
            else
            {
                source.RemoveAt(source.Count - 1);
                return true;
            }
        }

        public static bool RemoveLast<T>(this IList<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");

            int? lastIndex = source.LastIndex(predicate);
            if (lastIndex.HasValue)
            {
                source.RemoveAt(lastIndex.Value);
                return true;
            }
            else
                return false;
        }

        public static IEnumerable<T> RemoveAll<T>(this IList<T> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var removedItems = new List<T>(source);
            source.Clear();
            return removedItems;
        }

        public static IEnumerable<T> RemoveAll<T>(this IList<T> source, T item)
        {
            if (source == null) throw new ArgumentNullException("source");

            return source.RemoveAll(t => Object.Equals(t, item));
        }

        public static IEnumerable<T> RemoveAll<T>(this IList<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var removedItems = new List<T>();

            for (int i = source.Count - 1; i >= 0; i--)
            {
                var item = source[i];
                if (predicate(item))
                {
                    removedItems.Add(item);
                    source.RemoveAt(i);
                }
            }

            removedItems.Reverse();
            return removedItems;
        }

        public static T PopFirst<T>(this IList<T> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var firstItem = source.First();
            source.RemoveFirst();
            return firstItem;
        }

        public static IList<T> PopFirst<T>(this IList<T> source, int count)
        {
            if (source == null) throw new ArgumentNullException("source");

            if (count <= -1) throw new ArgumentOutOfRangeException();

            var returnList = new List<T>();

            foreach (var i in Enumerable.Range(0, count))
            {
                returnList.Add(source.PopFirst());
            }

            return returnList;
        }

        public static T PopLast<T>(this IList<T> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var lastItem = source.Last();
            source.RemoveLast();
            return lastItem;
        }

        public static IList<T> PopLast<T>(this IList<T> source, int count)
        {
            if (source == null) throw new ArgumentNullException("source");

            if (count <= -1) throw new ArgumentOutOfRangeException();

            var returnList = new List<T>();

            foreach (var i in Enumerable.Range(0, count))
            {
                returnList.Add(source.PopLast());
            }

            returnList.Reverse();
            return returnList;
        }

        
    }
}
