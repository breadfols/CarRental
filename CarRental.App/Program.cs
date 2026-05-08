using CarRental.Core;
using CarRental.Services;
using System.Linq;

namespace CarRental.App
{
    /// <summary>
    /// Точка входа приложения и основной консольный интерфейс работы с системой аренды
    /// </summary>
    internal class Program
    {
        private static RentalCostService costService = new RentalCostService();

        /// <summary>
        /// Главный метод приложения, реализующий цикл меню и обработку команд пользователя
        /// </summary>
        static void Main()
        {
            var storage = DataStorage.Load();
            var catalog = new CarCatalog();
            catalog.SetCars(storage.Cars);

            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                InputHelper.CancelRequested = true;
                Console.WriteLine("\nВозврат в меню...");
            };

            while (true)
            {
                InputHelper.CancelRequested = false;

                Console.WriteLine("\n1. Показать машины");
                Console.WriteLine("2. Добавить машину");
                Console.WriteLine("3. Арендовать машину");
                Console.WriteLine("4. Показать заявки");
                Console.WriteLine("5. Удалить заявку");
                Console.WriteLine("0. Выход");

                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ShowCars(catalog);
                            break;

                        case "2":
                            AddCar(catalog);
                            break;

                        case "3":
                            RentCar(catalog, storage);
                            break;

                        case "4":
                            ShowRequests(storage);
                            break;

                        case "5":
                            DeleteRequest(storage);
                            break;

                        case "0":
                            storage.Save();
                            Console.WriteLine("Данные сохранены.");
                            return;

                        default:
                            Console.WriteLine("Неверный выбор");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.WriteLine("Попробуйте снова.\n");
                }
            }
        }

        /// <summary>
        /// Отображает список автомобилей в кратком или полном виде в зависимости от выбранной роли
        /// </summary>
        /// <param name="catalog">Каталог автомобилей</param>
        static void ShowCars(CarCatalog catalog)
        {
            var cars = catalog.GetAll();

            if (cars.Count == 0)
            {
                Console.WriteLine("Нет доступных машин.");
                return;
            }

            Console.WriteLine("Кто просматривает?");
            Console.WriteLine("1. Клиент (кратко)");
            Console.WriteLine("2. Менеджер (полная информация)");

            string role;
            while (true)
            {
                role = Console.ReadLine();

                if (role == "1" || role == "2")
                    break;

                Console.WriteLine("Введите 1 или 2.");
            }

            bool isManager = role == "2";

            foreach (var car in cars)
            {
                if (isManager)
                {
                    Console.WriteLine(
                        $"ID: {car.Id} | " +
                        $"Brand: {car.Brand} | " +
                        $"Model: {car.Model} | " +
                        $"Year: {car.Year} | " +
                        $"Transmission: {car.Transmission} | " +
                        $"Power: {car.Power} HP | " +
                        $"Fuel: {car.FuelConsumption} L/100km | " +
                        $"Price: {car.PricePerDay}$ / day | " +
                        $"Status: {car.Status}"
                    );
                }
                else
                {
                    Console.WriteLine($"ID: {car.Id} | {car.GetShortInfo()}");
                }
            }
        }

        /// <summary>
        /// Запрашивает у пользователя характеристики и добавляет новый автомобиль в каталог
        /// </summary>
        /// <param name="catalog">Каталог автомобилей</param>
        static void AddCar(CarCatalog catalog)
        {
            Console.WriteLine("1. Audi\n2. BMW");

            string type;
            while (true)
            {
                type = InputHelper.ReadRequiredString("Выберите тип: ");
                if (InputHelper.CancelRequested) return;

                if (type == "1" || type == "2")
                    break;

                Console.WriteLine("Нужно выбрать 1 или 2.");
            }

            string model = InputHelper.ReadRequiredString("Model: ");
            if (InputHelper.CancelRequested) return;

            int year = InputHelper.ReadInt("Year: ", 1900, 2026);
            if (InputHelper.CancelRequested) return;

            decimal price = InputHelper.ReadDecimal("Price per day: ");
            if (InputHelper.CancelRequested) return;

            string transmission = InputHelper.ReadRequiredString("Transmission (Auto/Manual): ");
            if (InputHelper.CancelRequested) return;

            int power = InputHelper.ReadPositiveInt("Power (HP): ");
            if (InputHelper.CancelRequested) return;

            double fuel;
            while (true)
            {
                var input = InputHelper.ReadRequiredString("Fuel consumption: ");
                if (InputHelper.CancelRequested) return;

                if (double.TryParse(input, out fuel) && fuel > 0)
                    break;

                Console.WriteLine("Введите корректное число.");
            }

            Car car = type == "1" ? new AudiCar() : new BMWCar();

            car.Id = catalog.GetAll().Any() ? catalog.GetAll().Max(c => c.Id) + 1 : 1;
            car.Brand = type == "1" ? "Audi" : "BMW";
            car.Model = model;
            car.Year = year;
            car.PricePerDay = price;
            car.Transmission = transmission;
            car.Power = power;
            car.FuelConsumption = fuel;
            car.Status = "Available";
            car.Type = type == "1" ? "Audi" : "BMW";

            catalog.AddCar(car);

            Console.WriteLine("Машина добавлена.");
        }

        /// <summary>
        /// Создаёт заявку на аренду автомобиля по введённым данным клиента и опциям
        /// </summary>
        /// <param name="catalog">Каталог автомобилей</param>
        /// <param name="storage">Хранилище данных приложения</param>
        static void RentCar(CarCatalog catalog, DataStorage storage)
        {
            int id = InputHelper.ReadPositiveInt("Введите ID машины: ");
            if (InputHelper.CancelRequested) return;

            var car = catalog.FindCar(id);

            if (car == null)
            {
                Console.WriteLine("Машина не найдена.");
                return;
            }

            if (car.Status == "Rented")
            {
                Console.WriteLine("Машина уже занята.");
                return;
            }

            string name = InputHelper.ReadClientName("Имя клиента: ");
            if (InputHelper.CancelRequested) return;

            int days = InputHelper.ReadPositiveInt("Дней аренды: ");
            if (InputHelper.CancelRequested) return;

            Console.WriteLine("Нужна страховка? (y/n)");
            bool insurance = Console.ReadLine()?.ToLower() == "y";

            Console.WriteLine("Нужно детское кресло? (y/n)");
            bool childSeat = Console.ReadLine()?.ToLower() == "y";

            var client = new Client { FullName = name };

            var request = new RentalRequest
            {
                Id = storage.Requests.Count + 1,
                Client = client,
                Car = car,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(days),

                Insurance = insurance,
                ChildSeat = childSeat
            };

            request.CalculateCost(costService);

            storage.Requests.Add(request);
            car.Status = "Rented";

            Console.WriteLine($"Стоимость аренды: {request.TotalCost}$");
        }

        /// <summary>
        /// Выводит на экран список всех существующих заявок на аренду
        /// </summary>
        /// <param name="storage">Хранилище данных приложения</param>
        static void ShowRequests(DataStorage storage)
        {
            if (storage.Requests.Count == 0)
            {
                Console.WriteLine("Заявок нет.");
                return;
            }

            foreach (var r in storage.Requests)
            {
                Console.WriteLine(
                    $"#{r.Id} | Клиент: {r.Client?.FullName ?? "неизвестно"} | Машина ID: {r.Car?.Id ?? 0} | Стоимость: {r.TotalCost}$"
                );
            }
        }

        /// <summary>
        /// Удаляет заявку по идентификатору, освобождает соответствующий автомобиль и перенумеровывает оставшиеся заявки
        /// </summary>
        /// <param name="storage">Хранилище данных приложения</param>
        static void DeleteRequest(DataStorage storage)
        {
            int id = InputHelper.ReadPositiveInt("Введите ID заявки: ");
            if (InputHelper.CancelRequested) return;

            var request = storage.Requests.FirstOrDefault(r => r.Id == id);

            if (request == null)
            {
                Console.WriteLine("Заявка не найдена.");
                return;
            }
            if (request.Car != null)
                request.Car.Status = "Available";

            storage.Requests.Remove(request);

            for (int i = 0; i < storage.Requests.Count; i++)
            {
                storage.Requests[i].Id = i + 1;
            }

            Console.WriteLine("Заявка удалена.");
        }
    }
}