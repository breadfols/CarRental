namespace CarRental.Core
{
    public class RentalCostService
    {
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