using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel
{
    public enum RoomType
    {
        Single,//одноместный
        Double,//двухместный
        Suite,//люкс
        HalfSuite,//полулюкс
        DoubleWithSofa//двухместный с диваном
    }
    public abstract class Room 
    {
        public int Number { get; } // количество номеров
        public RoomType Type { get; set; } // тип номера
        public double Price { get; } // Цена
        public int Max_Count_People { get; set; } // max count people
        public int Days { get; set; }
        public bool Occupied { get; set; } // buzy or not
        public int FirstDay { get; set; }  
        public int LastDay { get; set; }

        protected Room(int num, double cost)
        {
            Number = num;
            Price = cost;
            Days = 0;
            Occupied = false;
        }
        public abstract double Cost();
    }

    // Concrete Room classes
    public class SingleRoom : Room
    {
        // цена и название
        public SingleRoom(int num, double cost) : base(num, cost)
        {
            Type = RoomType.Single;
            Max_Count_People = 1;
        }
        public override double Cost()
        {
            double cost = Days * Price;
            return cost;
        }
        //public SingleRoom(int number, int people_cnt)  { }
    }

    public class DoubleRoom : Room
    {
        public int Real_People { get; set; } // real count of people
        public DoubleRoom(int num, double cost) : base(num, cost)
        {
            Type = RoomType.Double;
            Max_Count_People = 2;
            Real_People = 0;
        }
        public override double Cost()
        {
            double cost = 0;
            if (Real_People == Max_Count_People)
            {
                cost = Price * Days;
            }
            else
            {
                cost = Real_People * Days * 0.7;
            }
            return cost;
        }
        //public DoubleRoom(int number, int people_cnt) : base(number, RoomType.Double, people_cnt, 100) { }
    }

    public class Suite : Room
    {
        public Suite(int num, double cost) : base(num, cost)
        {
            Type = RoomType.Suite;
            Max_Count_People = 1;
        }
        public override double Cost()
        {
            double cost = Days * Price;
            return cost;
        }
        //public Suite(int number, int people_cnt) : base(number, RoomType.Suite, people_cnt, 120) { }
    }

    public class HalfSuite : Room
    {
        public HalfSuite(int num, double cost) : base(num, cost)
        {
            Type = RoomType.HalfSuite;
            Max_Count_People = 1;
        }
        public override double Cost()
        {
            double cost = Days * Price;
            return cost;
        }
        //public HalfSuite(int number, int people_cnt) : base(number, RoomType.HalfSuite, people_cnt, 90) { }
    }

    public class DoubleWithSofa : Room
    {
        public int Real_People { get; set; } // real count of people
        public DoubleWithSofa(int num, double cost) : base(num, cost)
        {
            Type = RoomType.DoubleWithSofa;
            Max_Count_People = 2;
            Real_People = 0;
        }
        public override double Cost()
        {
            double cost = 0;
            if (Real_People == Max_Count_People)
            {
                cost = Price * Days;
            }
            else
            {
                cost = Real_People * Days * 0.7;
            }
            return cost;
        }
        //public DoubleWithSofa(int number, int people_cnt) : base(number, RoomType.DoubleWithSofa, people_cnt, 110) { }
    }
}




/*
        //public virtual void CheckIn()
        //{
        //    IsOccupied = true;
        //}

        //public virtual void CheckOut()
        //{
        //    IsOccupied = false;
        //}
        //public void CheckIn() => IsOccupied = true;
        //public void CheckOut() => IsOccupied = false;



    //public class Booking
    //{
    //    public int BookingId { get; set; }
    //    public Room Room { get; set; }
    //    public DateTime CheckInDate { get; set; }
    //    public DateTime CheckOutDate { get; set; }
    //    public double TotalCost { get; set; }

    //    public Booking(Room room, DateTime checkInDate, DateTime checkOutDate)
    //    {
    //        Room = room;
    //        CheckInDate = checkInDate;
    //        CheckOutDate = checkOutDate;
    //        TotalCost = CalculateTotalCost();
    //    }

    //    private double CalculateTotalCost()
    //    {
    //        int days = (CheckOutDate - CheckInDate).Days;
    //        return days * Room.Price;
    //    }
    //}

    //public class Customer
    //{
    //    public int CustomerId { get; set; }
    //    public string Name { get; set; }
    //    public string PassportNumber { get; set; }
    //    public List<Booking> Bookings { get; set; }

    //    public Customer(string name, string passportNumber)
    //    {
    //        Name = name;
    //        PassportNumber = passportNumber;
    //        Bookings = new List<Booking>();
    //    }
    //}
*/
