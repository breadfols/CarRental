namespace CarRental.Core
{
    /// <summary>
    /// Конкретная реализация автомобиля марки BMW
    /// </summary>
    public class BMWCar : Car
    {
        /// <summary>
        /// Возвращает краткое описание автомобиля BMW для клиента
        /// </summary>
        public override string GetShortInfo()
        {
            return $"BMW {Model} ({Year}) - {PricePerDay}$/day";
        }

        /// <summary>
        /// Возвращает подробные технические характеристики автомобиля BMW
        /// </summary>
        public override string GetSpecifications()
        {
            return $"BMW {Model}, {Year}, {Transmission}, {Power}HP";
        }
    }
}