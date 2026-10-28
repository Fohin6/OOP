using System;
using System.Globalization;

namespace Lab9
{
    public class Program
    {
        public static Diapason FindMax(DiapasonArray a)
        {
            if (a.Length == 0)
                throw new InvalidOperationException("Массив пуст.");
            Diapason max = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (!a[i] > !max)
                    max = a[i];
            }
            return max;
        }

        static double ReadDouble(string input)
        {
            while (true)
            {
                Console.Write(input);
                string s = Console.ReadLine()?.Replace(',', '.');
                if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double v))
                    return v;
                Console.WriteLine("Ошибка: введите число.");
            }
        }

        static int ReadCount(string input)
        {
            while (true)
            {
                Console.Write(input);
                if (int.TryParse(Console.ReadLine(), out int v) && v >= 0)
                    return v;
                Console.WriteLine("Ошибка: введите целое число не меньше 0.");
            }
        }

        static DiapasonArray ReadArray()
        {
            int n = ReadCount("Сколько диапазонов ввести? ");
            Diapason[] items = new Diapason[n];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Диапазон №{i + 1}:");
                double x = ReadDouble("  начало (x): ");
                double y = ReadDouble("  конец  (y): ");
                items[i] = new Diapason(x, y);
            }
            return new DiapasonArray(items);
        }

        public static void Main()
        {
            Console.WriteLine("\n===== Часть 1: класс Diapason =====");
            Diapason d = new Diapason(2, 8);
            d.Print();
            Console.WriteLine("Статичный Contains(d, 5):   " + Diapason.Contains(d, 5));
            Console.WriteLine("Метод   d.Contains(10):  " + d.Contains(10));

            Console.WriteLine("\n===== Часть 2: перегруженные операции =====");
            Console.WriteLine("Длина !d:        " + !d);
            Diapason inc = d;
            inc++;
            Console.WriteLine("d++ :            " + inc + "  (исходный " + d + ")");
            Console.WriteLine("(int)d:          " + (int)d);
            double dy = d;
            Console.WriteLine("double = d:      " + dy);
            Console.WriteLine("d + 3:           " + (d + 3));
            Console.WriteLine("3 + d:           " + (3 + d));
            Console.WriteLine("d < 5 :          " + (d < 5));
            Console.WriteLine("d > 5 :          " + (d > 5));

            Console.WriteLine("\n===== Часть 3: DiapasonArray =====");
            DiapasonArray a1 = new DiapasonArray();
            Console.WriteLine("Пустой массив:");
            a1.Print();

            DiapasonArray a2 = new DiapasonArray(5, -10, 10);
            Console.WriteLine("Случайный массив:");
            a2.Print();
            Console.WriteLine("Максимальный (по длине): " + FindMax(a2));

            DiapasonArray a3 = ReadArray();
            Console.WriteLine("Массив, введённый с клавиатуры:");
            a3.Print();
            if (a3.Length > 0)
                Console.WriteLine("Максимальный (по длине): " + FindMax(a3));

            try { Console.WriteLine(a2[100]); }
            catch (ArgumentException ex) { Console.WriteLine("Ошибка индекса: " + ex.Message); }

            Console.WriteLine("\nСоздано массивов DiapasonArray: " + DiapasonArray.ObjectCount);
            Console.WriteLine("Создано объектов Diapason:      " + Diapason.ObjCount);
        }
    }
}