using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace JetBlack.ArrayIndexing
{
    public class ArrayIndexer
    {
        private readonly int[] _sum;

        public ArrayIndexer(params int[] bounds)
        {
            _sum = ComputeBoundsSums(bounds); // Pre-compute bounds sums for speed.
            Bounds = Array.AsReadOnly(bounds);
        }

        public ReadOnlyCollection<int> Bounds { get; private set; }

        public int ToIndex(params int[] indices)
        {
            if (indices.Length != Bounds.Count)
                throw new ArgumentException("There should be as many indices as bounds", "indices");
            if (indices.Where((x, i) => x < 0 || x >= Bounds[i]).Any())
                throw new IndexOutOfRangeException();

            var index = indices[0];
            for (int i = 1; i < indices.Length; ++i)
                index += _sum[i - 1] * indices[i];
            return index;
        }

        public int[] FromIndex(int index)
        {
            var indices = new int[Bounds.Count];
            for (var i = Bounds.Count - 1; i > 0; --i)
            {
                indices[i] = index / _sum[i - 1];
                index %= _sum[i - 1];
            }
            indices[0] = index;

            return indices;
        }

        private static int[] ComputeBoundsSums(int[] bounds)
        {
            var array = new int[bounds.Length - 1];
            for (int i = 1, sum = bounds[i - 1]; i < bounds.Length; ++i, sum *= bounds[i - 1])
                array[i - 1] = sum;
            return array;
        }
    }

    public static class ArrayIndexerExtensions
    {
        public static T Index<T>(this ArrayIndexer arrayIndexer, T[] array, params int[] indices)
        {
            if (arrayIndexer == null)
                throw new ArgumentNullException("arrayIndexer");
            if (array == null)
                throw new ArgumentNullException("array");

            return array[arrayIndexer.ToIndex(indices)];
        }

        public static object Index(this ArrayIndexer arrayIndexer, Array array, params int[] indices)
        {
            if (arrayIndexer == null)
                throw new ArgumentNullException("arrayIndexer");
            if (array == null)
                throw new ArgumentNullException("array");

            return array.GetValue(arrayIndexer.ToIndex(indices));
        }
    }
}
