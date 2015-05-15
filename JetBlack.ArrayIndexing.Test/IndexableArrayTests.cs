using System;
using NUnit.Framework;

namespace JetBlack.ArrayIndexing.Test
{
    [TestFixture]
    public class IndexableArrayTests
    {
        [Test]
        public void ShouIndex1DArray()
        {
            var array1D = new IndexableArray<int>(
                new[]
                {
                    0, 1, 2, 3, 4
                },
                5);

            Assert.AreEqual(0, array1D[0]);
            Assert.AreEqual(2, array1D[2]);
            Assert.AreEqual(4, array1D[4]);

            Assert.Throws<IndexOutOfRangeException>(() => array1D[-1] = 0);
            Assert.Throws<IndexOutOfRangeException>(() => array1D[5] = 0);
        }

        [Test]
        public void ShouldIndex2DArray()
        {
            var array2D = new IndexableArray<int>(
                new[]
                {
                    0, 1, 2, 3,
                    4, 5, 6, 7,
                    8, 9, 10, 11
                },
                4, 3);

            Assert.AreEqual(0, array2D[0, 0]);
            Assert.AreEqual(9, array2D[1, 2]);
            Assert.AreEqual(11, array2D[3, 2]);

            Assert.Throws<IndexOutOfRangeException>(() => array2D[-1, -1] = 0);
            Assert.Throws<IndexOutOfRangeException>(() => array2D[4, 3] = 0);
            Assert.Throws<ArgumentException>(() => array2D[0] = 0);
            Assert.Throws<ArgumentException>(() => array2D[0, 0, 0] = 0);
        }

        [Test]
        public void ShouldIndex3DArray()
        {
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
        }

        [Test]
        public void ShouldCreate()
        {
            var array3D = new[]
            {
                0, 1, 2, 3,
                4, 5, 6, 7,
                8, 9, 10, 11,

                12, 13, 14, 15,
                16, 17, 18, 19,
                20, 21, 22, 23
            }.Create(4, 3, 2);

            Assert.AreEqual(4, array3D.Bounds[0]);
            Assert.AreEqual(3, array3D.Bounds[1]);
            Assert.AreEqual(2, array3D.Bounds[2]);
        }
    }
}
