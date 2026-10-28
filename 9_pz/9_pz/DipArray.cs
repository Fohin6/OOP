using System;

namespace Lab9
{
    public class DiapasonArray
    {
        private Diapason[] arr;
        private static int objectCount = 0;
        private static readonly Random rnd = new Random();
        public static int ObjectCount => objectCount;
        public int Length => arr.Length;

        public DiapasonArray()
        {
            arr = new Diapason[0];
            objectCount++;
        }

        public DiapasonArray(int size, double min, double max)
        {
            if (size < 0) throw new ArgumentException("Размер массива не может быть отрицательным.");
            if (min > max) throw new ArgumentException("min не может быть больше max.");

            arr = new Diapason[size];
            for (int i = 0; i < size; i++)
            {
                double a = Math.Round(min + rnd.NextDouble() * (max - min), 2);
                double b = Math.Round(min + rnd.NextDouble() * (max - min), 2);
                arr[i] = new Diapason(a, b);
            }
            objectCount++;
        }

        public DiapasonArray(Diapason[] items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            arr = new Diapason[items.Length];
            for (int i = 0; i < items.Length; i++)
                arr[i] = items[i] ?? throw new ArgumentException("Элемент массива не может быть null.");
            objectCount++;
        }

        public Diapason this[int index]
        {
            get
            {
                CheckIndex(index);
                return arr[index];
            }
            set
            {
                CheckIndex(index);
                arr[index] = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= arr.Length)
                throw new ArgumentException($"Индекс {index} вне границ массива (0..{arr.Length - 1}).");
        }

        public void Print()
        {
            if (arr.Length == 0) { Console.WriteLine("Массив пуст."); return; }
            for (int i = 0; i < arr.Length; i++)
                Console.WriteLine($"[{i}] {arr[i]}  длина = {!arr[i]}");
        }
    }
}