using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Lab11
{
    public class Person : IComparable, ICloneable
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person() { Name = "Без имени"; Age = 18; }
        public Person(string name, int age) { Name = name; Age = age; }

        public virtual object Clone() => new Person(Name, Age);
        public int CompareTo(object obj)
        {
            Person p = obj as Person;
            if (p == null) throw new ArgumentException("Не Person");
            int byName = string.Compare(Name, p.Name, StringComparison.OrdinalIgnoreCase);
            return byName != 0 ? byName: Age.CompareTo(p.Age);
        }
        public override bool Equals(object obj)
        {
            Person p = obj as Person;
            return p != null && p.GetType() == typeof(Person) && Name == p.Name && Age == p.Age;
        }
        public override int GetHashCode() => HashCode.Combine(Name, Age);
        public override string ToString() => $"Person: {Name}, {Age} лет";
    }

    public class Student : Person
    {
        public string University { get; set; }
        public int Course { get; set; }

        public Student() { University = "Не указан"; Course = 1; }
        public Student(string name, int age, string university, int course) : base(name, age)
        {
            University = university; Course = course;
        }

        public Person BasePerson => new Person(Name, Age);

        public override object Clone() => new Student(Name, Age, University, Course);
        public override bool Equals(object obj)
        {
            Student s = obj as Student;
            return s != null && Name == s.Name && Age == s.Age && University == s.University && Course == s.Course;
        }
        public override int GetHashCode() => HashCode.Combine(Name, Age, University, Course);
        public override string ToString() => $"Student: {Name}, {Age} лет, {University}, {Course} курс";
    }
  /*################################
  #  __ __  _  _ _  ___  _  _     #
  # |  \  \| || | || . || || |    #
  # |     || ||   ||   || || |_   #
  # |_|_|_||_||_|_||_|_||_||___|  #
  ################################*/
    public class HashtableDemo
    {
        public Hashtable Table = new Hashtable();
        int nextKey = 1;

        public HashtableDemo()
        {
            Add(new Person("Иванов", 40));
            Add(new Student("Петров", 20, "МГУ", 2));
            Add(new Student("Сидоров", 19, "МИФИ", 1));
            Add(new Person("Алексеев", 35));
            Add(new Student("Кузнецов", 22, "МГТУ", 4));
        }

        public int Add(Person p) { Table.Add(nextKey, p); return nextKey++; }
        public bool Remove(int key) { bool ok = Table.ContainsKey(key); if (ok) Table.Remove(key); return ok; }

        public int CountStudents() { int c = 0; foreach (DictionaryEntry e in Table) if (e.Value is Student) c++; return c; }
        public List<Person> OnlyPersons()
        {
            var l = new List<Person>();
            foreach (DictionaryEntry e in Table) if (e.Value.GetType() == typeof(Person)) l.Add((Person)e.Value);
            return l;
        }
        public double AverageStudentAge()
        {
            int sum = 0, n = 0;
            foreach (DictionaryEntry e in Table) if (e.Value is Student s) { sum += s.Age; n++; }
            return n == 0 ? 0 : (double)sum / n;
        }

        public Hashtable ShallowClone() => (Hashtable)Table.Clone();
        public Hashtable DeepClone()
        {
            var h = new Hashtable();
            foreach (DictionaryEntry e in Table) h.Add(e.Key, ((ICloneable)e.Value).Clone());
            return h;
        }

        public Person[] Sorted()
        {
            var arr = new Person[Table.Count];
            Table.Values.CopyTo(arr, 0);
            Array.Sort(arr);
            return arr;
        }
        public List<int> FindKeysByName(string name)
        {
            var l = new List<int>();
            foreach (DictionaryEntry e in Table) if (((Person)e.Value).Name == name) l.Add((int)e.Key);
            return l;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n--- Задание 1: Hashtable ---");
                Console.WriteLine("1 Добавить  2 Удалить  3 Запросы  4 foreach");
                Console.WriteLine("5 Клонировать  6 Сортировка  7 Поиск  0 Назад");
                switch (Console.ReadLine())
                {
                    case "1": Add(Program.ReadPerson()); Console.WriteLine("Добавлено."); break;
                    case "2": Console.WriteLine(Remove(Program.ReadInt("Ключ: ")) ? "Удалено." : "Нет такого ключа."); break;
                    case "3":
                        Console.WriteLine($"1) Студентов: {CountStudents()}");
                        Console.WriteLine("2) Только Person:");
                        foreach (var p in OnlyPersons()) Console.WriteLine("   " + p);
                        Console.WriteLine($"3) Средний возраст студентов: {AverageStudentAge():F1}");
                        break;
                    case "4":
                        foreach (DictionaryEntry e in Table) Console.WriteLine($"[{e.Key}] {e.Value}");
                        break;
                    case "5":
                        var shallow = ShallowClone(); var deep = DeepClone();
                        var first = (Person)Table[1]; first.Name = "ИЗМЕНЕНО";
                        Console.WriteLine("Поверхностная копия: " + ((Person)shallow[1]).Name);
                        Console.WriteLine("Глубокая копия:      " + ((Person)deep[1]).Name);
                        first.Name = "Иванов";
                        break;
                    case "6": foreach (var p in Sorted()) Console.WriteLine("  " + p); break;
                    case "7":
                        Console.Write("Имя: ");
                        var keys = FindKeysByName(Console.ReadLine());
                        if (keys.Count == 0) Console.WriteLine("Не найден.");
                        foreach (int k in keys) Console.WriteLine($"[{k}] {Table[k]}");
                        break;
                    case "0": return;
                }
            }
        }
    }
}
