namespace CarRental.Core
{
    public class Client
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }

        public Car? SelectCar(CarCatalog catalog, int id)
        {
            return catalog.FindCar(id);
        }

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

        public string ViewSpecifications(ICar car)
        {
            return car.GetSpecifications();
        }

        public decimal ViewEstimatedCost(Car car, int days, RentalCostService service, bool insurance, bool childSeat)
        {
            return service.CalculateEstimatedCost(car, days, insurance, childSeat);
        }
    }
}