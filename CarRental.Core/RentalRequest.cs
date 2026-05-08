using System.Text.Json.Serialization;

namespace CarRental.Core
{
    /// <summary>
    /// Заявка на аренду автомобиля, содержащая клиента, автомобиль, период и расчётную стоимость
    /// </summary>
    public class RentalRequest
    {
        /// <summary>Идентификатор заявки</summary>
        public int Id { get; set; }

        /// <summary>Клиент, подавший заявку</summary>
        public Client? Client { get; set; }

        /// <summary>Идентификатор арендуемого автомобиля, используется при сериализации</summary>
        public int CarId { get; set; }

        /// <summary>Дата и время начала аренды</summary>
        public DateTime StartDate { get; set; }

        /// <summary>Дата и время окончания аренды</summary>
        public DateTime EndDate { get; set; }

        /// <summary>Итоговая стоимость аренды</summary>
        public decimal TotalCost { get; set; }

        /// <summary>Признак включения страховки в аренду</summary>
        public bool Insurance { get; set; }

        /// <summary>Признак включения детского кресла в аренду</summary>
        public bool ChildSeat { get; set; }

        /// <summary>
        /// Связанный объект автомобиля, не сохраняемый напрямую в JSON
        /// </summary>
        [JsonIgnore]
        public Car? Car { get; set; }

        /// <summary>
        /// Возвращает количество дней аренды по разнице дат начала и окончания
        /// </summary>
        public int GetNumberOfDays()
        {
            return (EndDate - StartDate).Days;
        }

        /// <summary>
        /// Рассчитывает и сохраняет итоговую стоимость аренды с учётом доп. опций
        /// </summary>
        /// <param name="service">Сервис расчёта стоимости аренды</param>
        public decimal CalculateCost(RentalCostService service)
        {
            if (Car == null) return 0;

            TotalCost = service.CalculateEstimatedCost(
                Car,
                GetNumberOfDays(),
                Insurance,
                ChildSeat
            );

            return TotalCost;
        }
    }
}