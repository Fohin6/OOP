using System;

namespace _5_pz
{
    class WrongInp : Exception
    {
        public WrongInp() : base("Неправильный ввод.") { }
        public WrongInp(string message) : base(message) { }
    }

    internal static class ConsoleHelper
    {
        public static void Wait()
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }

    internal class Program
    {
        static void Main()
        {
            Array array = new Array();
            _String strings = new _String();

            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("  __   ____  ____   ___  ____  ____  ____ \r" +
                        "\n / _\\ (  _ \\(  _ \\ ( _ \\/ ___)(_  _)(  _ \\\r" +
                        "\n/    \\ )   / )   // _  /\\___ \\  )(   )   /\r" +
                        "\n\\_/\\_/(__\\_)(__\\_)\\__\\_)(____/ (__) (__\\_)");
                    Console.WriteLine("1. Работа с массивами");
                    Console.WriteLine("2. Работа со строками");
                    Console.WriteLine("3. Выход");
                    Console.Write("Ваш выбор: ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            array.MenuArray();
                            break;
                        case "2":
                            strings.MenuString();
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
                    Console.WriteLine("Непредвиденная ошибка: " + ex.Message);
                    ConsoleHelper.Wait();
                }
            }
        }
    }
}
