using System;
using System.Linq;

namespace lab10
{
    class Program
    {
        static Random rnd = new Random();
        internal static Organization MakeObject(int type)
        {
            Organization o;
            switch (type)
            {
                case 1: o = new Insurance(); break;
                case 2: o = new ShipCompany(); break;
                case 3: o = new Factory(); break;
                case 4: o = new Library(); break;
                default: o = new Organization(); break;
            }
            o.RandomInit();
            return o;
        }
        static Organization[] MakeArray(int n)
        {
            Organization[] a = new Organization[n];
            for (int i = 0; i < n; i++)
                a[i] = MakeObject(i < 5 ? i : rnd.Next(5));
            return a;
        }
        static void ShowArray(Organization[] a) { foreach (Organization o in a) o.Show(); }
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Organization[] arr = MakeArray(10);
            while (true)
            {
                Console.WriteLine("\n===== Лабораторная работа №10, вариант 4 =====");
                Console.WriteLine("1. Часть 1: виртуальные и невиртуальные методы, Equals");
                Console.WriteLine("2. Часть 2: запросы (is / as)");
                Console.WriteLine("3. Часть 3: IComparable, IComparer, бинарный поиск");
                Console.WriteLine("4. Часть 3: массив IInit");
                Console.WriteLine("5. Часть 3: клонирование и ShallowCopy");
                Console.WriteLine("6. Пересоздать массив");
                Console.WriteLine("0. Выход");
                int choice = Input.GetInt("Выбор: ", 0, 6);
                Console.WriteLine();
                switch (choice)
                {
                    case 1: Part1(arr); break;
                    case 2: Part2(arr); break;
                    case 3: Part3Sort(arr); break;
                    case 4: Part3IInit(); break;
                    case 5: Part3Clone(); break;
                    case 6: arr = MakeArray(10); Console.WriteLine("Массив создан заново."); break;
                    case 0: return;
                }
            }
        }
        static void Part1(Organization[] arr)
        {
            Console.WriteLine("--- Виртуальный метод Show() (вызывается версия по типу объекта) ---");
            ShowArray(arr);
            Console.WriteLine("\n--- Невиртуальный метод ShowNoVirtual() (версия по типу ссылки Organization) ---");
            foreach (Organization o in arr) o.ShowNoVirtual();
            Console.WriteLine("\nРазница: Show() объявлен virtual и переопределён (override) в потомках, поэтому");
            Console.WriteLine("версия выбирается во время выполнения по реальному типу объекта (позднее связывание).");
            Console.WriteLine("ShowNoVirtual() скрыт через new, версия выбирается при компиляции по типу ссылки,");
            Console.WriteLine("поэтому через ссылку Organization всегда вызывается базовая версия.");
            Console.WriteLine("\n--- Equals ---");
            Library l1 = new Library("Центральная", 20, "Москва", 1000);
            Library l2 = new Library(l1);
            Library l3 = new Library("Центральная", 20, "Тула", 1000);
            Console.WriteLine($"l1.Equals(l2) = {l1.Equals(l2)}");
            Console.WriteLine($"l1.Equals(l3) = {l1.Equals(l3)}");
            Console.WriteLine($"l1.Equals(new Organization(\"Центральная\", 20)) = {l1.Equals(new Organization("Центральная", 20))}");
            Console.WriteLine("\n--- Ввод объекта с клавиатуры (Init) ---");
            if (Input.GetInt("Ввести библиотеку вручную? (1 - да, 0 - нет): ", 0, 1) == 1)
            {
                Library lib = new Library();
                lib.Init();
                lib.Show();
            }
        }
        static void Part2(Organization[] arr)
        {
            Console.WriteLine("Текущий массив:");
            ShowArray(arr);
            Console.WriteLine("\nЗапросы:");
            Console.WriteLine("1. Суммарный страховой фонд всех страховых компаний региона");
            Console.WriteLine("2. Количество инженеров на заводе (по названию)");
            Console.WriteLine("3. Количество библиотек в городе и суммарное число книг в них");
            Console.WriteLine("4. Наименование всех цехов на заводе (по названию)");
            int q = Input.GetInt("Выбор запроса: ", 1, 4);
            switch (q)
            {
                case 1:
                    {
                        Console.WriteLine("Регионы: " + string.Join(", ", Insurance.Regions));
                        string region = Input.GetString("Регион: ");
                        double sum = 0;
                        foreach (Organization o in arr)
                            if (o is Insurance ic && ic.Region == region)
                                sum += ic.Fund;
                        Console.WriteLine($"Суммарный страховой фонд региона «{region}»: {sum:F1} млн руб.");
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Заводы: " + string.Join(", ", Factory.FactoryNames));
                        string name = Input.GetString("Название завода: ");
                        int count = 0;
                        foreach (Organization o in arr)
                        {
                            Factory f = o as Factory;
                            if (f != null && f.Name == name) count += f.Engineers;
                        }
                        Console.WriteLine($"Инженеров на заводе «{name}»: {count}");
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Города: " + string.Join(", ", Library.Cities));
                        string city = Input.GetString("Город: ");
                        int libs = 0; long books = 0;
                        foreach (Organization o in arr)
                            if (o is Library lib && lib.City == city)
                            { libs++; books += lib.Books; }
                        Console.WriteLine($"В городе «{city}» библиотек: {libs}, книг в них: {books}");
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Заводы: " + string.Join(", ", Factory.FactoryNames));
                        string name = Input.GetString("Название завода: ");
                        bool found = false;
                        foreach (Organization o in arr)
                        {
                            Factory f = o as Factory;
                            if (f != null && f.Name == name)
                            {
                                found = true;
                                Console.WriteLine("Цеха: " + string.Join(", ", f.Shops));
                            }
                        }
                        if (!found) Console.WriteLine("Завод не найден.");
                        break;
                    }
            }
        }
        static void Part3Sort(Organization[] source)
        {
            Organization[] arr = (Organization[])source.Clone();
            Console.WriteLine("--- Сортировка по названию (IComparable, Array.Sort) ---");
            Array.Sort(arr);
            ShowArray(arr);
            Console.WriteLine("\n--- Сортировка по числу сотрудников (IComparer, Array.Sort) ---");
            Array.Sort(arr, new SortByEmployers());
            ShowArray(arr);
            Array.Sort(arr);
            Console.WriteLine("\nНазвания в массиве: " + string.Join(", ", arr.Select(x => x.Name).Distinct()));
            string key = Input.GetString("Название для бинарного поиска: ").Trim();
            int idx = BinSearch(arr, key);
            Console.WriteLine(idx >= 0
                ? $"Собственный бинарный поиск: найден элемент №{idx}:"
                : "Собственный бинарный поиск: не найдено.");
            if (idx >= 0) arr[idx].Show();
            int idx2 = Array.BinarySearch(arr, new Organization(key, 0));
            Console.WriteLine(idx2 >= 0 ? $"Array.BinarySearch: индекс {idx2}" : "Array.BinarySearch: не найдено.");
        }
        internal static int BinSearch(Organization[] a, string name)
        {
            int lo = 0, hi = a.Length - 1;
            while (lo <= hi)
            {
                int mid = (lo + hi) / 2;
                int c = string.Compare(a[mid].Name, name, StringComparison.Ordinal);
                if (c == 0) return mid;
                if (c < 0) lo = mid + 1; else hi = mid - 1;
            }
            return -1;
        }
        static void Part3IInit()
        {
            IInit[] items = new IInit[]
            {
                new Organization(), new Insurance(), new ShipCompany(),
                new Factory(), new Library(), new Founder()
            };
            Console.WriteLine("--- RandomInit() для всех элементов ---");
            foreach (IInit it in items) it.RandomInit();
            ShowIInit(items);
            Console.WriteLine("\n--- Init() (ввод с клавиатуры) для выбранного элемента ---");
            for (int i = 0; i < items.Length; i++)
                Console.WriteLine($"{i}: {items[i].GetType().Name}");
            int k = Input.GetInt("Номер элемента: ", 0, items.Length - 1);
            items[k].Init();
            ShowIInit(items);
        }
        static void ShowIInit(IInit[] items)
        {
            foreach (IInit it in items)
            {
                if (it is Organization o) o.Show();
                else if (it is Founder f) f.Show();
            }
        }
        static void Part3Clone()
        {
            Factory original = new Factory("Заря", 500, 50, new[] { "Литейный", "Сборочный" });
            Factory shallow = (Factory)original.ShallowCopy();
            Factory clone = (Factory)original.Clone();
            Console.WriteLine("До изменения:");
            original.Show(); shallow.Show(); clone.Show();
            original.Shops[0] = "ИЗМЕНЁННЫЙ";
            original.Name = "Новое имя";
            Console.WriteLine("\nПосле изменения оригинала (цех[0] и название):");
            Console.Write("Оригинал:            "); original.Show();
            Console.Write("ShallowCopy:         "); shallow.Show();
            Console.Write("Clone (глубокий):    "); clone.Show();
            Console.WriteLine("\nShallowCopy копирует поля значений, но массив цехов остаётся общим (ссылка),");
            Console.WriteLine("поэтому изменение цеха видно и в оригинале, и в поверхностной копии.");
            Console.WriteLine("Clone создаёт независимый массив — клон не изменился.");
            Console.WriteLine($"ReferenceEquals(original.Shops, shallow.Shops) = {ReferenceEquals(original.Shops, shallow.Shops)}");
            Console.WriteLine($"ReferenceEquals(original.Shops, clone.Shops)   = {ReferenceEquals(original.Shops, clone.Shops)}");
        }
    }
}
