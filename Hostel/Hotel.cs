using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Hostel
{
    // Hotel class
    internal static class Program
    {
        public struct pair : IComparable<pair>
        {
            public double f;
            public int s;
            public pair(double f, int s)
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
                return string.Format($"{f:f6} {s}");
            }
        }
    }
       

    public class Hotel 
    {
        private readonly List<Room> Rooms;
        int[] Cnt_Rooms;
        private SingleRoom[] singleRooms;
        private DoubleRoom[] doubleRooms;
        private Suite[] suiteRooms;
        private HalfSuite[] halfSuiteRooms;
        private DoubleWithSofa[] doubleWithSofaRooms;
        private int[] Max_Num;
        private SortedList<int,int> occupied;
        //public PriorityQueue <int, int> Occupied = new PriorityQueue<int, int> ();
        public List<Customer> Customers { get; set; }

        public Hotel(int[] k, double[] cost)
        {
            int sum = 0;
            Cnt_Rooms = k;
            Max_Num = new int[5];
            singleRooms = new SingleRoom[k[0]];
            occupied = new SortedList<int,int>();
            for (int i = 1; i <= k[0];i++)
            {
                singleRooms[i] = new SingleRoom(i + sum, cost[0]);
            }
            sum += k[0];
            Max_Num[0] = sum;
            doubleRooms = new DoubleRoom[k[1]];
            for (int i = 1; i <= k[1]; i++)
            {
                doubleRooms[i] = new DoubleRoom(i + sum, cost[1]);
            }
            sum += k[1];
            Max_Num[1] = sum;
            suiteRooms = new Suite[k[2]];
            for (int i = 1; i <= k[2]; i++)
            {
                suiteRooms[i] = new Suite(i + sum, cost[2]);
            }
            sum += k[2];
            Max_Num[2] = sum;
            halfSuiteRooms = new HalfSuite[k[3]];
            for (int i = 1; i <= k[3]; i++)
            {
                halfSuiteRooms[i] = new HalfSuite(i + sum, cost[3]);
            }
            sum += k[3];
            Max_Num[3] = sum;
            doubleWithSofaRooms = new DoubleWithSofa[k[4]];
            for (int i = 1; i <= k[4]; i++)
            {
                doubleWithSofaRooms[i] = new DoubleWithSofa(i + sum, cost[4]);
            }
            sum += k[4];
            Max_Num[4] = sum;
        }
        //public Hotel(int totalRooms)
        //{
        //    Rooms = new List<Room>();
        //    Customers = new List<Customer>();
        //    InitializeRooms(totalRooms);
        //}
        // Поиск доступных номеров по типу
        //public Room FindAvailableRoom(RoomType type, DateTime checkInDate, DateTime checkOutDate)
        //{
        //    foreach (var room in Rooms)
        //    {
        //        if (room.Type == type && IsRoomAvailable(room, checkInDate, checkOutDate))
        //        {
        //            return room;
        //        }
        //    }
        //    return null;
        //}
        // Проверка доступности номера
        //private bool IsRoomAvailable(Room room, DateTime checkInDate, DateTime checkOutDate)
        //{
        //    foreach (var booking in room.Bookings)
        //    {
        //        if (!(checkOutDate <= booking.CheckInDate || checkInDate >= booking.CheckOutDate))
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
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

        // Бронирование номера
        //public bool BookRoom(Customer customer, RoomType roomType, DateTime checkInDate, DateTime checkOutDate)
        //{
        //    var room = FindAvailableRoom(roomType, checkInDate, checkOutDate);
        //    if (room != null)
        //    {
        //        var booking = new Booking(room, checkInDate, checkOutDate);
        //        room.Bookings.Add(booking);
        //        customer.Bookings.Add(booking);
        //        return true;
        //    }
        //    return false;
        //}

        // выселение
        public double CheckOut(int roomNumber)
        {
            if (roomNumber < Max_Num[0])
            {
                double cost = singleRooms[roomNumber].Cost();
                occupied.RemoveAt(occupied.Count - 1); // del in Queue or SortedList Occupied
                singleRooms[roomNumber].Occupied = false;
                singleRooms[roomNumber].Days = 0;
                return cost;
            }
            else if(roomNumber < Max_Num[1])
            {
                double cost = doubleRooms[roomNumber - Max_Num[0]].Cost();
                occupied.RemoveAt(occupied.Count - 1); // del in SortedList Occupied
                doubleRooms[roomNumber - Max_Num[0]].Occupied = false;
                doubleRooms[roomNumber - Max_Num[0]].Days = 0;
                doubleRooms[roomNumber - Max_Num[0]].Real_People = 0;
                return cost;
            }
            else if (roomNumber < Max_Num[2])
            {
                double cost = suiteRooms[roomNumber - Max_Num[1]].Cost();
                occupied.RemoveAt(occupied.Count - 1); // del in SortedList Occupied
                suiteRooms[roomNumber - Max_Num[1]].Occupied = false;
                suiteRooms[roomNumber - Max_Num[1]].Days = 0;
                return cost;
            }
            else if (roomNumber < Max_Num[3])
            {
                double cost = halfSuiteRooms[roomNumber - Max_Num[2]].Cost();
                occupied.RemoveAt(occupied.Count - 1); // del in SortedList Occupied
                halfSuiteRooms[roomNumber - Max_Num[2]].Occupied = false;
                halfSuiteRooms[roomNumber - Max_Num[2]].Days = 0;
                return cost;
            }
            else
            {
                double cost = doubleWithSofaRooms[roomNumber - Max_Num[3]].Cost();
                occupied.RemoveAt(occupied.Count - 1); // del in SortedList Occupied
                doubleWithSofaRooms[roomNumber - Max_Num[3]].Occupied = false;
                doubleWithSofaRooms[roomNumber - Max_Num[3]].Days = 0;
                doubleWithSofaRooms[roomNumber - Max_Num[3]].Real_People = 0;
                return cost;
            }
            //var room = Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            //if (room != null && room.IsOccupied)
            //{
            //    //room.CheckOut();
            //    Console.WriteLine($"Room {room.RoomNumber} checked out.");
            //}
            //else
            //{
            //    Console.WriteLine($"Room {roomNumber} is not occupied or does not exist.");
            //}
        }

        //public void DisplayRoomStatus()
        //{
        //    foreach (var room in Rooms)
        //    {
        //        // исправить вывод данных
        //        Console.WriteLine($"Room {room.RoomNumber}: Type = {room.Type}, Price = {room.PricePerDay}, Occupied = {room.IsOccupied}");
        //    }
        //}
    }

}
/*
 * ресепшен прием и обработка заявок от генератора
 * хранить количество заброненых и занятых
 * к перечеслению привязать стоимость
 */




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