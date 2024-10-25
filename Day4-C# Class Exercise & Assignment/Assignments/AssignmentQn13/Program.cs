namespace AssignmentQn13
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TransportManager manager = new TransportManager();

            // Adding some transport schedules data's
            manager.AddSchedule(new TransportSchedule("Bus", "Route A", new DateTime(2024, 10, 25, 8, 30, 0), new DateTime(2024, 10, 25, 10, 0, 0), 50.00m, 30));
            manager.AddSchedule(new TransportSchedule("Flight", "Route B", new DateTime(2024, 10, 25, 9, 30, 0), new DateTime(2024, 10, 25, 11, 30, 0), 200.00m, 10));
            manager.AddSchedule(new TransportSchedule("Bus", "Route A", new DateTime(2024, 10, 26, 2, 0, 0), new DateTime(2024, 10, 26, 10, 0, 0), 55.00m, 5));
            manager.AddSchedule(new TransportSchedule("Bus", "Route C", new DateTime(2024, 10, 27, 10, 0, 0), new DateTime(2024, 10, 27, 12, 0, 0), 45.00m, 20));
            manager.AddSchedule(new TransportSchedule("Flight", "Route D", new DateTime(2024, 10, 27, 14, 30, 0), new DateTime(2024, 10, 27, 16, 30, 0), 300.00m, 15));


            var busSchedules = manager.Search(transportType: "Bus");
            Console.WriteLine("----Found Bus Schedules:----");
            foreach (var schedule in busSchedules)
            {
                Console.WriteLine($"- Route: {schedule.Route}, Departure: {schedule.DepartureTime:dd-MM-yyyy hh:mm:ss tt}, Seats Available: {schedule.SeatsAvailable}");
            }

            var flightSchedules = manager.Search(transportType: "Flight");
            Console.WriteLine("\n----Found Flight Schedules:----");
            foreach (var schedule in flightSchedules)
            {
                Console.WriteLine($"- Route: {schedule.Route}, Departure: {schedule.DepartureTime:dd-MM-yyyy hh:mm:ss tt}, Seats Available: {schedule.SeatsAvailable}");
            }

            // Group by transport type
            var groupedSchedules = manager.GroupByTransportType();
            Console.WriteLine("\n----Transport Schedules:----");
            foreach (var group in groupedSchedules)
            {
                Console.WriteLine($"Transport Type: {group.Key}");
                foreach (var schedule in group.Value)
                {
                    Console.WriteLine($" - {schedule.Route} at {schedule.DepartureTime:dd-MM-yyyy hh:mm:ss tt}");
                }
            }

            // Total seats and average price
            var (totalSeats, averagePrice) = manager.AggregateData();
            Console.WriteLine($"\nTotal Seats Available: {totalSeats}, Average Price: {averagePrice:F2}");
        }
    }
}
