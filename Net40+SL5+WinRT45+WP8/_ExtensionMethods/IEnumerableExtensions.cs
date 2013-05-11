using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kirinji.LightWands
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (action == null) throw new ArgumentNullException("action");

            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (action == null) throw new ArgumentNullException("action");

            foreach (var item in source) action(item);
        }

        public static IEnumerable<T> Hide<T>(this IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException("source");
            foreach (var s in source) yield return s;
        }

        /// <summary>
        /// 指定された条件に合致した、最後の要素のインデックスを返します。見つからなかった場合は null が返されます。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int? FirstIndex<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");

            for (int i = 0; i <= source.Count() - 1; i++)
            {
                if (predicate(source.ElementAt(i))) return i;
            }

            return null;
        }

        /// <summary>
        /// 指定された条件に合致した、最後の要素のインデックスを返します。見つからなかった場合は null が返されます。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int? LastIndex<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");

            for (int i = source.Count() - 1; i >= 0; i--)
            {
                if (predicate(source.ElementAt(i))) return i;
            }
            return null;
        }

        /// <summary>
        /// 指定された条件に合致した、唯一の要素のインデックスを返します。見つからなかった場合は null が返されます。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int? SingleIndex<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");

            int? firstIndex = null;
            for (int i = 0; i <= source.Count() - 1; i++)
            {
                if (predicate(source.ElementAt(i)))
                {
                    if (firstIndex == null)
                    {
                        firstIndex = i;
                    }
                    else
                    {
                        throw new InvalidOperationException("要素が複数見つかりました。");
                    }
                }
            }
            return firstIndex;
        }

        public static IEnumerable<int> WhereIndexes<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");

            List<int> matchedIndexes = new List<int>();
            for (int i = 0; i <= source.Count() - 1; i++)
            {
                if (predicate(source.ElementAt(i))) matchedIndexes.Add(i);
            }
            return matchedIndexes;
        }

        /// <summary>
        /// 指定されたインデックスの要素を返します。存在しない場合は null が返されます。
        /// </summary>
        /// <typeparam name="T">T は値型である必要があります。</typeparam>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T? ElementAtOrNull<T>(this IEnumerable<T> source, int index) where T : struct
        {
            if (source == null) throw new ArgumentNullException("source");

            if (source.Count() - 1 >= index)
            {
                return source.ElementAt(index);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 最初の要素を返します。存在しない場合は null が返されます。
        /// </summary>
        /// <typeparam name="T">T は値型である必要があります。</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T? FirstOrNull<T>(this IEnumerable<T> source) where T : struct
        {
            if (source == null) throw new ArgumentNullException("source");

            if (source.Any())
            {
                return source.ElementAt(0);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 指定された条件に合致した最初の要素を返します。見つからなかった場合は null が返されます。
        /// </summary>
        /// <typeparam name="T">T は値型である必要があります。</typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T? FirstOrNull<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : struct
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");

            int? index = FirstIndex(source, predicate);
            if (index.HasValue)
            {
                return source.ElementAt(index.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 最後の要素を返します。存在しない場合は null が返されます。
        /// </summary>
        /// <typeparam name="T">T は値型である必要があります。</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T? LastOrNull<T>(this IEnumerable<T> source) where T : struct
        {
            if (source == null) throw new ArgumentNullException("source");

            if (source.Any())
            {
                return source.ElementAt(source.Count() - 1);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 指定された条件に合致した最後の要素を返します。見つからなかった場合は null が返されます。
        /// </summary>
        /// <typeparam name="T">T は値型である必要があります。</typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T? LastOrNull<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : struct
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");

            int? index = LastIndex(source, predicate);
            if (index.HasValue)
            {
                return source.ElementAt(index.Value);
            }
            else
            {
                return null;
            }
        }

        // SingleOrNull<T> の predicate がないオーバーロードメソッドは、例外を返すかnullを返すかのシチュエーションがはっきりしないので未定義
        // まあ SingleOrDefault<T> を参考にすればいいんだけど実装しても使う場面がないし

        /// <summary>
        /// 指定された条件に合致した唯一の要素を返します。見つからなかった場合は null が返されます。
        /// </summary>
        /// <typeparam name="T">T は値型である必要があります。</typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T? SingleOrNull<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : struct
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");

            int? index = SingleIndex(source, predicate);
            if (index.HasValue)
            {
                return source.ElementAt(index.Value);
            }
            else
            {
                return null;
            }
        }

        public static bool NonSequenceEqual<T>(this IEnumerable<T> source, IEnumerable<T> second)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (second == null) throw new ArgumentNullException("second");

            Func<T, T, bool> comparer = EqualityComparer<T>.Default.Equals;
            var sourceList = source.ToList();
            foreach (var sec in second)
            {
                if (!sourceList.RemoveFirst(t => comparer(t, sec))) return false;
            }
            return !sourceList.Any();
        }

        public static bool NonSequenceEqual<T>(this IEnumerable<T> source, IEnumerable<T> second, Func<T, T, bool> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (second == null) throw new ArgumentNullException("second");
            if (comparer == null) throw new ArgumentNullException("comparer");

            var sourceList = source.ToList();
            foreach (var sec in second)
            {
                if (!sourceList.RemoveFirst(t => comparer(t, sec))) return false;
            }
            return !sourceList.Any();
        }
    }
}
