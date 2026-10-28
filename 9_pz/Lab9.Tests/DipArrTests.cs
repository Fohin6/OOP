using System;
using Lab9;
using Xunit;

namespace Lab9.Tests
{
    [Collection("Sequential")]
    public class DiapasonArrayTests
    {
        [Fact]
        public void DefaultCtor_EmptyArray()
        {
            Assert.Equal(0, new DiapasonArray().Length);
        }

        [Fact]
        public void RandomCtor_CreatesElementsInRange()
        {
            var a = new DiapasonArray(20, -5, 5);
            Assert.Equal(20, a.Length);
            for (int i = 0; i < a.Length; i++)
            {
                Assert.InRange(a[i].X, -5, 5);
                Assert.InRange(a[i].Y, -5, 5);
            }
        }

        [Fact]
        public void RandomCtor_NegativeSize_Throws()
        {
            Assert.Throws<ArgumentException>(() => new DiapasonArray(-1, 0, 1));
        }

        [Fact]
        public void RandomCtor_MinGreaterThanMax_Throws()
        {
            Assert.Throws<ArgumentException>(() => new DiapasonArray(3, 10, 0));
        }

        [Fact]
        public void ItemsCtor_CopiesElements()
        {
            var items = new[] { new Diapason(1, 2), new Diapason(3, 4) };
            var a = new DiapasonArray(items);
            Assert.Equal(2, a.Length);
            Assert.Same(items[1], a[1]);
        }

        [Fact]
        public void ItemsCtor_Null_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new DiapasonArray((Diapason[])null!));
        }

        [Fact]
        public void ItemsCtor_NullElement_Throws()
        {
            Assert.Throws<ArgumentException>(() => new DiapasonArray(new Diapason[] { null! }));
        }

        [Fact]
        public void Indexer_SetAndGet()
        {
            var a = new DiapasonArray(new[] { new Diapason(1, 2) });
            var n = new Diapason(9, 10);
            a[0] = n;
            Assert.Same(n, a[0]);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(2)]
        [InlineData(100)]
        public void Indexer_OutOfRange_Throws(int index)
        {
            var a = new DiapasonArray(new[] { new Diapason(1, 2), new Diapason(3, 4) });
            Assert.Throws<ArgumentException>(() => a[index]);
            Assert.Throws<ArgumentException>(() => a[index] = new Diapason());
        }

        [Fact]
        public void Indexer_SetNull_Throws()
        {
            var a = new DiapasonArray(new[] { new Diapason() });
            Assert.Throws<ArgumentNullException>(() => a[0] = null!);
        }

        [Fact]
        public void ObjectCount_Increases()
        {
            int before = DiapasonArray.ObjectCount;
            _ = new DiapasonArray();
            _ = new DiapasonArray(2, 0, 1);
            _ = new DiapasonArray(new Diapason[0]);
            Assert.Equal(before + 3, DiapasonArray.ObjectCount);
        }

        [Fact]
        public void Print_EmptyAndFilled()
        {
            var old = Console.Out;
            var sw = new System.IO.StringWriter();
            Console.SetOut(sw);
            try
            {
                new DiapasonArray().Print();
                new DiapasonArray(new[] { new Diapason(1, 2) }).Print();
            }
            finally { Console.SetOut(old); }
            Assert.Contains("пуст", sw.ToString());
            Assert.Contains("[1; 2]", sw.ToString());
        }
    }

    [Collection("Sequential")]
    public class ProgramTests
    {
        [Fact]
        public void FindMax_ReturnsLongestRange()
        {
            var longest = new Diapason(-10, 10);
            var a = new DiapasonArray(new[] { new Diapason(1, 2), longest, new Diapason(5, 3) });
            Assert.Same(longest, Program.FindMax(a));
        }

        [Fact]
        public void FindMax_SingleElement()
        {
            var d = new Diapason(1, 2);
            Assert.Same(d, Program.FindMax(new DiapasonArray(new[] { d })));
        }

        [Fact]
        public void FindMax_Empty_Throws()
        {
            Assert.Throws<InvalidOperationException>(() => Program.FindMax(new DiapasonArray()));
        }
    }
}