
namespace CarRental.Core
{
    public class CarCatalog
    {
        private List<Car> cars = new();

        public void SetCars(List<Car> list)
        {
            cars = list;
        }

        public void AddCar(Car car)
        {
            cars.Add(car);
        }

        public void HideCar(int id)
        {
            var car = cars.FirstOrDefault(c => c.Id == id);
            if (car != null)
                car.Status = "Hidden";
        }

        public Car? FindCar(int id)
        {
            return cars.FirstOrDefault(c => c.Id == id);
        }

        public List<Car> GetAvailableCars()
        {
            return cars.Where(c => c.Status != "Hidden").ToList();
        }

        public List<Car> GetAll()
        {
            return cars;
        }
        public void ReorderIds()
        {
            for (int i = 0; i < cars.Count; i++)
            {
                cars[i].Id = i + 1;
            }
        }
        public void RemoveCar(int id)
        {
            var car = cars.FirstOrDefault(c => c.Id == id);
            if (car != null)
            {
                cars.Remove(car);
                ReorderIds();
            }
        }
    }
}