using System.Collections.Generic;
using CarRental.Core;
using CarRental.Services;

namespace CarRental.Core
{
    public class Administrator
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }

        private DataStorage _dataStorage;

        public Administrator(DataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public List<RentalRequest> ViewRequests()
        {
            return _dataStorage.Requests;
        }
        public void AddCar(CarCatalog catalog, CarFactory factory)
        {
            var car = factory.CreateCar();
            catalog.AddCar(car);
        }

        public void UpdateCar(Car car)
        {
            car.Status = "Updated";
        }

        public void RemoveFromRental(CarCatalog catalog, int id)
        {
            catalog.HideCar(id);
        }

        public void RestrictCar(Car car, string reason)
        {
            car.Status = $"Restricted: {reason}";
        }
    }
}