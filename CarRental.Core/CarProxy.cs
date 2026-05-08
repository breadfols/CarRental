namespace CarRental.Core
{
    public class CarProxy : ICar
    {
        private readonly Car _car;
        private readonly RentalCostService _service;

        public CarProxy(Car car, RentalCostService service)
        {
            _car = car;
            _service = service;
        }

        public string GetSpecifications()
        {
            return _car.GetSpecifications();
        }

        public decimal CalculateEstimatedCost(int days, bool insurance = false, bool childSeat = false)
        {
            return _service.CalculateEstimatedCost(_car, days, insurance, childSeat);
        }
    }
}