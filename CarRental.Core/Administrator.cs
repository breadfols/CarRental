using System.Collections.Generic;
using CarRental.Core;
using CarRental.Services;

namespace CarRental.Core
{
    /// <summary>
    /// Администратор сервиса, управляющий каталогом автомобилей и просмотром заявок
    /// </summary>
    public class Administrator
    {
        /// <summary>Идентификатор администратора</summary>
        public int Id { get; set; }

        /// <summary>Полное имя администратора</summary>
        public string? FullName { get; set; }

        /// <summary>Контактный номер телефона администратора</summary>
        public string? Phone { get; set; }

        private DataStorage _dataStorage;

        /// <summary>
        /// Создаёт администратора, использующего указанное хранилище данных
        /// </summary>
        /// <param name="dataStorage">Хранилище данных приложения</param>
        public Administrator(DataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        /// <summary>
        /// Возвращает список всех заявок на аренду из хранилища
        /// </summary>
        public List<RentalRequest> ViewRequests()
        {
            return _dataStorage.Requests;
        }

        /// <summary>
        /// Добавляет новый автомобиль в каталог с использованием переданной фабрики
        /// </summary>
        /// <param name="catalog">Каталог автомобилей</param>
        /// <param name="factory">Фабрика конкретной марки автомобиля</param>
        public void AddCar(CarCatalog catalog, CarFactory factory)
        {
            var car = factory.CreateCar();
            catalog.AddCar(car);
        }

        /// <summary>
        /// Помечает автомобиль как обновлённый
        /// </summary>
        /// <param name="car">Изменяемый автомобиль</param>
        public void UpdateCar(Car car)
        {
            car.Status = "Updated";
        }

        /// <summary>
        /// Снимает автомобиль с аренды, скрывая его из каталога
        /// </summary>
        /// <param name="catalog">Каталог автомобилей</param>
        /// <param name="id">Идентификатор автомобиля</param>
        public void RemoveFromRental(CarCatalog catalog, int id)
        {
            catalog.HideCar(id);
        }

        /// <summary>
        /// Накладывает ограничение на использование автомобиля с указанием причины
        /// </summary>
        /// <param name="car">Автомобиль, на который накладывается ограничение</param>
        /// <param name="reason">Причина ограничения</param>
        public void RestrictCar(Car car, string reason)
        {
            car.Status = $"Restricted: {reason}";
        }
    }
}