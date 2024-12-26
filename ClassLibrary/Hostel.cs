using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    // Hotel class
    public class Hotel
    {
        public Dictionary<RoomType, int> CountRooms {  get; set; }
        public Dictionary<RoomType, double> CostRooms { get; set; }
        public List <BookingItem> bookingItems {  get; set; }
        public Dictionary<RoomType, List<Room>> rooms { get; set; }
        public Hotel(Dictionary<RoomType, int> TheNumberOfRoomsOfDifferentTypes, Dictionary<RoomType, double> TheCostOfRoomsOfDifferentTypes)
        {
            bookingItems = new List<BookingItem>();
            rooms = new Dictionary<RoomType, List<Room>>();
            CountRooms = TheNumberOfRoomsOfDifferentTypes;
            CostRooms = TheCostOfRoomsOfDifferentTypes;
            foreach (RoomType room in Enum.GetValues(typeof(RoomType))) // перебор всех типов комнат
            {
                rooms.Add(room, new List<Room>());
                int number = 1;
                for (int i = 0; i < CountRooms[room]; ++i) 
                {
                    switch (room)
                    {
                        case RoomType.Single:
                            {
                                rooms[room].Add(new SingleRoom(number, CostRooms[room], new SingleRoomCostCounter())); break;
                            }
                        case RoomType.Double:
                            {
                                rooms[room].Add(new DoubleRoom(number, CostRooms[room], new DoubleRoomCostCounter())); break;
                            }
                        case RoomType.Suite:
                            {
                                rooms[room].Add(new SuiteRoom(number, CostRooms[room], new SuiteRoomCostCounter())); break;
                            }
                        case RoomType.HalfSuite:
                            {
                                rooms[room].Add(new HalfSuiteRoom(number, CostRooms[room], new HalfSuiteRoomCostCounter())); break;
                            }
                        case RoomType.DoubleWithSofa:
                            {
                                rooms[room].Add(new DoubleWithSofaRoom(number, CostRooms[room], new DoubleWithSofaRoomCostCounter())); break;
                            }
                    }
                    ++number;
                }
            }

        }

        // Бронирование номера
        public void BookRoom(RoomType type, int checkInDate, int checkOutDate)
        {
            
            //bookingItems = new List<BookingItem>();
            switch (type)
            {
                case RoomType.Single:
                    {
                        bookingItems.Add(new BookingItem(new SingleRoom(), checkInDate, checkOutDate)); break;
                    }
                case RoomType.Double:
                    {
                        bookingItems.Add(new BookingItem(new DoubleRoom(), checkInDate, checkOutDate)); break;
                    }
                case RoomType.Suite:
                    {
                        bookingItems.Add(new BookingItem(new SuiteRoom(), checkInDate, checkOutDate)); break;
                    }
                case RoomType.HalfSuite:
                    {
                        bookingItems.Add(new BookingItem(new HalfSuiteRoom(), checkInDate, checkOutDate)); break;
                    }
                case RoomType.DoubleWithSofa:
                    {
                        bookingItems.Add(new BookingItem(new DoubleWithSofaRoom(), checkInDate, checkOutDate)); break;
                    }
                throw new ArgumentException("Given room Type was not found or implemented!");
            }
        }

        // Заселение в номер
        public bool CheckIn(BookingItem item)
        {
            bool result = FindAvailableRoom(item);
            //if(result)
            //{
            //    bookingItems[indexBook]
            //}
            return result;
        }


        //Поиск доступных номеров по типу
        // метод возвращает true, или false если подходящих номеров не нашлось
        public bool FindAvailableRoom(BookingItem item)
        {
            // словарь который определяет важность каждой комнаты для последующего предложения улучшения условий
            Dictionary<RoomType,int> important = new Dictionary<RoomType, int>()
                {
         { RoomType.Single, 0 },
         { RoomType.HalfSuite, 1 },
         { RoomType.Suite, 2 },
         { RoomType.Double, 3 },
         { RoomType.DoubleWithSofa, 4 }
     };
            Dictionary<int, RoomType> indexRoom = new Dictionary<int, RoomType>()
                {
         { 0, RoomType.Single },
         { 1, RoomType.HalfSuite },
         { 2, RoomType.Suite },
         { 3, RoomType.Double },
         { 4, RoomType.DoubleWithSofa }
     };
            int indexStart = important[item.room.Type];
            bool findRoom = false;
            while (indexStart < important.Count) 
            {
                for(int i = 0; i < rooms[indexRoom[indexStart]].Count;++i)
                {
                    if (!rooms[indexRoom[indexStart]][i].Occupied)
                    {
                        rooms[indexRoom[indexStart]][i].Occupied = true;
                        item.room = rooms[indexRoom[indexStart]][i];
                        //bookingItems.Add(new BookingItem(rooms[indexRoom[indexStart]][i], checkInDate, checkOutDate));
                        findRoom = true;
                        return findRoom;
                    }
                }
                ++indexStart;
            }
            return findRoom;
            throw new ArgumentException("Given room Type was not found or implemented!");
        }



        // выселение с подсчетом стоимости проживания
        public double CheckOut(int CheckOutDay)
        {
            Stack<int> delIndex = new Stack<int>();
            double profit = 0;
            if (bookingItems != null)
            {
                for (int j = 0; j < (bookingItems.Count); ++j)
                {
                    if (bookingItems[j].LastDay == CheckOutDay)
                    {
                        RoomType room = bookingItems[j].room.Type;
                        //int i = CountRooms[room] - 1;
                        //while (i >= 0 && !rooms[room][i].Occupied)
                        //{
                        //    i--;
                        //}
                        //if (i >= 0)
                        //{
                        if (bookingItems[j].room.Number - 1 > 0 && rooms[room][bookingItems[j].room.Number - 1].Occupied)
                        {
                            rooms[room][bookingItems[j].room.Number - 1].Occupied = false;
                            profit += (bookingItems[j].LastDay - bookingItems[j].FirstDay) * bookingItems[j].Cost();
                        }
                        delIndex.Push(j);
                        //}
                    }
                }
            }
            
            while(delIndex.Count > 0)
            {
                bookingItems.RemoveAt(delIndex.Pop());

            }
            return profit;
        }

        // метод подсчета количества занятых комнат в данный момент времени
        public int buzyRooms(RoomType type)
        {
            int cnt = 0;
            for(int i = 0; i < rooms[type].Count;++i)
            {
                if (rooms[type][i].Occupied)
                    ++cnt;
            }
            return cnt;
        }
    }
}

/*
foreach (RoomType room in Enum.GetValues(typeof(RoomType))) // перебор всех типов комнат
            {
                for (int i = 0; i < CountRooms[room]; ++i)
                {
                    if (!rooms[room][i].Occupied)
                    {
                        rooms[room][i].Occupied = false;
                }
            }
}
*/