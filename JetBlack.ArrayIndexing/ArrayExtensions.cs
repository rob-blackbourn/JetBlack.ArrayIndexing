using System;

namespace JetBlack.ArrayIndexing
{
    public static class ArrayExtensions
    {
        public static Array CreateArray<T>(this T[] array1D, params int[] bounds)
        {
            if (array1D == null)
                throw new ArgumentNullException("array1D");
            
            var arrayNd = Array.CreateInstance(typeof(T), bounds);

            var indices = new int[bounds.Length];
            foreach (var value in array1D)
            {
                arrayNd.SetValue(value, indices);

                for (var j = 0; j < bounds.Length; ++j)
                {
                    if (++indices[j] < bounds[j])
                        break;
                    indices[j] = 0;
                }
            }

            return arrayNd;
        }

        public static T Index<T>(this T[] array, int[] bounds, params int[] indices)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (bounds == null)
                throw new ArgumentNullException("bounds");
            if (indices.Length == 0 || indices.Length != bounds.Length)
                throw new ArgumentException("There should be at least one index and as many indices as bounds", "indices");

            var index = indices[0];
            for (int i = 1, sum = bounds[i - 1]; i < indices.Length; ++i, sum *= bounds[i - 1])
                index += sum * indices[i];
            return array[index];
        }

        public static object Index(this Array array, int[] bounds, params int[] indices)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (array.Rank != 1)
                throw new ArgumentException("The array must be one dimensional", "array");
            if (bounds == null)
                throw new ArgumentNullException("bounds");
            if (indices.Length == 0 || indices.Length != bounds.Length)
                throw new ArgumentException("There should be at least one index and as many indices as bounds", "indices");

            var index = indices[0];
            for (int i = 1, sum = bounds[i - 1]; i < indices.Length; ++i, sum *= bounds[i - 1])
                index += sum * indices[i];
            return array.GetValue(index);
        }
    }
}
