namespace CarRental.Core
{
    /// <summary>
    /// Сервис расчёта стоимости аренды автомобиля с учётом дополнительных опций
    /// </summary>
    public class RentalCostService
    {
        /// <summary>
        /// Рассчитывает предполагаемую стоимость аренды на основании цены за день и доп. услуг
        /// </summary>
        /// <param name="car">Арендуемый автомобиль</param>
        /// <param name="days">Количество дней аренды</param>
        /// <param name="insurance">Включена ли страховка</param>
        /// <param name="childSeat">Включено ли детское кресло</param>
        public decimal CalculateEstimatedCost(Car car, int days, bool insurance, bool childSeat)
        {
            decimal cost = car.PricePerDay * days;

            if (insurance)
                cost += 5m * days;

            if (childSeat)
                cost += 2.5m * days;

            return cost;
        }
    }
}