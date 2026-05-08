using System.Text.Json;
using CarRental.Core;

namespace CarRental.Services
{
    public class DataStorageDto
    {
        public List<CarDto> Cars { get; set; } = new();
        public List<RentalRequest> Requests { get; set; } = new();
    }
    public class DataStorage
    {
        private const string FileName = "data.json";

        public List<Car> Cars { get; set; } = new();
        public List<RentalRequest> Requests { get; set; } = new();

        public void Save()
        {
            var dto = new DataStorageDto();

            foreach (var c in Cars)
            {
                dto.Cars.Add(new CarDto
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    Transmission = c.Transmission,
                    Power = c.Power,
                    FuelConsumption = c.FuelConsumption,
                    PricePerDay = c.PricePerDay,
                    Status = c.Status,
                    Type = c.Type
                });
            }
            foreach (var r in Requests)
            { 
                r.CarId = r.Car?.Id ?? -1;
            }

            dto.Requests = Requests;

            var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(FileName, json);
        }

        public static DataStorage Load()
        {
            if (!File.Exists(FileName))
                return new DataStorage();

            var json = File.ReadAllText(FileName);

            var dto = JsonSerializer.Deserialize<DataStorageDto>(json)
                      ?? new DataStorageDto();

            var data = new DataStorage();

            foreach (var c in dto.Cars)
            {
                Car car = c.Type switch
                {
                    "Audi" => new AudiCar(),
                    "BMW" => new BMWCar(),
                    _ => new AudiCar()
                };

                car.Id = c.Id;
                car.Brand = c.Brand;
                car.Model = c.Model;
                car.Year = c.Year;
                car.Transmission = c.Transmission;
                car.Power = c.Power;
                car.FuelConsumption = c.FuelConsumption;
                car.PricePerDay = c.PricePerDay;
                car.Status = c.Status;
                car.Type = c.Type;

                data.Cars.Add(car);
            }

            data.Requests = dto.Requests;
            foreach (var r in data.Requests)
            {
                r.Car = data.Cars.FirstOrDefault(c => c.Id == r.CarId);
            }
            return data;

        }
    }
}