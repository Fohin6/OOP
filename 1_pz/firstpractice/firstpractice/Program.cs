using System;

namespace firstapp{
    class Program{ //option 20
        static void Main(){
            while (true)
            {
                Console.WriteLine("__/\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\__/\\\\\\\\\\\\\\\\\\\\\\\\\\____/\\\\\\______________/\\\\\\_        \r\n " +
                "_\\/\\\\\\///////////__\\/\\\\\\/////////\\\\\\_\\/\\\\\\_____________\\/\\\\\\_       \r\n  " +
                "_\\/\\\\\\_____________\\/\\\\\\_______\\/\\\\\\_\\/\\\\\\_____________\\/\\\\\\_      \r\n   " +
                "_\\/\\\\\\\\\\\\\\\\\\\\\\_____\\/\\\\\\\\\\\\\\\\\\\\\\\\\\/__\\//\\\\\\____/\\\\\\____/\\\\\\__     \r\n    " +
                "_\\/\\\\\\///////______\\/\\\\\\/////////_____\\//\\\\\\__/\\\\\\\\\\__/\\\\\\___    \r\n     " +
                "_\\/\\\\\\_____________\\/\\\\\\_______________\\//\\\\\\/\\\\\\/\\\\\\/\\\\\\____   \r\n      " +
                "_\\/\\\\\\_____________\\/\\\\\\________________\\//\\\\\\\\\\\\//\\\\\\\\\\_____  \r\n       " +
                "_\\/\\\\\\_____________\\/\\\\\\_________________\\//\\\\\\__\\//\\\\\\______ \r\n        " +
                "_\\///______________\\///___________________\\///____\\///_______");
                Console.WriteLine("1. Задание ");
                Console.WriteLine("2. Задание ");
                Console.WriteLine("3. Задание ");
                Console.WriteLine("0. Выход ");
                if (!int.TryParse(Console.ReadLine(), out int n))
                {
                    Console.WriteLine("\r\n[Ошибка] Пожалуйста, введите корректное число!\r\n");
                    continue;
                }

                switch (n)
                {
                    default:
                        Console.WriteLine("Нет такого значения");
                        break;
                    case 1:
                        new firstTask();
                        break;
                    case 2:
                        new secondTask();
                        break;
                    case 3:
                        new thirdTask();
                        break;
                    case 0:
                        return;

                }
            }
        }
    }
    class firstTask
    {
        public firstTask()
        {
            init();
        }
        void init()
        {
            while (true)
            {
                Console.Write("Введите значение переменной N: ");
                if (!int.TryParse(Console.ReadLine(), out int n))
                {
                    Console.WriteLine("\r\n[Ошибка] Пожалуйста, введите корректное число!\r\n");
                    continue;
                }
                Console.Write("Введите значение переменной M: ");
                if (!int.TryParse(Console.ReadLine(), out int m))
                {
                    Console.WriteLine("\r\n[Ошибка] Пожалуйста, введите корректное число!\r\n");
                    continue;
                }
                Console.WriteLine($"n++*--m: {this.first(n, m)}");
                Console.WriteLine($"return n--<m++: {this.second(n, m)}");
                Console.WriteLine($"--n>--m: {this.third(n, m)}" + "\n");

                for (int x = -40; x < 41; x++)
                {
                    Console.WriteLine($"Значение при x {x} = {this.fourth(x)}");
                }
                return;
            }
        }
        int first(int n, int m)
        {
            return n++ * --m;
        }
        bool second(int n, int m)
        {
            return n-- < m++;
        }
        bool third(int n, int m)
        {
            return --n > --m;
        }
        double fourth(int x)
        {
            return 5 * Math.Pow(x, 3) * Math.Pow((1 / Math.Pow(x, 2)) + (1 / Math.Pow(x, 3)), 1.0 / 5.0);
        }
    }
    class secondTask {
        public secondTask()
        {
            init();
        }
        void init()
        {
            while (true)
            {
                Console.Write("Введите значение переменной X: ");
                if (!double.TryParse(Console.ReadLine(), out double x))
                {
                    Console.WriteLine("\r\n[Ошибка] Пожалуйста, введите корректное число!\r\n");
                    continue;
                }
                Console.Write("Введите значение переменной Y: ");
                if (!double.TryParse(Console.ReadLine(), out double y))
                {
                    Console.WriteLine("\r\n[Ошибка] Пожалуйста, введите корректное число!\r\n");
                    continue;
                }
                bool belongs = task2(x, y);
                if (belongs) { Console.WriteLine("Точка принадлежит графику" + "\n"); }
                else { Console.WriteLine("Точка не принадлежит графику" + "\n"); }
                return;
            }
        }
        bool task2(double x, double y)
        {
            bool firstArea = x >= -5 && x <= 0 && y >= 0 && y <= x + 5;
            bool secondArea = x > 0 && x <= 4 && y >= 7.0 / 4.0 * x - 7 && y <= -5.0 / 4.0 * x + 5;
            bool belongs = firstArea || secondArea;
            return belongs;
        }
    }
    class thirdTask {
        public float a = 1000;
        public float b = 0.0001f;
        public double a2 = 1000;
        public double b2 = 0.0001f;
        public thirdTask()
        {
            init();
        }
        void init()
        {
            Console.WriteLine($"Значения при типе данных float: {this.calculationFloat(a, b)}");
            Console.WriteLine($"Значения при типе данных double: {this.calculationDouble(a2, b2)}" + "\n");
        }
        private float calculationFloat(float a, float b)
        {
            float c1 = a - b;

            float c2 = (float)Math.Pow(c1, 2);

            float aSquared = (float)Math.Pow(a, 2);
            float twoAB = 2 * a * b;
            float numerator = aSquared + twoAB;

            float bSquared = (float)Math.Pow(b, 2);

            float fraction = numerator / bSquared;

            float result = c2 - fraction;

            return result;
        }
        private double calculationDouble(double a, double b)
        {
            double c1 = a - b;
            double c2 = Math.Pow(c1, 2);

            double aSquared = Math.Pow(a, 2);
            double twoAB = 2 * a * b;
            double numerator = aSquared + twoAB;

            double bSquared = Math.Pow(b, 2);

            double fraction = numerator / bSquared;

            double result = c2 - fraction;

            return result;
        }
    }
}