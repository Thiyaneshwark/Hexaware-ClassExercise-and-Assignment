namespace AssignmentQn13
{
    public class TransportSchedule
    {
        public string TransportType { get; set; } 
        public string Route { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public int SeatsAvailable { get; set; }

        public TransportSchedule(string transportType, string route, DateTime departureTime, DateTime arrivalTime, decimal price, int seatsAvailable)
        {
            TransportType = transportType;
            Route = route;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            Price = price;
            SeatsAvailable = seatsAvailable;
        }
    }
}
