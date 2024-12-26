using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel
{
    internal class Reception
    {
        public int[] CountRooms { get; }
        public double[] CostRooms { get; }
        public Hotel hostel { get; }
        Reception(int[] Counts, double[] Costs) 
        {
            CountRooms = Counts;
            CostRooms = Costs;
            hostel = new Hotel(Counts, Costs);
        }
        
    }

    public class RequestGenerator
    {
        private Random _random = new Random();

        public void GenerateRequests(Hotel hotel, int daysToSimulate)
        {
            DateTime currentTime = DateTime.Now;
            for (int i = 0; i < daysToSimulate; i++)
            {
                int hoursUntilNextRequest = _random.Next(1, 5); // Интервал между заявками
                currentTime = currentTime.AddHours(hoursUntilNextRequest);

                RoomType randomRoomType = (RoomType)_random.Next(0, 5);
                DateTime checkInDate = currentTime.AddDays(_random.Next(1, 3));
                DateTime checkOutDate = checkInDate.AddDays(_random.Next(1, 10));

                // Обработка брони или заселения в зависимости от типа заявки
            }
        }
    }
}
