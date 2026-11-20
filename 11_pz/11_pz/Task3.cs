using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Lab11
{
    public class TestCollections
    {
        public Stack<Student> Col1 = new Stack<Student>();
        public Stack<string> Col1Str = new Stack<string>();
        public Dictionary<Person, Student> Col2 = new Dictionary<Person, Student>();
        public Dictionary<string, Student> Col2Str = new Dictionary<string, Student>();
        public List<Student> Source = new List<Student>();

        static readonly string[] Univ = { "МГУ", "МИФИ", "МГТУ", "СПбГУ", "НГУ" };

        public TestCollections(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Student s = Generate(i);
                Source.Add(s);
                Add(s);
            }
        }

        public static Student Generate(int i) =>
            new Student("Студент" + i, 17 + i % 10, Univ[i % Univ.Length], 1 + i % 5);

        public void Add(Student s)
        {
            Col1.Push(s);
            Col1Str.Push(s.ToString());
            Col2.Add(s.BasePerson, s);
            Col2Str.Add(s.ToString(), s);
        }

        public bool Remove(Student s)
        {
            string str = s.ToString();
            bool removed = RemoveFromStack(Col1, x => x.Equals(s));
            RemoveFromStack(Col1Str, x => x == str);
            Col2.Remove(s.BasePerson);
            Col2Str.Remove(str);
            return removed;
        }

        static bool RemoveFromStack<T>(Stack<T> st, Func<T, bool> match)
        {
            bool removed = false;
            var tmp = new Stack<T>();
            while (st.Count > 0)
            {
                T x = st.Pop();
                if (!removed && match(x)) removed = true; else tmp.Push(x);
            }
            while (tmp.Count > 0) st.Push(tmp.Pop());
            return removed;
        }

        static long Measure(Func<bool> action, out bool result)
        {
            Stopwatch sw = Stopwatch.StartNew();
            result = action();
            sw.Stop();
            return sw.ElapsedTicks;
        }

        static string Yn(bool b) => b ? "да" : "нет";

        public void RunSearchTests()
        {
            int n = Source.Count;
            Student Copy(Student s) => new Student(s.Name, s.Age, s.University, s.Course);

            Student w = Copy(Source[0]);
            Col1.Contains(w); Col1Str.Contains(w.ToString());
            Col2.ContainsKey(w.BasePerson); Col2Str.ContainsKey(w.ToString()); Col2.ContainsValue(w);

            var targets = new[]
            {
                ("Первый", Copy(Source[0])),
                ("Центральный", Copy(Source[n / 2])),
                ("Последний", Copy(Source[n - 1])),
                ("Отсутствует", new Student("НетТакого", 99, "Нигде", 9)),
            };

            Console.WriteLine($"\nЭлементов: {n}. Время в тиках (Frequency = {Stopwatch.Frequency} Гц)\n");
            Console.WriteLine("{0,-13}|{1,-15}|{2,-15}|{3,-17}|{4,-17}|{5,-17}",
                "Элемент", "Stack<Student>", "Stack<string>", "Dict<Person,St>", "Dict<string,St>", "ContainsValue");
            Console.WriteLine(new string('-', 99));

            foreach (var (title, s) in targets)
            {
                string str = s.ToString();
                Person key = new Person(s.Name, s.Age);
                bool r1, r2, r3, r4, r5;
                long t1 = Measure(() => Col1.Contains(s), out r1);
                long t2 = Measure(() => Col1Str.Contains(str), out r2);
                long t3 = Measure(() => Col2.ContainsKey(key), out r3);
                long t4 = Measure(() => Col2Str.ContainsKey(str), out r4);
                long t5 = Measure(() => Col2.ContainsValue(s), out r5);
                Console.WriteLine("{0,-13}|{1,-15}|{2,-15}|{3,-17}|{4,-17}|{5,-17}", title,
                    $"{t1} ({Yn(r1)})", $"{t2} ({Yn(r2)})", $"{t3} ({Yn(r3)})", $"{t4} ({Yn(r4)})", $"{t5} ({Yn(r5)})");
            }
        }
    }

    public class Program
    {
        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int v)) return v;
                Console.WriteLine("Введите целое число.");
            }
        }

        public static Person ReadPerson()
        {
            Console.Write("1 - Person, 2 - Student: ");
            string t = Console.ReadLine();
            Console.Write("Имя: "); string name = Console.ReadLine();
            int age = ReadInt("Возраст: ");
            if (t != "2") return new Person(name, age);
            Console.Write("ВУЗ: "); string u = Console.ReadLine();
            return new Student(name, age, u, ReadInt("Курс: "));
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            var h = new HashtableDemo();
            var d = new DictionaryDemo();
            TestCollections tc = null;

            while (true)
            {
                Console.WriteLine("\n=== Практическая работа №11 (вариант 20) ===");
                Console.WriteLine("1 Задание 1 (Hashtable)");
                Console.WriteLine("2 Задание 2 (Dictionary<K,T>)");
                Console.WriteLine("3 Задание 3: создать TestCollections и замерить поиск");
                Console.WriteLine("4 Задание 3: добавить / удалить элемент");
                Console.WriteLine("0 Выход");
                switch (Console.ReadLine())
                {
                    case "1": h.Run(); break;
                    case "2": d.Run(); break;
                    case "3": tc = new TestCollections(1000); tc.RunSearchTests(); break;
                    case "4":
                        if (tc == null) { Console.WriteLine("Сначала пункт 3."); break; }
                        Student s = TestCollections.Generate(5000);
                        tc.Add(s);
                        Console.WriteLine($"Добавлен: {s}. Stack: {tc.Col1.Count}, Dictionary: {tc.Col2.Count}");
                        tc.Remove(s);
                        Console.WriteLine($"Удален.   Stack: {tc.Col1.Count}, Dictionary: {tc.Col2.Count}");
                        break;
                    case "0": return;
                }
            }
        }
    }
}
