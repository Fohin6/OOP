using System;

namespace thirdpz
{
    class Programm //option 20
    {
        public static void Main()
        {
            Console.WriteLine($" ____  ____  _     ____ \r\n/   _\\/  _ \\/ \\   /   _\\\r\n" +
                $"|  /  | / \\|| |   |  /  \r\n" +
                $"|  \\__| |-||| |_/\\|  \\__\r\n" +
                $"\\____/\\_/ \\|\\____/\\____/\r\n");
            double a = 0.1;
            double b = 1.0;
            int n = 30;
            double eps = 0.0001;
            double step = (b - a) / 9.0;
            Calculate.PrintCalc();
            for (int i = 0; i < 10; i++)
            {
                double x = a + i * step;
                double sn = Calculate.SN(x, n);
                double se = Calculate.SE(x, eps);
                double y = Calculate.Func(x);

                Console.WriteLine(
                    $"{x:F4}\t{sn:F8}\t{se:F8}\t{y:F8}");
            }
        }
    }
    class Calculate
    {
        public static double SN(double x, int n) //вычисление суммы ряда из n
        {
            double sum = 1.0; //начало ряда
            double term = 1.0; //начальный член ряда
            for (int i = 1; i <= n; i++) {
                term = term * (i * i + 1.0) / ((i-1)*(i-1)+1.0) * (x / 2.0) / i;
                sum += term;
            }
            return sum;
        }
        public static double SE(double x, double eps) //вычесление с точностью
        {
            double sum = 1.0;
            double term = 1.0;
            int i = 0;
            while (Math.Abs(term) >= eps)
            {
                i++;
                term = term * (i * i + 1.0) / ((i - 1) * (i - 1) + 1.0) * (x / 2.0) / i;
                sum += term;
            }
            return sum;
        }
        public static double Func(double x) //точное занчение функции
        {
            return (x * x / 4.0 + x / 2.0 + 1.0) * Math.Exp(x / 2.0);
        }
        public static void PrintCalc() //вывод
        {
            Console.WriteLine("\tВычеселение функции");
            Console.WriteLine("x\t\tSN\t\tSE\t\tY");
        }
    }
}