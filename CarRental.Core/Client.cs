namespace CarRental.Core
{
    /// <summary>
    /// Клиент сервиса аренды автомобилей
    /// </summary>
    public class Client
    {
        /// <summary>Идентификатор клиента</summary>
        public int Id { get; set; }

        /// <summary>Полное имя клиента</summary>
        public string? FullName { get; set; }

        /// <summary>Контактный номер телефона</summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Выбирает автомобиль из каталога по его идентификатору
        /// </summary>
        /// <param name="catalog">Каталог автомобилей</param>
        /// <param name="id">Идентификатор автомобиля</param>
        public Car? SelectCar(CarCatalog catalog, int id)
        {
            return catalog.FindCar(id);
        }

        /// <summary>
        /// Создаёт новую заявку на аренду автомобиля на указанное число дней
        /// </summary>
        /// <param name="car">Арендуемый автомобиль</param>
        /// <param name="days">Количество дней аренды</param>
        public RentalRequest CreateRequest(Car car, int days)
        {
            return new RentalRequest
            {
                Client = this,
                Car = car,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(days)
            };
        }

        /// <summary>
        /// Возвращает технические характеристики автомобиля
        /// </summary>
        /// <param name="car">Автомобиль, описание которого требуется получить</param>
        public string ViewSpecifications(ICar car)
        {
            return car.GetSpecifications();
        }

        /// <summary>
        /// Получает предполагаемую стоимость аренды автомобиля с учётом дополнительных опций
        /// </summary>
        /// <param name="car">Арендуемый автомобиль</param>
        /// <param name="days">Количество дней аренды</param>
        /// <param name="service">Сервис расчёта стоимости</param>
        /// <param name="insurance">Признак включения страховки</param>
        /// <param name="childSeat">Признак включения детского кресла</param>
        public decimal ViewEstimatedCost(Car car, int days, RentalCostService service, bool insurance, bool childSeat)
        {
            return service.CalculateEstimatedCost(car, days, insurance, childSeat);
        }
    }
}