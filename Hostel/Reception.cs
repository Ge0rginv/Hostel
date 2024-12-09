using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel
{
    // класс самого ресепшена в котором создается сама гостиница
    public class Reception
    {
        public int[] CountRooms { get; }
        public double[] CostRooms { get; }
        public Hotel hostel { get; }
        public Reception(int[] Counts, double[] Costs)
        {
            CountRooms = Counts;
            CostRooms = Costs;
            hostel = new Hotel(Counts, Costs);
        }
    }
}




/*

    //public class Modeling
    //{
    //    public int time { get; set; } // переменная времени в часах
    //    public int M { get; set; } // количество дней моделирования
    //    public int time_step { get; set; } // промежуток появления заявок
    //    public int[] CountRooms { get; } // количество номеров каждого типа
    //    public double[] CostRooms { get; } // стоимость каждого типа номеров


    //}
    //public class RequestGenerator
    //{
    //    private Random _random = new Random();

    //    public void GenerateRequests(Hotel hotel, int daysToSimulate)
    //    {
    //        DateTime currentTime = DateTime.Now;
    //        for (int i = 0; i < daysToSimulate; i++)
    //        {
    //            int hoursUntilNextRequest = _random.Next(1, 5); // Интервал между заявками
    //            currentTime = currentTime.AddHours(hoursUntilNextRequest);

    //            RoomType randomRoomType = (RoomType)_random.Next(0, 5);
    //            DateTime checkInDate = currentTime.AddDays(_random.Next(1, 3));
    //            DateTime checkOutDate = checkInDate.AddDays(_random.Next(1, 10));

    //            // Обработка брони или заселения в зависимости от типа заявки
    //        }
    //    }
    //}
*/
