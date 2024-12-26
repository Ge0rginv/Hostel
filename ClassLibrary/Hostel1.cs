using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public struct RoomAndDayOfEviction1 : IComparable<RoomAndDayOfEviction1>
    {
        public int First;
        public int Second;
        public RoomAndDayOfEviction1(int first, int second)
        {
            this.First = first;
            this.Second = second;
        }
        public int CompareTo(RoomAndDayOfEviction1 point)
        {
            if (this.First == point.First)
                return Second.CompareTo(point.Second);
            return First.CompareTo(point.First);
        }
        public override string ToString()
        {
            return string.Format($"{First} {Second}");
        }
    }
    // Hotel class
    public class Hotel1
    {
        public int[] Cnt_Rooms; // количество комнат каждого типа
        // далее массивы с существующими комнатами каждого типа
        public SingleRoom[] singleRooms;
        public DoubleRoom[] doubleRooms;
        public Suite[] suiteRooms;
        public HalfSuite[] halfSuiteRooms;
        public DoubleWithSofa[] doubleWithSofaRooms;
        public int[] Max_Num; // максимальный номер комнаты объединенные в блоки по типу комнаты

        public List <List<BookingItem>> bookingItems {  get; set; }
        public Dictionary<RoomType, List<Room>> rooms { get; set; }

        public Hotel1(int[] TheNumberOfRoomsOfDifferentTypes, double[] TheCostOfRoomsOfDifferentTypes)
        {
            List<List<BookingItem>> bookingItems = new List<List<BookingItem>>(TheNumberOfRoomsOfDifferentTypes.Length);
            Dictionary<RoomType, List<Room>> rooms = new Dictionary<RoomType, List<Room>>();
            
            foreach(RoomType room in Enum.GetValues(typeof(RoomType))) // перебор всех типов комнат
            {
                rooms.Add(room, new List<Room>());
            }

            int sum = 0;
            Cnt_Rooms = TheNumberOfRoomsOfDifferentTypes;
            Max_Num = new int[5];
            singleRooms = new SingleRoom[TheNumberOfRoomsOfDifferentTypes[0]];
            
            for (int i = 1; i <= TheNumberOfRoomsOfDifferentTypes[0]; i++)
            {
                singleRooms[i - 1] = new SingleRoom(i + sum, TheCostOfRoomsOfDifferentTypes[0], new CountRoomPrice());
            }
            sum += TheNumberOfRoomsOfDifferentTypes[0];
            Max_Num[0] = sum;
            doubleRooms = new DoubleRoom[TheNumberOfRoomsOfDifferentTypes[1]];
            for (int i = 1; i <= TheNumberOfRoomsOfDifferentTypes[1]; i++)
            {
                doubleRooms[i - 1] = new DoubleRoom(i + sum, TheCostOfRoomsOfDifferentTypes[1], new CountRoomPrice());
            }
            sum += TheNumberOfRoomsOfDifferentTypes[1];
            Max_Num[1] = sum;
            suiteRooms = new Suite[TheNumberOfRoomsOfDifferentTypes[2]];
            for (int i = 1; i <= TheNumberOfRoomsOfDifferentTypes[2]; i++)
            {
                suiteRooms[i - 1] = new Suite(i + sum, TheCostOfRoomsOfDifferentTypes[2], new CountRoomPrice());
            }
            sum += TheNumberOfRoomsOfDifferentTypes[2];
            Max_Num[2] = sum;
            halfSuiteRooms = new HalfSuite[TheNumberOfRoomsOfDifferentTypes[3]];
            for (int i = 1; i <= TheNumberOfRoomsOfDifferentTypes[3]; i++)
            {
                halfSuiteRooms[i - 1] = new HalfSuite(i + sum, TheCostOfRoomsOfDifferentTypes[3], new CountRoomPrice());
            }
            sum += TheNumberOfRoomsOfDifferentTypes[3];
            Max_Num[3] = sum;
            doubleWithSofaRooms = new DoubleWithSofa[TheNumberOfRoomsOfDifferentTypes[4]];
            for (int i = 1; i <= TheNumberOfRoomsOfDifferentTypes[4]; i++)
            {
                doubleWithSofaRooms[i - 1] = new DoubleWithSofa(i + sum, TheCostOfRoomsOfDifferentTypes[4], new CountRoomPrice());
            }
            sum += TheNumberOfRoomsOfDifferentTypes[4];
            Max_Num[4] = sum;
        }

        // Бронирование и заселение в номер
        public int BookRoom(string type, int checkInDate, int checkOutDate)
        {
            int result = FindAvailableRoom(type, checkInDate, checkOutDate);
            return result;
        }



        //Поиск доступных номеров по типу
        // метод возвращает номер забронированной комнаты или -1 если подходящих номеров не нашлось
        public int FindAvailableRoom(string type, int checkInDate, int checkOutDate)
        {
            // словарь который определяет важность каждой комнаты для последующего предложения улучшения условий
            Dictionary<string, Room[]> important = new Dictionary<string, Room[]>()
     {
         { "Single", singleRooms },
         { "HalfSuite", doubleRooms },
         { "Suite", suiteRooms },
         {"Double", halfSuiteRooms },
         {"DoubleWithSofa", doubleWithSofaRooms }
     };
            RoomType roomType;
            if (RoomType.TryParse(type, out roomType))
            {
                Room[] currentRoom;
                while (important.TryGetValue(roomType.ToString(), out currentRoom))
                {
                    for (int i = 0; i < currentRoom.Length; ++i)
                    {
                        if (!currentRoom[i].Occupied)
                        {
                            currentRoom[i].Occupied = true;
                            currentRoom[i].FirstDay = checkInDate;
                            currentRoom[i].LastDay = checkOutDate;
                            currentRoom[i].Days = checkOutDate - checkInDate;

                            return currentRoom[i].Number;
                        }
                    }
                    ++roomType;
                }
                return -1;
            }
            throw new ArgumentException("Given room Type was not found or implemented!");
        }



        // выселение с подсчетом стоимости проживания
        public double CheckOut(int roomNumber, int CheckOutDay)
        {
            if (roomNumber <= Max_Num[0])
            {
                double cost = singleRooms[roomNumber - 1].Cost();
                //occupied.RemoveAt(occupied.Count - 1); // del in Queue or SortedList Occupied
                singleRooms[roomNumber - 1].Occupied = false;
                singleRooms[roomNumber - 1].Days = 0;
                return cost;
            }
            else if (roomNumber <= Max_Num[1])
            {
                double cost = doubleRooms[roomNumber - Max_Num[0] - 1].Cost();
                // occupied.RemoveAt(occupied.Count - 1);
                doubleRooms[roomNumber - Max_Num[0] - 1].Occupied = false;
                doubleRooms[roomNumber - Max_Num[0] - 1].Days = 0;
                doubleRooms[roomNumber - Max_Num[0] - 1].Real_People = 0;
                return cost;
            }
            else if (roomNumber <= Max_Num[2])
            {
                double cost = suiteRooms[roomNumber - Max_Num[1] - 1].Cost();
                //occupied.RemoveAt(occupied.Count - 1); // del in Queue or SortedList Occupied
                suiteRooms[roomNumber - Max_Num[1] - 1].Occupied = false;
                suiteRooms[roomNumber - Max_Num[1] - 1].Days = 0;
                return cost;
            }
            else if (roomNumber <= Max_Num[3])
            {
                double cost = halfSuiteRooms[roomNumber - Max_Num[2] - 1].Cost();
                //occupied.RemoveAt(occupied.Count - 1); // del in Queue or SortedList Occupied
                halfSuiteRooms[roomNumber - Max_Num[2] - 1].Occupied = false;
                halfSuiteRooms[roomNumber - Max_Num[2] - 1].Days = 0;
                return cost;
            }
            else
            {
                double cost = doubleWithSofaRooms[roomNumber - Max_Num[3] - 1].Cost();
                //occupied.RemoveAt(occupied.Count - 1); // del in Queue or SortedList Occupied
                doubleWithSofaRooms[roomNumber - Max_Num[3] - 1].Occupied = false;
                doubleWithSofaRooms[roomNumber - Max_Num[3] - 1].Days = 0;
                doubleWithSofaRooms[roomNumber - Max_Num[3] - 1].Real_People = 0;
                return cost;
            }
        }

        // метод подсчета количества занятых комнат в данный момент времени
        public int buzyRooms(RoomType type, int time)
        {
            int cnt = 0;
            for(int i = 0; i < rooms[type].Count;++i)
            {
                if (!rooms[type][i].Occupied)
                    ++cnt;
            }
            return cnt;
        }
    }
}
