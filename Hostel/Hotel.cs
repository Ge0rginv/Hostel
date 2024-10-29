using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel
{
    // Hotel class
    public class Hotel : IHotel
    {
        private readonly List<Room> Rooms;
        public List<Customer> Customers { get; set; }
        public Hotel(int totalRooms)
        {
            Rooms = new List<Room>();
            Customers = new List<Customer>();
            InitializeRooms(totalRooms);
        }
        // Поиск доступных номеров по типу
        public Room FindAvailableRoom(RoomType type, DateTime checkInDate, DateTime checkOutDate)
        {
            foreach (var room in Rooms)
            {
                if (room.Type == type && IsRoomAvailable(room, checkInDate, checkOutDate))
                {
                    return room;
                }
            }
            return null;
        }
        // Проверка доступности номера
        private bool IsRoomAvailable(Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            foreach (var booking in room.Bookings)
            {
                if (!(checkOutDate <= booking.CheckInDate || checkInDate >= booking.CheckOutDate))
                {
                    return false;
                }
            }
            return true;
        }
        private void InitializeRooms(int totalRooms)
        {
            for (int i = 1; i <= totalRooms; i++)
            {
                if (i <= 10) Rooms.Add(new Suite(i, 1));
                else if (i <= 15) Rooms.Add(new HalfSuite(i, 1));
                else if (i <= 20) Rooms.Add(new DoubleRoom(i, 2));
                else if (i <= 25) Rooms.Add(new DoubleWithSofa(i, 2));
                else Rooms.Add(new SingleRoom(i, 1));
            }
        }

        // Бронирование номера
        public bool BookRoom(Customer customer, RoomType roomType, DateTime checkInDate, DateTime checkOutDate)
        {
            var room = FindAvailableRoom(roomType, checkInDate, checkOutDate);
            if (room != null)
            {
                var booking = new Booking(room, checkInDate, checkOutDate);
                room.Bookings.Add(booking);
                customer.Bookings.Add(booking);
                return true;
            }
            return false;
        }

        public void CheckOut(int roomNumber)
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

        public void DisplayRoomStatus()
        {
            foreach (var room in Rooms)
            {
                // исправить вывод данных
                Console.WriteLine($"Room {room.RoomNumber}: Type = {room.Type}, Price = {room.PricePerDay}, Occupied = {room.IsOccupied}");
            }
        }
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