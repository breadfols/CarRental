namespace CarRental.Core
{
    /// <summary>
    /// Менеджер компании, имеющий доступ к подробной информации об автомобилях
    /// </summary>
    public class Manager
    {
        /// <summary>Идентификатор менеджера</summary>
        public int Id { get; set; }

        /// <summary>Полное имя менеджера</summary>
        public string? FullName { get; set; }

        /// <summary>Контактный номер телефона менеджера</summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Возвращает технические характеристики автомобиля
        /// </summary>
        /// <param name="car">Автомобиль, описание которого требуется получить</param>
        public string ViewSpecifications(ICar car)
        {
            return car.GetSpecifications();
        }
    }
}