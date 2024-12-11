using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.InteropServices;

namespace Hostel
{
    // Hotel class
    public class Hotel
    {
        //private readonly List<Room> Rooms;
        public int[] Cnt_Rooms; // количество комнат каждого типа
        // далее массивы с существующими комнатами каждого типа
        public SingleRoom[] singleRooms;
        public DoubleRoom[] doubleRooms;
        public Suite[] suiteRooms;
        public HalfSuite[] halfSuiteRooms;
        public DoubleWithSofa[] doubleWithSofaRooms;

        public int[] Max_Num; // максимальный номер комнаты объединенные в блоки по типу комнаты
        public SortedList<int, int> occupied; // лист занятых или забронированных номеров
        public int[] Count_occupied; // количество занятых комнат объединенные в блоки по типу комнаты
        
        public Hotel(int[] k, double[] cost)
        {
            int sum = 0;
            Cnt_Rooms = k;
            Max_Num = new int[5];
            singleRooms = new SingleRoom[k[0]];
            occupied = new SortedList<int, int>();
            Count_occupied = new int[5];
            for (int i = 0; i < 5; ++i)
            {
                Count_occupied[i] = 0;
            }
            for (int i = 1; i <= k[0]; i++)
            {
                singleRooms[i-1] = new SingleRoom(i + sum, cost[0], new CountRoomPrice ());
            }
            sum += k[0];
            Max_Num[0] = sum;
            doubleRooms = new DoubleRoom[k[1]];
            for (int i = 1; i <= k[1]; i++)
            {
                doubleRooms[i-1] = new DoubleRoom(i + sum, cost[1], new CountRoomPrice());
            }
            sum += k[1];
            Max_Num[1] = sum;
            suiteRooms = new Suite[k[2]];
            for (int i = 1; i <= k[2]; i++)
            {
                suiteRooms[i - 1] = new Suite(i + sum, cost[2], new CountRoomPrice());
            }
            sum += k[2];
            Max_Num[2] = sum;
            halfSuiteRooms = new HalfSuite[k[3]];
            for (int i = 1; i <= k[3]; i++)
            {
                halfSuiteRooms[i-1] = new HalfSuite(i + sum, cost[3], new CountRoomPrice());
            }
            sum += k[3];
            Max_Num[3] = sum;
            doubleWithSofaRooms = new DoubleWithSofa[k[4]];
            for (int i = 1; i <= k[4]; i++)
            {
                doubleWithSofaRooms[i-1] = new DoubleWithSofa(i + sum, cost[4], new CountRoomPrice());
            }
            sum += k[4];
            Max_Num[4] = sum;
        }

