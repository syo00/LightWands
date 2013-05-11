using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kirinji.LightWands
{
    public static class ICollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> collection)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (collection == null) throw new ArgumentNullException("collection");
            foreach (var r in collection) source.Add(r);
        }
    }
}
