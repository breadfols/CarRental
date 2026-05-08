
namespace CarRental.Core
{
    /// <summary>
    /// Каталог автомобилей, обеспечивающий хранение и базовые операции над коллекцией
    /// </summary>
    public class CarCatalog
    {
        private List<Car> cars = new();

        /// <summary>
        /// Полностью замещает текущий список автомобилей переданным
        /// </summary>
        /// <param name="list">Новый список автомобилей</param>
        public void SetCars(List<Car> list)
        {
            cars = list;
        }

        /// <summary>
        /// Добавляет автомобиль в каталог
        /// </summary>
        /// <param name="car">Добавляемый автомобиль</param>
        public void AddCar(Car car)
        {
            cars.Add(car);
        }

        /// <summary>
        /// Скрывает автомобиль из доступных, не удаляя его из каталога
        /// </summary>
        /// <param name="id">Идентификатор автомобиля</param>
        public void HideCar(int id)
        {
            var car = cars.FirstOrDefault(c => c.Id == id);
            if (car != null)
                car.Status = "Hidden";
        }

        /// <summary>
        /// Ищет автомобиль по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор автомобиля</param>
        /// <returns>Найденный автомобиль или null</returns>
        public Car? FindCar(int id)
        {
            return cars.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Возвращает список автомобилей, не помеченных как скрытые
        /// </summary>
        public List<Car> GetAvailableCars()
        {
            return cars.Where(c => c.Status != "Hidden").ToList();
        }

        /// <summary>
        /// Возвращает все автомобили каталога
        /// </summary>
        public List<Car> GetAll()
        {
            return cars;
        }

        /// <summary>
        /// Перенумеровывает идентификаторы автомобилей подряд начиная с единицы
        /// </summary>
        public void ReorderIds()
        {
            for (int i = 0; i < cars.Count; i++)
            {
                cars[i].Id = i + 1;
            }
        }

        /// <summary>
        /// Удаляет автомобиль по идентификатору и перенумеровывает оставшиеся
        /// </summary>
        /// <param name="id">Идентификатор автомобиля</param>
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