        // Бронирование и заселение в номер
        public int BookRoom(string type, int checkInDate, int checkOutDate)
        {
            int resalt = FindAvailableRoom(type, checkInDate, checkOutDate);
            return resalt;
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
                            occupied.Add(currentRoom[i].Number, checkInDate);
                            switch (roomType)
                            {
                                case RoomType.Single:
                                    {
                                        ++Count_occupied[1 - 1]; break;
                                    }
                                case RoomType.Double:
                                    {
                                        ++Count_occupied[2 - 1]; break;
                                    }
                                case RoomType.Suite:
                                    {
                                        ++Count_occupied[3 - 1]; break;
                                    }
                                case RoomType.HalfSuite:
                                    {
                                        ++Count_occupied[4 - 1]; break;
                                    }
                                case RoomType.DoubleWithSofa:
                                    {
                                        ++Count_occupied[5 - 1]; break;
                                    }
                            }
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
        public double CheckOut(int roomNumber)
        {
            if (roomNumber < Max_Num[0])
            {
                double cost = singleRooms[roomNumber].Cost();
                occupied.Remove(roomNumber);
                //occupied.RemoveAt(occupied.Count - 1); // del in Queue or SortedList Occupied
                singleRooms[roomNumber].Occupied = false;
                singleRooms[roomNumber].Days = 0;
                return cost;
            }
            else if (roomNumber < Max_Num[1])
            {
                double cost = doubleRooms[roomNumber - Max_Num[0]].Cost();
                occupied.Remove(roomNumber); // del in SortedList Occupied
                // occupied.RemoveAt(occupied.Count - 1);
                doubleRooms[roomNumber - Max_Num[0]].Occupied = false;
                doubleRooms[roomNumber - Max_Num[0]].Days = 0;
                doubleRooms[roomNumber - Max_Num[0]].Real_People = 0;
                return cost;
            }
            else if (roomNumber < Max_Num[2])
            {
                double cost = suiteRooms[roomNumber - Max_Num[1]].Cost();
                occupied.Remove(roomNumber);
                //occupied.RemoveAt(occupied.Count - 1); // del in Queue or SortedList Occupied
                suiteRooms[roomNumber - Max_Num[1]].Occupied = false;
                suiteRooms[roomNumber - Max_Num[1]].Days = 0;
                return cost;
            }
            else if (roomNumber < Max_Num[3])
            {
                double cost = halfSuiteRooms[roomNumber - Max_Num[2]].Cost();
                occupied.Remove(roomNumber);
                //occupied.RemoveAt(occupied.Count - 1); // del in Queue or SortedList Occupied
                halfSuiteRooms[roomNumber - Max_Num[2]].Occupied = false;
                halfSuiteRooms[roomNumber - Max_Num[2]].Days = 0;
                return cost;
            }
            else
            {
                double cost = doubleWithSofaRooms[roomNumber - Max_Num[3]].Cost();
                occupied.Remove(roomNumber);
                //occupied.RemoveAt(occupied.Count - 1); // del in Queue or SortedList Occupied
                doubleWithSofaRooms[roomNumber - Max_Num[3]].Occupied = false;
                doubleWithSofaRooms[roomNumber - Max_Num[3]].Days = 0;
                doubleWithSofaRooms[roomNumber - Max_Num[3]].Real_People = 0;
                return cost;
            }
        }

        // метод подсчета количества занятых комнат в данный момент времени
        public int buzyRooms(string type, int time) 
        {
            int cnt = 0;
            if (type == "Single")
            {
                for (int i = 0; i < singleRooms.Length; ++i)
                {
                    if (!singleRooms[i].Occupied && singleRooms[i].FirstDay >= time / 24)
                    {
                        ++cnt;
                    }
                }
            }
            else if (type == "HalfSuite")
            {
                for (int i = 0; i < halfSuiteRooms.Length; ++i)
                {
                    if (!halfSuiteRooms[i].Occupied && halfSuiteRooms[i].FirstDay >= time / 24)
                    {
                        ++cnt;
                    }
                }
            }
            else if (type == "Suite")
            {
                for (int i = 0; i < suiteRooms.Length; ++i)
                {
                    if (!suiteRooms[i].Occupied && suiteRooms[i].FirstDay >= time / 24)
                    {
                        ++cnt;
                    }
                }
            }
            else if (type == "Double")
            {
                for (int i = 0; i < doubleRooms.Length; ++i)
                {
                    if (!doubleRooms[i].Occupied && doubleRooms[i].FirstDay >= time / 24)
                    {
                        ++cnt;
                    }
                }
            }
            else
            {
                for (int i = 0; i < doubleWithSofaRooms.Length; ++i)
                {
                    if (!doubleWithSofaRooms[i].Occupied && doubleWithSofaRooms[i].FirstDay >= time / 24)
                    {
                        ++cnt;
                    }
                }
            }
            return cnt;
        }
        public double Cost(int number)
        {
            double cost = 0;
            int id = 0;
            for (int i = 0; i < Max_Num.Length; ++i) 
            {
                if (number <= Max_Num[i])
                {
                    id = i; 
                    break;
                }
            }
            return cost;
        }
    }

}


/*
 public bool BookRoom(RoomType type)
 {
     var room = Rooms.FirstOrDefault(r => r.Type == type && !r.IsOccupied);
     if (room != null)
     {
         //room.CheckIn();
         Console.WriteLine($"Room {room.Number} of type {room.Type} booked.");
         return true;
     }
     Console.WriteLine($"No available rooms of type {type}.");
     return false;
 }
*/

/*
public double CheckOut(int roomNumber)
{
            var room = Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (room != null && room.IsOccupied)
            {
                //room.CheckOut();
                Console.WriteLine($"Room {room.RoomNumber} checked out.");
            }
            else
            {
                Console.WriteLine($"Room {roomNumber} is not occupied or does not exist.");
            }
}



        //public Hotel(int totalRooms)
        //{
        //    Rooms = new List<Room>();
        //    Customers = new List<Customer>();
        //    InitializeRooms(totalRooms);
        //}

        //private void InitializeRooms(int totalRooms)
        //{
        //    for (int i = 1; i <= totalRooms; i++)
        //    {
        //        if (i <= 10) Rooms.Add(new Suite(i, 1));
        //        else if (i <= 15) Rooms.Add(new HalfSuite(i, 1));
        //        else if (i <= 20) Rooms.Add(new DoubleRoom(i, 2));
        //        else if (i <= 25) Rooms.Add(new DoubleWithSofa(i, 2));
        //        else Rooms.Add(new SingleRoom(i, 1));
        //    }
        //}







    internal static class Program
    {
        // класс пары для храниния двух целых значений
        // скорее всего не потребуется, но на всякий случай есть
        public struct pair : IComparable<pair> 
        {
            public int f;
            public int s;
            public pair(int f, int s)
            {
                this.f = f;
                this.s = s;
            }
            public int CompareTo(pair point)
            {
                if (this.f == point.f)
                    return s.CompareTo(point.s);
                return f.CompareTo(point.f);
            }
            public override string ToString()
            {
                return string.Format($"{f} {s}");
                //return string.Format($"{f:f6} {s}");
            }
        }
    }
*/