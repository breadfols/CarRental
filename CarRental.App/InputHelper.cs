using System;
using System.Linq;

namespace CarRental.App
{
    public class InvalidInputException : ArgumentException
    {
        public string? InputValue { get; }

        public InvalidInputException(string message, string? inputValue = null)
            : base(message)
        {
            InputValue = inputValue;
        }
    }

    public class EmptyInputException : InvalidInputException
    {
        public EmptyInputException(string message = "Поле не может быть пустым.")
            : base(message, "") { }
    }

    public class InvalidIntRangeException : InvalidInputException
    {
        public int Min { get; }
        public int Max { get; }

        public InvalidIntRangeException(int min, int max, string? input = null)
            : base($"Некорректный ввод.", input)
        {
            Min = min;
            Max = max;
        }
    }

    public class InvalidDecimalException : InvalidInputException
    {
        public InvalidDecimalException(string message = "Некорректный ввод цены.", string? input = null)
            : base(message, input) { }
    }

    public class InvalidPositiveIntException : InvalidInputException
    {
        public InvalidPositiveIntException(string message = "Введите положительное число.", string? input = null)
            : base(message, input) { }
    }

    public class InvalidClientNameException : InvalidInputException
    {
        public InvalidClientNameException(string message = "Имя должно содержать только буквы и пробелы.", string? input = null)
            : base(message, input) { }
    }

    internal static class InputHelper
    {
        public static bool CancelRequested { get; set; }

        public static string ReadRequiredString(string prompt)
        {
            while (true)
            {
                if (CancelRequested) return string.Empty;

                Console.Write(prompt);
                var value = Console.ReadLine();

                if (CancelRequested) return string.Empty;

                if (!string.IsNullOrWhiteSpace(value))
                    return value;

                Console.WriteLine("Поле не может быть пустым.");
            }
        }

        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                if (CancelRequested) return -1;

                Console.Write(prompt);
                var input = Console.ReadLine();

                if (CancelRequested) return -1;

                if (int.TryParse(input, out int value) && value >= min && value <= max)
                    return value;

                Console.WriteLine($"Некорректный ввод. Допустимо: {min}–{max}");
            }
        }

        public static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                if (CancelRequested) return -1;

                Console.Write(prompt);
                var input = Console.ReadLine();

                if (CancelRequested) return -1;

                if (decimal.TryParse(input, out decimal value) && value > 0)
                    return value;

                Console.WriteLine("Некорректный ввод цены.");
            }
        }

        public static int ReadPositiveInt(string prompt)
        {
            while (true)
            {
                if (CancelRequested) return -1;

                Console.Write(prompt);
                var input = Console.ReadLine();

                if (CancelRequested) return -1;

                if (int.TryParse(input, out int value) && value > 0)
                    return value;

                Console.WriteLine("Введите положительное число.");
            }
        }

        public static string ReadClientName(string prompt)
        {
            while (true)
            {
                if (CancelRequested) return string.Empty;

                Console.Write(prompt);
                var name = Console.ReadLine();

                if (CancelRequested) return string.Empty;

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Имя не может быть пустым.");
                    continue;
                }

                bool valid = name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));

                if (valid)
                    return name.Trim();

                Console.WriteLine("Имя должно содержать только буквы и пробелы.");
            }
        }
    }
}