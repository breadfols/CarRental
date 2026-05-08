/// <summary>
/// Объект передачи данных для автомобиля, используемый при сохранении и загрузке из JSON
/// </summary>
public class CarDto
{
    /// <summary>Уникальный идентификатор автомобиля</summary>
    public int Id { get; set; }

    /// <summary>Марка автомобиля</summary>
    public string? Brand { get; set; }

    /// <summary>Модель автомобиля</summary>
    public string? Model { get; set; }

    /// <summary>Год выпуска</summary>
    public int Year { get; set; }

    /// <summary>Тип трансмиссии</summary>
    public string? Transmission { get; set; }

    /// <summary>Мощность двигателя в лошадиных силах</summary>
    public int Power { get; set; }

    /// <summary>Расход топлива (л/100 км)</summary>
    public double FuelConsumption { get; set; }

    /// <summary>Стоимость аренды за один день</summary>
    public decimal PricePerDay { get; set; }

    /// <summary>Текущий статус автомобиля</summary>
    public string? Status { get; set; }

    /// <summary>Тип автомобиля для восстановления конкретного класса при загрузке</summary>
    public string? Type { get; set; }
}