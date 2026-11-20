using Lab11;
using System;
using System.Collections;
using System.Linq;
using Xunit;

namespace Lab11.Tests
{
    public class PersonTests
    {
        [Fact]
        public void Equals_ComparesByContent()
        {
            Assert.Equal(new Person("Аня", 20), new Person("Аня", 20));
            Assert.NotEqual(new Person("Аня", 20), new Person("Аня", 21));
        }

        [Fact]
        public void Student_BasePerson_ReturnsBaseObjectWithSameData()
        {
            var s = new Student("Боб", 21, "МГУ", 3);
            Person p = s.BasePerson;
            Assert.Equal(typeof(Person), p.GetType());
            Assert.Equal("Боб", p.Name);
            Assert.Equal(21, p.Age);
        }

        [Fact]
        public void Clone_CreatesIndependentCopy()
        {
            var s = new Student("Боб", 21, "МГУ", 3);
            var c = (Student)s.Clone();
            Assert.Equal(s, c);
            Assert.NotSame(s, c);
            c.Name = "Другой";
            Assert.Equal("Боб", s.Name);
        }

        [Fact]
        public void CompareTo_SortsByName()
        {
            Assert.True(new Person("А", 1).CompareTo(new Person("Б", 1)) < 0);
        }
    }

    public class HashtableDemoTests
    {
        [Fact]
        public void InitialCollection_HasFiveElements() => Assert.Equal(5, new HashtableDemo().Table.Count);

        [Fact]
        public void AddAndRemove_Work()
        {
            var d = new HashtableDemo();
            int key = d.Add(new Person("Новый", 30));
            Assert.Equal(6, d.Table.Count);
            Assert.True(d.Remove(key));
            Assert.False(d.Remove(key));
            Assert.Equal(5, d.Table.Count);
        }

        [Fact]
        public void Queries_ReturnExpectedValues()
        {
            var d = new HashtableDemo();
            Assert.Equal(3, d.CountStudents());
            Assert.Equal(2, d.OnlyPersons().Count);
            Assert.Equal((20 + 19 + 22) / 3.0, d.AverageStudentAge(), 5);
        }

        [Fact]
        public void ShallowClone_SharesObjects_DeepClone_DoesNot()
        {
            var d = new HashtableDemo();
            var shallow = d.ShallowClone();
            var deep = d.DeepClone();
            Assert.Same(d.Table[1], shallow[1]);
            Assert.NotSame(d.Table[1], deep[1]);
            Assert.Equal(d.Table[1], deep[1]);
        }

        [Fact]
        public void Sorted_IsOrderedByName()
        {
            var names = new HashtableDemo().Sorted().Select(p => p.Name).ToList();
            Assert.Equal(names.OrderBy(n => n, System.StringComparer.Ordinal).ToList(), names);
        }

        [Fact]
        public void FindKeysByName_FindsExistingAndMissing()
        {
            var d = new HashtableDemo();
            Assert.Single(d.FindKeysByName("Сидоров"));
            Assert.Empty(d.FindKeysByName("Нет такого"));
        }
    }

    public class DictionaryDemoTests
    {
        [Fact]
        public void AddAndRemove_Work()
        {
            var d = new DictionaryDemo();
            int key = d.Add(new Student("Новый", 18, "МГУ", 1));
            Assert.Equal(6, d.Dict.Count);
            Assert.True(d.Remove(key));
            Assert.False(d.Remove(key));
        }

        [Fact]
        public void Queries_ReturnExpectedValues()
        {
            var d = new DictionaryDemo();
            Assert.Equal(3, d.CountStudents());
            Assert.Equal(2, d.OlderThan(30).Count);
            Assert.Equal(4, d.MaxCourse());
        }

        [Fact]
        public void DeepClone_IsIndependent()
        {
            var d = new DictionaryDemo();
            var clone = d.DeepClone();
            d.Dict[1].Name = "ИЗМЕНЕНО";
            Assert.Equal("Иванов", clone[1].Name);
        }

        [Fact]
        public void SortedByAge_IsAscending()
        {
            var ages = new DictionaryDemo().SortedByAge().Select(kv => kv.Value.Age).ToList();
            Assert.Equal(ages.OrderBy(a => a).ToList(), ages);
        }
    }

    public class TestCollectionsTests
    {
        [Fact]
        public void AllFourCollections_HaveSameCount()
        {
            var tc = new TestCollections(1000);
            Assert.Equal(1000, tc.Col1.Count);
            Assert.Equal(1000, tc.Col1Str.Count);
            Assert.Equal(1000, tc.Col2.Count);
            Assert.Equal(1000, tc.Col2Str.Count);
        }

        [Fact]
        public void Search_ByContent_FindsCopiesNotReferences()
        {
            var tc = new TestCollections(1000);
            var s = tc.Source[500];
            var copy = new Student(s.Name, s.Age, s.University, s.Course);
            Assert.NotSame(s, copy);
            Assert.Contains(copy, tc.Col1);
            Assert.True(tc.Col1Str.Contains(copy.ToString()));
            Assert.True(tc.Col2.ContainsKey(new Person(s.Name, s.Age)));
            Assert.True(tc.Col2Str.ContainsKey(copy.ToString()));
            Assert.True(tc.Col2.ContainsValue(copy));
        }

        [Fact]
        public void Search_MissingElement_ReturnsFalse()
        {
            var tc = new TestCollections(1000);
            var missing = new Student("НетТакого", 99, "Нигде", 9);
            Assert.DoesNotContain(missing, tc.Col1);
            Assert.False(tc.Col2.ContainsKey(missing.BasePerson));
            Assert.False(tc.Col2Str.ContainsKey(missing.ToString()));
            Assert.False(tc.Col2.ContainsValue(missing));
        }

        [Fact]
        public void AddThenRemove_RestoresSizes()
        {
            var tc = new TestCollections(1000);
            var s = TestCollections.Generate(5000);
            tc.Add(s);
            Assert.Equal(1001, tc.Col1.Count);
            Assert.True(tc.Remove(s));
            Assert.Equal(1000, tc.Col1.Count);
            Assert.Equal(1000, tc.Col1Str.Count);
            Assert.Equal(1000, tc.Col2.Count);
            Assert.Equal(1000, tc.Col2Str.Count);
        }

        [Fact]
        public void Remove_KeepsOrderOfStack()
        {
            var tc = new TestCollections(10);
            tc.Remove(tc.Source[5]);
            var expected = tc.Source.Where((_, i) => i != 5).Reverse().ToList();
            Assert.Equal(expected, tc.Col1.ToList());
        }
    }
}
