using System;
using System.Globalization;

namespace Lab9
{
    public class Diapason
    {
        private double x;
        private double y;
        private static int objCount = 0;

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public static int ObjCount => objCount;

        public Diapason() : this(0, 0) { }

        public Diapason(double x, double y)
        {
            X = x;
            Y = y;
            objCount++;
        }
        public void Print()
        {
            Console.WriteLine($"x = {X}");
            Console.WriteLine($"y = {Y}");
        }

        public override string ToString() =>
            string.Create(CultureInfo.InvariantCulture, $"[{X}; {Y}]");

        public static bool Contains(Diapason d, double value)
        {
            double min = Math.Min(d.X, d.Y);
            double max = Math.Max(d.X, d.Y);
            return value >= min && value <= max;
        }

        public bool Contains(double value) => Contains(this, value);

        public static double operator !(Diapason d) => Math.Abs(d.Y - d.X);
        public static Diapason operator ++(Diapason d) => new Diapason(d.X + 1, d.Y + 1);

        public static explicit operator int(Diapason d) => (int)d.X;
        public static implicit operator double(Diapason d) => d.Y;

        public static Diapason operator +(Diapason d, int v) => new Diapason(d.X + v, d.Y + v);
        public static Diapason operator +(int v, Diapason d) => new Diapason(v + d.X, v + d.Y);

        public static bool operator <(Diapason d, int v) => Contains(d, v);
        public static bool operator >(Diapason d, int v) => !Contains(d, v);
    }
}