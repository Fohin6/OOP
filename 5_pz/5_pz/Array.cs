using System;

namespace _5_pz
{
    internal class Array
    {
        public void MenuArray()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Работа с массивами — вариант 20");
                    Console.WriteLine("1. Матрица: добавить строку после каждой чётной строки");
                    Console.WriteLine("2. Ступенчатый массив: добавить строку в конец");
                    Console.WriteLine("3. Назад");
                    Console.Write("Ваш выбор: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Matrix();
                            ConsoleHelper.Wait();
                            break;
                        case "2":
                            StupArray();
                            ConsoleHelper.Wait();
                            break;
                        case "3":
                            return;
                        default:
                            throw new WrongInp("Выберите пункт 1, 2 или 3.");
                    }
                }
                catch (WrongInp ex)
                {
                    Console.WriteLine(ex.Message);
                    ConsoleHelper.Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при работе с массивами: " + ex.Message);
                    ConsoleHelper.Wait();
                }
            }
        }

        void Matrix()
        {
            int rows = ReadPositive("Количество строк: ");
            int cols = ReadPositive("Количество столбцов: ");
            int[,] a = new int[rows, cols];

            Console.WriteLine("Способ заполнения: 1 — вручную, 2 — случайно");
            int way = ReadChoice("Ваш выбор: ", 1, 2);

            if (way == 1)
            {
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < cols; j++)
                        a[i, j] = ReadNumber("Элемент [" + (i + 1) + "," + (j + 1) + "]: ");
            }
            else
            {
                Random rnd = new Random();
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < cols; j++)
                        a[i, j] = rnd.Next(1, 10);
            }

            int[,] b = new int[rows + rows / 2, cols];
            int k = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    b[k, j] = a[i, j];
                k++;
                if ((i + 1) % 2 == 0)
                    k++;
            }

            Console.WriteLine("\nИсходная матрица:");
            PrintMatrix(a);
            Console.WriteLine("\nМатрица после добавления строк:");
            PrintMatrix(b);
            Console.WriteLine("Добавленные строки заполнены нулями.");
        }

        void StupArray()
        {
            int rows = ReadPositive("Количество строк: ");
            int[][] a = new int[rows][];

            Console.WriteLine("Способ заполнения: 1 — вручную, 2 — случайно");
            int way = ReadChoice("Ваш выбор: ", 1, 2);
            Random rnd = new Random();

            for (int i = 0; i < rows; i++)
            {
                int len = ReadZeroOrMore("Количество элементов в строке " + (i + 1) + ": ");
                a[i] = new int[len];

                for (int j = 0; j < len; j++)
                {
                    if (way == 1)
                        a[i][j] = ReadNumber("Элемент: ");
                    else
                        a[i][j] = rnd.Next(1, 10);
                }
            }

            Console.WriteLine("\nИсходный ступенчатый массив:");
            PrintStup(a);

            int newLen = ReadZeroOrMore("Количество элементов в новой строке: ");
            int[] newRow = new int[newLen];
            for (int j = 0; j < newLen; j++)
            {
                if (way == 1)
                    newRow[j] = ReadNumber("Элемент новой строки: ");
                else
                    newRow[j] = rnd.Next(1, 10);
            }

            int[][] b = new int[rows + 1][];
            for (int i = 0; i < rows; i++)
                b[i] = a[i];
            b[rows] = newRow;

            Console.WriteLine("\nМассив после добавления строки:");
            PrintStup(b);
        }

        int ReadNumber(string text)
        {
            while (true)
            {
                Console.Write(text);
                if (int.TryParse(Console.ReadLine(), out int value))
                    return value;
                Console.WriteLine("Нужно ввести целое число, попробуйте ещё раз.");
            }
        }

        int ReadPositive(string text)
        {
            int value = ReadNumber(text);
            if (value <= 0)
                throw new WrongInp("Число должно быть больше нуля.");
            return value;
        }

        int ReadZeroOrMore(string text)
        {
            int value = ReadNumber(text);
            if (value < 0)
                throw new WrongInp("Число не может быть отрицательным.");
            return value;
        }

        int ReadChoice(string text, int min, int max)
        {
            int value = ReadNumber(text);
            if (value < min || value > max)
                throw new WrongInp("Выберите число от " + min + " до " + max + ".");
            return value;
        }

        void PrintMatrix(int[,] a, string title = "")
        {
            if (!string.IsNullOrEmpty(title))
                Console.WriteLine(title);

            if (a == null || a.Length == 0)
            {
                Console.WriteLine("Матрица пустая.");
                return;
            }

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                    Console.Write(a[i, j] + "\t");
                Console.WriteLine();
            }
        }

        void PrintStup(int[][] a, string title = "")
        {
            if (!string.IsNullOrEmpty(title))
                Console.WriteLine(title);

            if (a == null || a.Length == 0)
            {
                Console.WriteLine("Массив пустой.");
                return;
            }

            for (int i = 0; i < a.Length; i++)
            {
                Console.Write("Строка " + (i + 1) + ": ");
                if (a[i] == null || a[i].Length == 0)
                    Console.WriteLine("(пустая)");
                else
                {
                    for (int j = 0; j < a[i].Length; j++)
                        Console.Write(a[i][j] + " ");
                    Console.WriteLine();
                }
            }
        }
    }
}
