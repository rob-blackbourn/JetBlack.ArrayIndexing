using NUnit.Framework;

namespace JetBlack.ArrayIndexing.Test
{
    [TestFixture]
    public class ArrayIndexerTests
    {
        [Test]
        public void ShouIndex1DArray()
        {
            int[] array1D =
            {
                0, 1, 2, 3
            };

            var indexer1D = new ArrayIndexer(4);
            Assert.AreEqual(2, indexer1D.Index(array1D, 2)); // col=2 -> 2
        }

        [Test]
        public void ShouldIndex2DArray()
        {
            int[] array2D =
            {
                0, 1, 2, 3,
                4, 5, 6, 7,
                8, 9, 10, 11
            };

            var indexer2D = new ArrayIndexer(4, 3);
            Assert.AreEqual(9, indexer2D.Index(array2D, 1, 2)); // col=1, row=2 -> 9
        }

        [Test]
        public void ShouldIndex3DArray()
        {
            int[] array3D =
            {
                0, 1, 2, 3,
                4, 5, 6, 7,
                8, 9, 10, 11,

                12, 13, 14, 15,
                16, 17, 18, 19,
                20, 21, 22, 23
            };
            var indexer3D = new ArrayIndexer(4, 3, 2);
            Assert.AreEqual(20, indexer3D.Index(array3D, 0, 2, 1)); // col=0, row=2, slice=1 -> 20
            Assert.AreEqual(0, indexer3D.Index(array3D, 0, 0, 0)); // col=0, row=0, slice=0 -> 0
        }

        [Test]
        public void ShouldCreate3DArray()
        {
            int[] array3D =
            {
                0, 1, 2, 3,
                4, 5, 6, 7,
                8, 9, 10, 11,

                12, 13, 14, 15,
                16, 17, 18, 19,
                20, 21, 22, 23
            };

            var array = (int[,,]) array3D.CreateArray(4, 3, 2);
            Assert.AreEqual(0, array[0, 0, 0]);
        }

        [Test]
        public void CompareArrays()
        {
            int[] array3D =
            {
                0, 1, 2, 3,
                4, 5, 6, 7,
                8, 9, 10, 11,

                12, 13, 14, 15,
                16, 17, 18, 19,
                20, 21, 22, 23
            };

            var copied3D = (int[,,]) array3D.CreateArray(4, 3, 2);
            var indexer3D = new ArrayIndexer(4, 3, 2);

            for (int i = 0, m = 0; i < 4; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    for (int k = 0; k < 2; ++k, ++m)
                    {
                        var x = indexer3D.Index(array3D, i, j, k);
                        var y = copied3D[i, j, k];
                        Assert.AreEqual(x, y);
                        //Debug.Print("Array[{0},{1},{2}] = {3} and {4} match = {5}", i, j, k, x, y, x == y);

                        var index = indexer3D.ToIndex(i, j, k);
                        var indices = indexer3D.FromIndex(index);
                        Assert.AreEqual(i, indices[0]);
                        Assert.AreEqual(j, indices[1]);
                        Assert.AreEqual(k, indices[2]);

                        //Debug.Print("Array[{0},{1},{2}] = {3} = Array[{4},{5},{6}]", i, j, k, index, indices[0], indices[1], indices[2]);
                    }
                }
            }
        }
    }
}
