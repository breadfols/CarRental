namespace CarRental.Core
{
    public abstract class CarFactory
    {
        public abstract Car CreateCar();
    }

    public class AudiCarFactory : CarFactory
    {
        public override Car CreateCar()
        {
            return new AudiCar();
        }
    }

    public class BMWCarFactory : CarFactory
    {
        public override Car CreateCar()
        {
            return new BMWCar();
        }
    }
}