using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _5_pz
{
    internal class _String
    {
        static readonly string[] tests =
        {
            "static void PrintUpper string info12346: WriteLine ToUpper info, 1234info.",
            "int _value = 10; string name = Alex.",
            "123abc 456 _x a1 abc12 longerIdentifier.",
            ""
        };

        static readonly HashSet<string> keywords = new HashSet<string>
        {
            "abstract", "as", "base", "bool", "break", "byte", "case", "catch",
            "char", "checked", "class", "const", "continue", "decimal", "default",
            "delegate", "do", "double", "else", "enum", "event", "explicit",
            "extern", "false", "finally", "fixed", "float", "for", "foreach",
            "goto", "if", "implicit", "in", "int", "interface", "internal",
            "is", "lock", "long", "namespace", "new", "null", "object",
            "operator", "out", "override", "params", "private", "protected",
            "public", "readonly", "ref", "return", "sbyte", "sealed", "short",
            "sizeof", "stackalloc", "static", "string", "struct", "switch",
            "this", "throw", "true", "try", "typeof", "uint", "ulong",
            "unchecked", "unsafe", "ushort", "using", "virtual", "void",
            "volatile", "while"
        };

        public void MenuString()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Работа со строками");
                    Console.WriteLine("1. Ввести строку с клавиатуры");
                    Console.WriteLine("2. Выбрать тестовую строку");
                    Console.WriteLine("3. Назад");
                    Console.Write("Ваш выбор: ");
                    string choice = Console.ReadLine();
                    string text;

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Введите строку: ");
                            text = Console.ReadLine() ?? "";
                            break;
                        case "2":
                            for (int i = 0; i < tests.Length; i++)
                                Console.WriteLine((i + 1) + ". " + (tests[i] == "" ? "(пустая строка)" : tests[i]));

                            Console.Write("Номер теста: ");
                            int num;
                            if (!int.TryParse(Console.ReadLine(), out num) || num < 1 || num > tests.Length)
                                throw new WrongInp("Такого теста нет.");
                            text = tests[num - 1];
                            break;
                        case "3":
                            return;
                        default:
                            throw new WrongInp("Выберите пункт 1, 2 или 3.");
                    }

                    FindId(text);
                    ConsoleHelper.Wait();
                }
                catch (WrongInp ex)
                {
                    Console.WriteLine(ex.Message);
                    ConsoleHelper.Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при работе со строками: " + ex.Message);
                    ConsoleHelper.Wait();
                }
            }
        }

        void FindId(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine("Строка пустая.");
                return;
            }

            Console.WriteLine("\nИсходная строка: " + text);
            MatchCollection found = Regex.Matches(
                text, @"(?<![A-Za-z0-9_])[A-Za-z_][A-Za-z0-9_]*(?![A-Za-z0-9_])");

            List<string> ids = new List<string>();
            foreach (Match item in found)
            {
                if (!keywords.Contains(item.Value))
                    ids.Add(item.Value);
            }

            if (ids.Count == 0)
            {
                Console.WriteLine("Идентификаторы не найдены.");
                return;
            }

            int min = ids[0].Length;
            for (int i = 1; i < ids.Count; i++)
                if (ids[i].Length < min)
                    min = ids[i].Length;

            Console.Write("Самый короткий идентификатор(ы): ");
            HashSet<string> printed = new HashSet<string>();
            bool first = true;
            foreach (string id in ids)
            {
                if (id.Length != min || !printed.Add(id))
                    continue;

                if (!first)
                    Console.Write(", ");
                Console.Write(id);
                first = false;
            }
            Console.WriteLine();
        }
    }
}
