using lab10;
using System;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.IO;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace lab10test
{
    public class OrganizationTests
    {
        [Fact]
        public void DefaultConstructor_SetsDefaultValues()
        {
            var organization = new Organization();

            Assert.Equal("БЕЗ ПАРАМЕТРОВ", organization.Name);
            Assert.Equal(0, organization.Employers);
        }

        [Theory]
        [InlineData(null, "БЕЗ НАЗВАНИЯ")]
        [InlineData("", "БЕЗ НАЗВАНИЯ")]
        [InlineData("   ", "БЕЗ НАЗВАНИЯ")]
        public void Name_InvalidValue_IsReplacedWithDefault(
            string? value, string expected)
        {
            var organization = new Organization();
            organization.Name = value!;

            Assert.Equal(expected, organization.Name);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Employers_NegativeValue_IsReplacedWithZero(int value)
        {
            var organization = new Organization("Тест", value);

            Assert.Equal(0, organization.Employers);
        }

        [Fact]
        public void Equals_ObjectsWithSameValues_ReturnsTrue()
        {
            var first = new Organization("Альфа", 100);
            var second = new Organization("Альфа", 100);

            Assert.True(first.Equals(second));
            Assert.Equal(first.GetHashCode(), second.GetHashCode());
        }

        [Fact]
        public void Equals_DifferentTypes_ReturnsFalse()
        {
            var organization = new Organization("Альфа", 100);
            var library = new Library("Альфа", 100, "Москва", 500);

            Assert.False(organization.Equals(library));
            Assert.False(library.Equals(organization));
        }

        [Fact]
        public void CompareTo_ComparesNames()
        {
            var first = new Organization("Альфа", 10);
            var second = new Organization("Бета", 10);

            Assert.True(first.CompareTo(second) < 0);
            Assert.True(second.CompareTo(first) > 0);

            Assert.Equal(0,
                first.CompareTo(new Organization("Альфа", 999)));
        }

        [Fact]
        public void CompareTo_InvalidObject_ThrowsException()
        {
            var organization = new Organization("Альфа", 10);

            Assert.Throws<ArgumentException>(() =>
            {
                organization.CompareTo("Альфа");
            });
        }

        [Fact]
        public void Clone_CreatesSeparateObject()
        {
            var original = new Organization("Орион", 50);

            var clone = Assert.IsType<Organization>(
                original.Clone());

            Assert.NotSame(original, clone);
            Assert.Equal(original, clone);
        }

        [Fact]
        public void ShallowCopy_CreatesSeparateObject()
        {
            var original = new Organization("Орион", 50);

            var copy = original.ShallowCopy();

            Assert.NotSame(original, copy);
            Assert.Equal(original, copy);
        }
    }
    public class DerivedOrganizationTests
    {

        [Fact]
        public void Insurance_InvalidValues_AreNormalized()
        {
            var company = new Insurance(
                "Страхование", -5, " ", -10);

            Assert.Equal(0, company.Employers);
            Assert.Equal("Не указан", company.Region);
            Assert.Equal(0, company.Fund);
        }

        [Fact]
        public void Insurance_Clone_PreservesValues()
        {
            var original = new Insurance(
                "Гарант", 120, "Урал", 250.5);

            var clone = Assert.IsType<Insurance>(
                original.Clone());

            Assert.NotSame(original, clone);
            Assert.Equal(original, clone);
        }

        [Fact]
        public void ShipCompany_InvalidValues_AreNormalized()
        {
            var company = new ShipCompany(
                "Верфь", 100, -2, -15);

            Assert.Equal(0, company.Ships);
            Assert.Equal(0, company.Displacement);
        }

        [Fact]
        public void ShipCompany_Clone_PreservesValues()
        {
            var original = new ShipCompany(
                "Верфь", 100, 12, 4500);

            var clone = Assert.IsType<ShipCompany>(
                original.Clone());

            Assert.NotSame(original, clone);
            Assert.Equal(original, clone);
        }

        [Fact]
        public void Factory_NullShops_BecomesEmptyArray()
        {
            var factory = new Factory(
                "Заря", 500, 40, null!);

            Assert.NotNull(factory.Shops);
            Assert.Empty(factory.Shops);
        }

        [Fact]
        public void Factory_Clone_CopiesShopsArray()
        {
            var original = new Factory(
                "Заря", 500, 40,
                new[] { "Литейный", "Сборочный" });

            var clone = Assert.IsType<Factory>(
                original.Clone());

            Assert.NotSame(original, clone);
            Assert.NotSame(original.Shops, clone.Shops);
            Assert.Equal(original, clone);

            original.Shops[0] = "Изменённый";

            Assert.Equal("Литейный", clone.Shops[0]);
        }

        [Fact]
        public void Factory_ShallowCopy_SharesShopsArray()
        {
            var original = new Factory(
                "Заря", 500, 40,
                new[] { "Литейный", "Сборочный" });

            var copy = original.ShallowCopy() as Factory;

            Assert.NotNull(copy);
            Assert.NotSame(original, copy);
            Assert.Same(original.Shops, copy.Shops);

            original.Shops[0] = "Изменённый";

            Assert.Equal("Изменённый", copy.Shops[0]);
        }

        [Fact]
        public void Factory_NegativeEngineers_BecomesZero()
        {
            var factory = new Factory(
                "Заря", 100, -1, Array.Empty<string>());

            Assert.Equal(0, factory.Engineers);
        }


        [Fact]
        public void Library_InvalidValues_AreNormalized()
        {
            var library = new Library(
                "Чтение", 10, "", -5);

            Assert.Equal("Не указан", library.City);
            Assert.Equal(0, library.Books);
        }

        [Fact]
        public void Library_Clone_PreservesValues()
        {
            var original = new Library(
                "Центральная", 20, "Москва", 1000);

            var clone = Assert.IsType<Library>(
                original.Clone());

            Assert.NotSame(original, clone);
            Assert.Equal(original, clone);
        }


        [Fact]
        public void Founder_InvalidShare_BecomesZero()
        {
            var founder = new Founder();

            founder.Share = 101;
            Assert.Equal(0, founder.Share);

            founder.Share = -1;
            Assert.Equal(0, founder.Share);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(25.5)]
        [InlineData(100)]
        public void Founder_ValidShare_IsPreserved(double share)
        {
            var founder = new Founder();

            founder.Share = share;

            Assert.Equal(share, founder.Share);
        }

        [Fact]
        public void Founder_NullName_BecomesEmptyString()
        {
            var founder = new Founder();

            founder.FullName = null!;

            Assert.Equal("", founder.FullName);
        }
    }

    public class SortingTests
    {
        [Fact]
        public void SortByEmployers_SortsAscending()
        {
            Organization[] organizations =
            {
                new Organization("Первый", 30),
                new Organization("Второй", 10),
                new Organization("Третий", 20)
            };

            Array.Sort(
                organizations,
                new SortByEmployers());

            Assert.Equal(
                new[] { 10, 20, 30 },
                Array.ConvertAll(
                    organizations,
                    x => x.Employers));
        }

        [Fact]
        public void ArraySort_SortsByName()
        {
            Organization[] organizations =
            {
                new Organization("Яблоко", 1),
                new Organization("Альфа", 2),
                new Organization("Море", 3)
            };

            Array.Sort(organizations);

            Assert.Equal(
                new[] { "Альфа", "Море", "Яблоко" },
                Array.ConvertAll(
                    organizations,
                    x => x.Name));
        }
    }

    public class InputTests
    {
        [Fact]
        public void GetString_ReturnsEnteredText()
        {
            var oldIn = Console.In;
            var oldOut = Console.Out;

            try
            {
                Console.SetIn(new StringReader(
                    "Привет" + Environment.NewLine));

                Console.SetOut(new StringWriter());

                string result = Input.GetString(
                    "Введите строку: ");

                Assert.Equal("Привет", result);
            }
            finally
            {
                Console.SetIn(oldIn);
                Console.SetOut(oldOut);
            }
        }

        [Fact]
        public void GetInt_InvalidInput_IsSkipped()
        {
            var oldIn = Console.In;
            var oldOut = Console.Out;

            try
            {
                Console.SetIn(new StringReader(
                    "не число" + Environment.NewLine +
                    "-5" + Environment.NewLine +
                    "12" + Environment.NewLine));

                var output = new StringWriter();
                Console.SetOut(output);

                int result = Input.GetInt(
                    "Число: ", 0, 20);

                Assert.Equal(12, result);

                Assert.Contains(
                    "Некорректное значение",
                    output.ToString());
            }
            finally
            {
                Console.SetIn(oldIn);
                Console.SetOut(oldOut);
            }
        }

        [Fact]
        public void GetDouble_ReturnsValidValue()
        {
            var oldIn = Console.In;
            var oldOut = Console.Out;
            var oldCulture = CultureInfo.CurrentCulture;

            try
            {
                CultureInfo.CurrentCulture =
                    CultureInfo.InvariantCulture;

                Console.SetIn(new StringReader(
                    "12.5" + Environment.NewLine));

                Console.SetOut(new StringWriter());

                double result = Input.GetDouble(
                    "Сумма: ", 0, 20);

                Assert.Equal(12.5, result, 5);
            }
            finally
            {
                Console.SetIn(oldIn);
                Console.SetOut(oldOut);
                CultureInfo.CurrentCulture = oldCulture;
            }
        }
    }
}