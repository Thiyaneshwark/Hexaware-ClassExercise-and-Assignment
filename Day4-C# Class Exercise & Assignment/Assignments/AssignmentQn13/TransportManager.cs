namespace AssignmentQn13
{
    public class TransportManager
    {
        private IList<TransportSchedule> schedules;

        public TransportManager()
        {
            schedules = new List<TransportSchedule>();
        }

        public void AddSchedule(TransportSchedule schedule)
        {
            schedules.Add(schedule);
        }

        // Search: Find schedules by transport type, route, or time.
        public IList<TransportSchedule> Search(string transportType = null, string route = null, DateTime? departureTime = null)
        {
            return schedules
                .Where(s =>
                    (string.IsNullOrEmpty(transportType) || s.TransportType.Equals(transportType, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrEmpty(route) || s.Route.Equals(route, StringComparison.OrdinalIgnoreCase)) &&
                    (!departureTime.HasValue || s.DepartureTime.Date == departureTime.Value.Date))
                .ToList(); // Convert to IList
        }

        // Group By: Group schedules by transport type.
        public IDictionary<string, IList<TransportSchedule>> GroupByTransportType()
        {
            return schedules
                .GroupBy(s => s.TransportType)
                .ToDictionary(g => g.Key, g => (IList<TransportSchedule>)g.ToList()); 
        }

        // Order By: Order schedules by departure time, price, or seats available.
        public IList<TransportSchedule> OrderByDepartureTime()
        {
            return schedules.OrderBy(s => s.DepartureTime).ToList();
        }

        public IList<TransportSchedule> OrderByPrice()
        {
            return schedules.OrderBy(s => s.Price).ToList();
        }

        public IList<TransportSchedule> OrderBySeatsAvailable()
        {
            return schedules.OrderBy(s => s.SeatsAvailable).ToList();
        }

        // Filter: Filter schedules based on availability of seats or routes within a time range.
        public IList<TransportSchedule> FilterBySeatsAvailable(int minSeats)
        {
            return schedules.Where(s => s.SeatsAvailable >= minSeats).ToList();
        }

        public IList<TransportSchedule> FilterByRouteAndTime(string route, DateTime startTime, DateTime endTime)
        {
            return schedules.Where(s => s.Route.Equals(route, StringComparison.OrdinalIgnoreCase) &&
                                        s.DepartureTime >= startTime &&
                                        s.DepartureTime <= endTime).ToList();
        }

        // Aggregate: Calculate total available seats and average price of transport.
        public (int totalSeats, decimal averagePrice) AggregateData()
        {
            int totalSeats = schedules.Sum(s => s.SeatsAvailable);
            decimal averagePrice = schedules.Count > 0 ? schedules.Average(s => s.Price) : 0;
            return (totalSeats, averagePrice);
        }

        // Select: Project a list of routes and their departure times.
        public IList<(string Route, DateTime DepartureTime)> SelectRoutesAndDepartureTimes()
        {
            return schedules.Select(s => (s.Route, s.DepartureTime)).ToList(); 
        }
    }
}
