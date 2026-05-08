using System.Text.Json.Serialization;

namespace CarRental.Core
{
    public class RentalRequest
    {
        public int Id { get; set; }
        public Client? Client { get; set; }
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }

        public bool Insurance { get; set; }
        public bool ChildSeat { get; set; }

        [JsonIgnore]
        public Car? Car { get; set; }

        public int GetNumberOfDays()
        {
            return (EndDate - StartDate).Days;
        }

        public decimal CalculateCost(RentalCostService service)
        {
            if (Car == null) return 0;

            TotalCost = service.CalculateEstimatedCost(
                Car,
                GetNumberOfDays(),
                Insurance,
                ChildSeat
            );

            return TotalCost;
        }
    }
}