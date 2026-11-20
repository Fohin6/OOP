using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Lab11
{
    public class DictionaryDemo
    {
        public Dictionary<int, Person> Dict = new Dictionary<int, Person>();
        int nextKey = 1;

        public DictionaryDemo()
        {
            Add(new Person("Иванов", 40));
            Add(new Student("Петров", 20, "МГУ", 2));
            Add(new Student("Сидоров", 19, "МИФИ", 1));
            Add(new Person("Алексеев", 35));
            Add(new Student("Кузнецов", 22, "МГТУ", 4));
        }

        public int Add(Person p) { Dict.Add(nextKey, p); return nextKey++; }
        public bool Remove(int key) => Dict.Remove(key);

        public int CountStudents() => Dict.Values.Count(p => p is Student);
        public List<Person> OlderThan(int age) => Dict.Values.Where(p => p.Age > age).ToList();
        public int MaxCourse()
        {
            var st = Dict.Values.OfType<Student>().ToList();
            return st.Count == 0 ? 0 : st.Max(s => s.Course);
        }

        public Dictionary<int, Person> DeepClone() => Dict.ToDictionary(kv => kv.Key, kv => (Person)kv.Value.Clone());
        public List<KeyValuePair<int, Person>> SortedByAge() => Dict.OrderBy(kv => kv.Value.Age).ToList();
        public List<int> FindKeysByName(string name) => Dict.Where(kv => kv.Value.Name == name).Select(kv => kv.Key).ToList();

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n--- Задание 2: Dictionary<int, Person> ---");
                Console.WriteLine("1 Добавить  2 Удалить  3 Запросы  4 foreach");
                Console.WriteLine("5 Клонировать  6 Сортировка  7 Поиск  0 Назад");
                switch (Console.ReadLine())
                {
                    case "1": Add(Program.ReadPerson()); Console.WriteLine("Добавлено."); break;
                    case "2": Console.WriteLine(Remove(Program.ReadInt("Ключ: ")) ? "Удалено." : "Нет такого ключа."); break;
                    case "3":
                        Console.WriteLine($"1) Студентов: {CountStudents()}");
                        Console.WriteLine("2) Старше 30 лет:");
                        foreach (var p in OlderThan(30)) Console.WriteLine("   " + p);
                        Console.WriteLine($"3) Максимальный курс: {MaxCourse()}");
                        break;
                    case "4":
                        foreach (KeyValuePair<int, Person> kv in Dict) Console.WriteLine($"[{kv.Key}] {kv.Value}");
                        break;
                    case "5":
                        var deep = DeepClone();
                        Dict[1].Name = "ИЗМЕНЕНО";
                        Console.WriteLine($"Оригинал: {Dict[1].Name}, клон: {deep[1].Name}");
                        Dict[1].Name = "Иванов";
                        break;
                    case "6": foreach (var kv in SortedByAge()) Console.WriteLine($"  [{kv.Key}] {kv.Value}"); break;
                    case "7":
                        Console.Write("Имя: ");
                        var keys = FindKeysByName(Console.ReadLine());
                        if (keys.Count == 0) Console.WriteLine("Не найден.");
                        foreach (int k in keys) Console.WriteLine($"[{k}] {Dict[k]}");
                        break;
                    case "0": return;
                }
            }
        }
    }
}
