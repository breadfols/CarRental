namespace CarRental.Core
{
    /// <summary>
    /// Абстрактный базовый класс автомобиля, содержащий общие свойства всех моделей
    /// </summary>
    public abstract class Car : ICar
    {
        /// <summary>Уникальный идентификатор автомобиля</summary>
        public int Id { get; set; }

        /// <summary>Марка автомобиля</summary>
        public string? Brand { get; set; }

        /// <summary>Модель автомобиля</summary>
        public string? Model { get; set; }

        /// <summary>Год выпуска</summary>
        public int Year { get; set; }

        /// <summary>Тип трансмиссии (автомат / механика)</summary>
        public string? Transmission { get; set; }

        /// <summary>Мощность двигателя в лошадиных силах</summary>
        public int Power { get; set; }

        /// <summary>Расход топлива (л/100 км)</summary>
        public double FuelConsumption { get; set; }

        /// <summary>Стоимость аренды за один день</summary>
        public decimal PricePerDay { get; set; }

        /// <summary>Текущий статус автомобиля (например, Available, Rented, Hidden)</summary>
        public string? Status { get; set; }

        /// <summary>Тип автомобиля, используемый при сериализации/десериализации</summary>
        public string? Type { get; set; }

        /// <summary>
        /// Возвращает краткое описание автомобиля для клиента
        /// </summary>
        public abstract string GetShortInfo();

        /// <summary>
        /// Возвращает подробные технические характеристики автомобиля
        /// </summary>
        public abstract string GetSpecifications();
    }
}