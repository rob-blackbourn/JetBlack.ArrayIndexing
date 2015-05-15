# JetBlack.ArrayIndexing

# Use Case
I had a task where I was presented with a large one-dimensional array, which
represented an n-dimensional matrix. I wanted to be able to index into the
matrix without rebuilding it as multi-dimensional.

# Example

The test following demonstates the behaviour.

```cs
    var array3D = new IndexableArray<int>(
        new[]
        {
            0, 1, 2, 3,
            4, 5, 6, 7,
            8, 9, 10, 11,

            12, 13, 14, 15,
            16, 17, 18, 19,
            20, 21, 22, 23
        },
        4, 3, 2);

    Assert.AreEqual(0, array3D[0, 0, 0]);
    Assert.AreEqual(20, array3D[0, 2, 1]);
    Assert.AreEqual(23, array3D[3, 2, 1]);
```

# Algorithm

The following algorithm is used to transform the n-dimensional indices into a 1-dimensional index.

```cs
    public static T Index<T>(this T[] array, int[] bounds, params int[] indices)
    {
        var index = indices[0];
        for (int i = 1, sum = bounds[i - 1]; i < indices.Length; ++i, sum *= bounds[i - 1])
            index += sum * indices[i];
        return array[index];
    }
```

It should be clear that the sum of the bounds at each dimension can be
precomputed. The `IndexableArray` class does this optimisation, and also
provides argument checking.
