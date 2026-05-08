namespace CarRental.Core
{
    /// <summary>
    /// Конкретная реализация автомобиля марки Audi
    /// </summary>
    public class AudiCar : Car
    {
        /// <summary>
        /// Возвращает краткое описание автомобиля Audi для клиента
        /// </summary>
        public override string GetShortInfo()
        {
            return $"Audi {Model} ({Year}) - {PricePerDay}$/day";
        }

        /// <summary>
        /// Возвращает подробные технические характеристики автомобиля Audi
        /// </summary>
        public override string GetSpecifications()
        {
            return $"Audi {Model}, {Year}, {Transmission}, {Power}HP";
        }
    }
}