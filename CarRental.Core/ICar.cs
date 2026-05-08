namespace CarRental.Core
{
    /// <summary>
    /// Базовый интерфейс автомобиля, описывающий минимальный контракт получения характеристик
    /// </summary>
    public interface ICar
    {
        /// <summary>
        /// Возвращает строку с техническими характеристиками автомобиля
        /// </summary>
        string GetSpecifications();
    }
}