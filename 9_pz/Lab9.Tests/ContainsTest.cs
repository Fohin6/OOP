using System;
using Lab9;
using Xunit;

namespace Lab9.Tests
{
    [Collection("Sequential")]
    public class DiapasonTests
    {
        [Fact]
        public void DefaultCtor_ZeroZero()
        {
            var d = new Diapason();
            Assert.Equal(0, d.X);
            Assert.Equal(0, d.Y);
        }

        [Fact]
        public void ParamCtor_SetsFields()
        {
            var d = new Diapason(1.5, 4);
            Assert.Equal(1.5, d.X);
            Assert.Equal(4, d.Y);
        }

        [Fact]
        public void Properties_SetAndGet()
        {
            var d = new Diapason { X = 3, Y = 7 };
            Assert.Equal(3, d.X);
            Assert.Equal(7, d.Y);
        }

        [Fact]
        public void ObjCount_Increases()
        {
            int before = Diapason.ObjCount;
            _ = new Diapason();
            _ = new Diapason(1, 2);
            Assert.Equal(before + 2, Diapason.ObjCount);
        }

        [Theory]
        [InlineData(2, 8, 5, true)]
        [InlineData(2, 8, 2, true)]
        [InlineData(2, 8, 8, true)]
        [InlineData(2, 8, 1.99, false)]
        [InlineData(2, 8, 9, false)]
        [InlineData(8, 2, 5, true)]
        [InlineData(8, 2, 10, false)]
        public void StaticContains_Works(double x, double y, double v, bool expected)
        {
            Assert.Equal(expected, Diapason.Contains(new Diapason(x, y), v));
        }

        [Fact]
        public void StaticContains_DoesNotMutate()
        {
            var d = new Diapason(8, 2);
            Diapason.Contains(d, 5);
            Assert.Equal(8, d.X);
            Assert.Equal(2, d.Y);
        }

        [Theory]
        [InlineData(2, 8, 5, true)]
        [InlineData(2, 8, 9, false)]
        [InlineData(8, 2, 3, true)]
        public void InstanceContains_Works(double x, double y, double v, bool expected)
        {
            Assert.Equal(expected, new Diapason(x, y).Contains(v));
        }

        [Theory]
        [InlineData(2, 8, 6)]
        [InlineData(8, 2, 6)]
        [InlineData(-3, 3, 6)]
        [InlineData(5, 5, 0)]
        public void Not_ReturnsLength(double x, double y, double expected)
        {
            Assert.Equal(expected, !new Diapason(x, y));
        }

        [Fact]
        public void Increment_ShiftsBothBounds_AndKeepsOriginal()
        {
            var d = new Diapason(1, 2);
            var r = d;
            r++;
            Assert.Equal(2, r.X);
            Assert.Equal(3, r.Y);
            Assert.Equal(1, d.X);
            Assert.Equal(2, d.Y);
        }

        [Theory]
        [InlineData(3.9, 3)]
        [InlineData(-3.9, -3)]
        [InlineData(0.5, 0)]
        public void ExplicitInt_ReturnsIntegerPartOfX(double x, int expected)
        {
            Assert.Equal(expected, (int)new Diapason(x, 10));
        }

        [Fact]
        public void ImplicitDouble_ReturnsY()
        {
            double v = new Diapason(1, 7.5);
            Assert.Equal(7.5, v);
        }

        [Fact]
        public void Plus_Int_BothSides()
        {
            var d = new Diapason(1, 2);
            var r1 = d + 3;
            var r2 = 3 + d;
            Assert.Equal(4, r1.X); Assert.Equal(5, r1.Y);
            Assert.Equal(4, r2.X); Assert.Equal(5, r2.Y);
        }

        [Theory]
        [InlineData(5, true)]
        [InlineData(2, true)]
        [InlineData(8, true)]
        [InlineData(1, false)]
        [InlineData(9, false)]
        public void Less_TrueWhenNumberInside(int v, bool expected)
        {
            var d = new Diapason(2, 8);
            Assert.Equal(expected, d < v);
            Assert.Equal(!expected, d > v);
        }

        [Fact]
        public void ToString_InvariantFormat()
        {
            Assert.Equal("[1.5; 2]", new Diapason(1.5, 2).ToString());
        }

        [Fact]
        public void Print_WritesToConsole()
        {
            var old = Console.Out;
            var sw = new System.IO.StringWriter();
            Console.SetOut(sw);
            try { new Diapason(1, 2).Print(); }
            finally { Console.SetOut(old); }
            Assert.Contains("x = ", sw.ToString());
            Assert.Contains("y = ", sw.ToString());
        }
    }
}