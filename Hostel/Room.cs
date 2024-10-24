using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel
{

    public enum RoomType
    {
        Single, // Одноместный
        Double, // Двухместный
        DoubleWithSofa, // Двухместный с диваном
        SemiLux, // Полулюкс
        Lux // Люкс
    }

    public class Room
    {
        public int RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public double PricePerDay { get; set; }
        public bool IsOccupied { get; set; }
        public List<Booking> Bookings { get; set; }

        public Room(int roomNumber, RoomType type, double pricePerDay)
        {
            RoomNumber = roomNumber;
            Type = type;
            PricePerDay = pricePerDay;
            IsOccupied = false;
            Bookings = new List<Booking>();
        }
    }


    public class Booking
    {
        public int BookingId { get; set; }
        public Room Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public double TotalCost { get; set; }

        public Booking(Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            Room = room;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            TotalCost = CalculateTotalCost();
        }

        private double CalculateTotalCost()
        {
            int days = (CheckOutDate - CheckInDate).Days;
            return days * Room.PricePerDay;
        }
    }


    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string PassportNumber { get; set; }
        public List<Booking> Bookings { get; set; }

        public Customer(string name, string passportNumber)
        {
            Name = name;
            PassportNumber = passportNumber;
            Bookings = new List<Booking>();
        }
    }



    public class Hotel
    {
        public List<Room> Rooms { get; set; }
        public List<Customer> Customers { get; set; }

        public Hotel(List<Room> rooms)
        {
            Rooms = rooms;
            Customers = new List<Customer>();
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