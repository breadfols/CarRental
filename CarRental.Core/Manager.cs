namespace CarRental.Core
{
    public class Manager
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }

        public string ViewSpecifications(ICar car)
        {
            return car.GetSpecifications();
        }
    }
}