namespace CarRental.Core
{
    public class BMWCar : Car
    {
        public override string GetShortInfo()
        {
            return $"BMW {Model} ({Year}) - {PricePerDay}$/day";
        }

        public override string GetSpecifications()
        {
            return $"BMW {Model}, {Year}, {Transmission}, {Power}HP";
        }
    }
}