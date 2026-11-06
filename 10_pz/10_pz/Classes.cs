using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace lab10
{
    public interface IInit
    {
        void Init();
        void RandomInit();
    }
    public static class Input
    {
        public static string GetString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? "";
        }
        public static int GetInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int v) && v >= min && v <= max) return v;
                Console.WriteLine("Некорректное значение, повторите ввод.");
            }
        }
        public static double GetDouble(string prompt, double min = double.MinValue, double max = double.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double v) && v >= min && v <= max) return v;
                Console.WriteLine("Некорректное значение, повторите ввод.");
            }
        }
    }
    public class Organization : IInit, IComparable, ICloneable
    {
        private string _name;
        private int employers;
        protected static Random random = new Random();
        public static readonly string[] Names =
            { "Альфа", "Гарант", "Север", "Восток", "Меридиан", "Спектр", "Орион", "Квант" };
        public string Name { get => _name; set => _name = string.IsNullOrWhiteSpace(value) ? "БЕЗ НАЗВАНИЯ" : value; }
        public int Employers { get => employers; set => employers = value >= 0 ? value : 0; }
        public Organization() { _name = "БЕЗ ПАРАМЕТРОВ"; employers = 0; }
        public Organization(string name, int count) { Name = name; Employers = count; }
        public Organization(Organization obj) { _name = obj._name; employers = obj.employers; }
        public virtual void Show() { Console.WriteLine($"Организация: {_name}, сотрудников: {employers}"); }
        public virtual void Init()
        {
            Name = Input.GetString("Название: ");
            Employers = Input.GetInt("Число сотрудников: ", 0);
        }
        public virtual void RandomInit()
        {
            _name = Names[random.Next(Names.Length)];
            employers = random.Next(5, 5000);
        }
        public void ShowNoVirtual() { Console.WriteLine($"[Organization.ShowNoVirtual] {_name}, сотрудников: {employers}"); }
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;
            Organization o = (Organization)obj;
            return _name == o._name && employers == o.employers;
        }
        public override int GetHashCode() => HashCode.Combine(_name, employers);
        public int CompareTo(object obj)
        {
            if (!(obj is Organization o)) throw new ArgumentException("Объект не является Organization");
            return string.Compare(_name, o._name, StringComparison.Ordinal);
        }
        public virtual object Clone() => new Organization(this);
        public Organization ShallowCopy() => (Organization)MemberwiseClone();
    }
    public class Insurance : Organization
    {
        private string region;
        private double fund;
        public static readonly string[] Regions = { "Центр", "Урал", "Сибирь", "Юг" };
        public string Region { get => region; set => region = string.IsNullOrWhiteSpace(value) ? "Не указан" : value; }
        public double Fund { get => fund; set => fund = value >= 0 ? value : 0; }
        public Insurance() : base() { region = "Не указан"; fund = 0; }
        public Insurance(string name, int count, string region, double fund) : base(name, count)
        { Region = region; Fund = fund; }
        public Insurance(Insurance obj) : base(obj) { region = obj.region; fund = obj.fund; }
        public override void Show() { Console.WriteLine($"Страховая компания: {Name}, сотрудников: {Employers}, регион: {region}, фонд: {fund:F1} млн руб."); }
        public new void ShowNoVirtual() { Console.WriteLine($"[Insurance.ShowNoVirtual] {Name}, регион: {region}, фонд: {fund:F1}"); }
        public override void Init()
        {
            base.Init();
            Region = Input.GetString("Регион: ");
            Fund = Input.GetDouble("Страховой фонд (млн руб.): ", 0);
        }
        public override void RandomInit()
        {
            base.RandomInit();
            region = Regions[random.Next(Regions.Length)];
            fund = Math.Round(random.NextDouble() * 1000, 1);
        }
        public override bool Equals(object obj) =>
            base.Equals(obj) && obj is Insurance o && region == o.region && fund == o.fund;
        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), region, fund);
        public override object Clone() => new Insurance(this);
    }
    public class ShipCompany : Organization
    {
        private int ships;
        private double displacement;
        public int Ships { get => ships; set => ships = value >= 0 ? value : 0; }
        public double Displacement { get => displacement; set => displacement = value >= 0 ? value : 0; }
        public ShipCompany() : base() { ships = 0; displacement = 0; }
        public ShipCompany(string name, int count, int ships, double displacement) : base(name, count)
        { Ships = ships; Displacement = displacement; }
        public ShipCompany(ShipCompany obj) : base(obj)
        { ships = obj.ships; displacement = obj.displacement; }
        public override void Show() { Console.WriteLine($"Судостроительная компания: {Name}, сотрудников: {Employers}, построено судов: {ships}, ср. водоизмещение: {displacement:F0} т"); }
        public new void ShowNoVirtual() { Console.WriteLine($"[ShipCompany.ShowNoVirtual] {Name}, судов: {ships}"); }
        public override void Init()
        {
            base.Init();
            Ships = Input.GetInt("Построено судов: ", 0);
            Displacement = Input.GetDouble("Среднее водоизмещение (т): ", 0);
        }
        public override void RandomInit()
        {
            base.RandomInit();
            ships = random.Next(1, 200);
            displacement = random.Next(500, 50000);
        }
        public override bool Equals(object obj) =>
            base.Equals(obj) && obj is ShipCompany o && ships == o.ships && displacement == o.displacement;
        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), ships, displacement);
        public override object Clone() => new ShipCompany(this);
    }
    public class Factory : Organization
    {
        private int engineers;
        private string[] shops;
        public static readonly string[] FactoryNames = { "Заря", "Прогресс", "Маяк" };
        public static readonly string[] ShopNames =
            { "Литейный", "Сборочный", "Механический", "Сварочный", "Покрасочный", "Инструментальный" };
        public int Engineers { get => engineers; set => engineers = value >= 0 ? value : 0; }
        public string[] Shops { get => shops; set => shops = value ?? new string[0]; }
        public Factory() : base() { engineers = 0; shops = new string[0]; }
        public Factory(string name, int count, int engineers, string[] shops) : base(name, count)
        { Engineers = engineers; Shops = shops; }
        public Factory(Factory obj) : base(obj)
        { engineers = obj.engineers; shops = (string[])obj.shops.Clone(); }
        public override void Show() { Console.WriteLine($"Завод: {Name}, сотрудников: {Employers}, инженеров: {engineers}, цеха: {string.Join(", ", shops)}"); }
        public new void ShowNoVirtual() { Console.WriteLine($"[Factory.ShowNoVirtual] {Name}, инженеров: {engineers}"); }
        public override void Init()
        {
            base.Init();
            Engineers = Input.GetInt("Число инженеров: ", 0);
            string s = Input.GetString("Цеха (через запятую): ");
            Shops = s.Split(',', StringSplitOptions.RemoveEmptyEntries)
                         .Select(x => x.Trim()).ToArray();
        }
        public override void RandomInit()
        {
            base.RandomInit();
            Name = FactoryNames[random.Next(FactoryNames.Length)];
            engineers = random.Next(1, Math.Max(2, Employers / 5));
            int n = random.Next(2, 5);
            shops = ShopNames.OrderBy(x => random.Next()).Take(n).ToArray();
        }
        public override bool Equals(object obj) =>
            base.Equals(obj) && obj is Factory o && engineers == o.engineers
            && shops.SequenceEqual(o.shops);
        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), engineers, shops.Length);
        public override object Clone() => new Factory(this);
    }
    public class Library : Organization
    {
        private string city;
        private int books;
        public static readonly string[] Cities = { "Москва", "Казань", "Тула", "Омск" };
        public string City { get => city; set => city = string.IsNullOrWhiteSpace(value) ? "Не указан" : value; }
        public int Books { get => books; set => books = value >= 0 ? value : 0; }
        public Library() : base() { city = "Не указан"; books = 0; }
        public Library(string name, int count, string city, int books) : base(name, count)
        { City = city; Books = books; }
        public Library(Library obj) : base(obj) { city = obj.city; books = obj.books; }
        public override void Show() { Console.WriteLine($"Библиотека: {Name}, сотрудников: {Employers}, город: {city}, книг: {books}"); }
        public new void ShowNoVirtual() { Console.WriteLine($"[Library.ShowNoVirtual] {Name}, город: {city}"); }
        public override void Init()
        {
            base.Init();
            City = Input.GetString("Город: ");
            Books = Input.GetInt("Количество книг: ", 0);
        }
        public override void RandomInit()
        {
            base.RandomInit();
            city = Cities[random.Next(Cities.Length)];
            books = random.Next(1000, 500000);
        }
        public override bool Equals(object obj) =>
            base.Equals(obj) && obj is Library o && city == o.city && books == o.books;
        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), city, books);
        public override object Clone() => new Library(this);
    }
    public class Founder : IInit
    {
        private static Random random = new Random();
        private string fullName = "";
        private double share;
        public string FullName { get => fullName; set => fullName = value ?? ""; }
        public double Share { get => share; set => share = (value >= 0 && value <= 100) ? value : 0; }
        public void Init()
        {
            FullName = Input.GetString("ФИО учредителя: ");
            Share = Input.GetDouble("Доля (%): ", 0, 100);
        }
        public void RandomInit()
        {
            string[] names = { "Иванов И.И.", "Петрова А.С.", "Сидоров П.К.", "Кузнецова М.В." };
            fullName = names[random.Next(names.Length)];
            share = random.Next(1, 100);
        }
        public void Show() => Console.WriteLine($"Учредитель: {fullName}, доля: {share}%");
    }
    public class SortByEmployers : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            Organization a = (Organization)x;
            Organization b = (Organization)y;
            return a.Employers.CompareTo(b.Employers);
        }
    }
}
