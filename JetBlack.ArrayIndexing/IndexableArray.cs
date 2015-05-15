using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace JetBlack.ArrayIndexing
{
    public class IndexableArray<T>
    {
        private readonly ArrayIndexer _indexer;

        public IndexableArray(T[] array, params int[] bounds)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (bounds.Aggregate(1, (x, y) => x * y) != array.Length)
                throw new ArgumentOutOfRangeException("bounds", "There array length does not match the bounds");

            Array = array;
            _indexer = new ArrayIndexer(bounds);
        }

        public T[] Array { get; private set; }

        public T this[params int[] indices]
        {
            get { return Array[_indexer.ToIndex(indices)]; }
            set { Array[_indexer.ToIndex(indices)] = value; }
        }

        public ReadOnlyCollection<int> Bounds { get { return _indexer.Bounds; } }
    }

    public static class IndexableArray
    {
        public static IndexableArray<T> Create<T>(this T[] array, params int[] bounds)
        {
            return new IndexableArray<T>(array, bounds);
        }
    }
}
