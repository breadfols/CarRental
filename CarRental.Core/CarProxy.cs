namespace CarRental.Core
{
    /// <summary>
    /// Прокси для автомобиля, добавляющий возможность расчёта предполагаемой стоимости аренды
    /// </summary>
    public class CarProxy : ICar
    {
        private readonly Car _car;
        private readonly RentalCostService _service;

        /// <summary>
        /// Создаёт прокси для указанного автомобиля и сервиса расчёта стоимости
        /// </summary>
        /// <param name="car">Оборачиваемый автомобиль</param>
        /// <param name="service">Сервис расчёта стоимости аренды</param>
        public CarProxy(Car car, RentalCostService service)
        {
            _car = car;
            _service = service;
        }

        /// <summary>
        /// Делегирует получение технических характеристик исходному автомобилю
        /// </summary>
        public string GetSpecifications()
        {
            return _car.GetSpecifications();
        }

        /// <summary>
        /// Рассчитывает предполагаемую стоимость аренды с учётом дополнительных опций
        /// </summary>
        /// <param name="days">Количество дней аренды</param>
        /// <param name="insurance">Признак включения страховки</param>
        /// <param name="childSeat">Признак включения детского кресла</param>
        public decimal CalculateEstimatedCost(int days, bool insurance = false, bool childSeat = false)
        {
            return _service.CalculateEstimatedCost(_car, days, insurance, childSeat);
        }
    }
}