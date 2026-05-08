using System;
using System.Linq;

namespace CarRental.App
{
    /// <summary>
    /// Базовое исключение для ошибок ввода пользователя
    /// </summary>
    public class InvalidInputException : ArgumentException
    {
        /// <summary>Введённое значение, вызвавшее ошибку</summary>
        public string? InputValue { get; }

        /// <summary>
        /// Создаёт исключение с указанным сообщением и опциональным значением ввода
        /// </summary>
        /// <param name="message">Описание ошибки</param>
        /// <param name="inputValue">Введённое значение</param>
        public InvalidInputException(string message, string? inputValue = null)
            : base(message)
        {
            InputValue = inputValue;
        }
    }

    /// <summary>
    /// Исключение, выбрасываемое при пустом значении обязательного поля
    /// </summary>
    public class EmptyInputException : InvalidInputException
    {
        /// <summary>
        /// Создаёт исключение пустого ввода с сообщением по умолчанию
        /// </summary>
        /// <param name="message">Описание ошибки</param>
        public EmptyInputException(string message = "Поле не может быть пустым.")
            : base(message, "") { }
    }

    /// <summary>
    /// Исключение, выбрасываемое при выходе целочисленного значения за допустимый диапазон
    /// </summary>
    public class InvalidIntRangeException : InvalidInputException
    {
        /// <summary>Минимально допустимое значение</summary>
        public int Min { get; }

        /// <summary>Максимально допустимое значение</summary>
        public int Max { get; }

        /// <summary>
        /// Создаёт исключение с указанием границ диапазона
        /// </summary>
        /// <param name="min">Минимально допустимое значение</param>
        /// <param name="max">Максимально допустимое значение</param>
        /// <param name="input">Введённое значение</param>
        public InvalidIntRangeException(int min, int max, string? input = null)
            : base($"Некорректный ввод.", input)
        {
            Min = min;
            Max = max;
        }
    }

    /// <summary>
    /// Исключение, выбрасываемое при некорректном вводе цены
    /// </summary>
    public class InvalidDecimalException : InvalidInputException
    {
        /// <summary>
        /// Создаёт исключение некорректного ввода десятичного числа
        /// </summary>
        /// <param name="message">Описание ошибки</param>
        /// <param name="input">Введённое значение</param>
        public InvalidDecimalException(string message = "Некорректный ввод цены.", string? input = null)
            : base(message, input) { }
    }

    /// <summary>
    /// Исключение, выбрасываемое при вводе неположительного целого числа
    /// </summary>
    public class InvalidPositiveIntException : InvalidInputException
    {
        /// <summary>
        /// Создаёт исключение некорректного положительного числа
        /// </summary>
        /// <param name="message">Описание ошибки</param>
        /// <param name="input">Введённое значение</param>
        public InvalidPositiveIntException(string message = "Введите положительное число.", string? input = null)
            : base(message, input) { }
    }

    /// <summary>
    /// Исключение, выбрасываемое при некорректном вводе имени клиента
    /// </summary>
    public class InvalidClientNameException : InvalidInputException
    {
        /// <summary>
        /// Создаёт исключение некорректного имени клиента
        /// </summary>
        /// <param name="message">Описание ошибки</param>
        /// <param name="input">Введённое значение</param>
        public InvalidClientNameException(string message = "Имя должно содержать только буквы и пробелы.", string? input = null)
            : base(message, input) { }
    }

    /// <summary>
    /// Вспомогательный класс для безопасного чтения пользовательского ввода с консоли
    /// </summary>
    internal static class InputHelper
    {
        /// <summary>
        /// Флаг отмены ввода, выставляемый при перехвате Ctrl+C
        /// </summary>
        public static bool CancelRequested { get; set; }

        /// <summary>
        /// Запрашивает у пользователя непустую строку, повторяя запрос до получения корректного ввода
        /// </summary>
        /// <param name="prompt">Текст приглашения</param>
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

        /// <summary>
        /// Читает целое число в заданном диапазоне, повторяя запрос при некорректном вводе
        /// </summary>
        /// <param name="prompt">Текст приглашения</param>
        /// <param name="min">Минимально допустимое значение</param>
        /// <param name="max">Максимально допустимое значение</param>
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

        /// <summary>
        /// Читает положительное десятичное число, повторяя запрос при некорректном вводе
        /// </summary>
        /// <param name="prompt">Текст приглашения</param>
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

        /// <summary>
        /// Читает положительное целое число, повторяя запрос при некорректном вводе
        /// </summary>
        /// <param name="prompt">Текст приглашения</param>
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

        /// <summary>
        /// Читает имя клиента, допуская только буквы и пробелы
        /// </summary>
        /// <param name="prompt">Текст приглашения</param>
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