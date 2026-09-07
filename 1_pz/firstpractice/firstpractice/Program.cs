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
                        firstTask app = new firstTask();
                        break;
                    case 2:
                        secondTask app2 = new secondTask();
                        break;
                    case 3:
                        thirdTask app3 = new thirdTask();
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
                    Console.WriteLine($"Значение при {x} = {this.fourth(x)}");
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
                if (this.task2(x, y) == true) { Console.WriteLine("Точка принадлежит графику" + "\n"); }
                else { Console.WriteLine("Точка не принадлежит графику" + "\n"); }
                return;
            }
        }
        bool task2(double x, double y)
        {
            if (x >= -5 && x <= 0 && y >= 0 && y <= (x + 5)) { return true; }
            if (x > 0 && x <= 4 && y >= (7.0 / 4.0 * x - 7) && y <= (-5.0 / 4.0 * x + 5)) { return true; }
            return false;
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
            return (a - b) * (a - b) - (a * a + 2 * a * b) / (b * b);
        }
        private double calculationDouble(double a, double b)
        {
            return Math.Pow((a - b), 2) - (Math.Pow(a, 2) + 2 * a * b) / (b * b);
        }
    }
}
