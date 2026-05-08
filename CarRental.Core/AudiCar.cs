namespace CarRental.Core
{
    public class AudiCar : Car
    {
        public override string GetShortInfo()
        {
            return $"Audi {Model} ({Year}) - {PricePerDay}$/day";
        }

        public override string GetSpecifications()
        {
            return $"Audi {Model}, {Year}, {Transmission}, {Power}HP";
        }
    }
}