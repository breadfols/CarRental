namespace CarRental.Core
{
    /// <summary>
    /// Абстрактная фабрика автомобилей, реализующая паттерн «Фабричный метод»
    /// </summary>
    public abstract class CarFactory
    {
        /// <summary>
        /// Создаёт новый экземпляр автомобиля конкретной марки
        /// </summary>
        public abstract Car CreateCar();
    }

    /// <summary>
    /// Фабрика для создания автомобилей марки Audi
    /// </summary>
    public class AudiCarFactory : CarFactory
    {
        /// <summary>
        /// Создаёт новый экземпляр <see cref="AudiCar"/>
        /// </summary>
        public override Car CreateCar()
        {
            return new AudiCar();
        }
    }

    /// <summary>
    /// Фабрика для создания автомобилей марки BMW
    /// </summary>
    public class BMWCarFactory : CarFactory
    {
        /// <summary>
        /// Создаёт новый экземпляр <see cref="BMWCar"/>
        /// </summary>
        public override Car CreateCar()
        {
            return new BMWCar();
        }
    }
}