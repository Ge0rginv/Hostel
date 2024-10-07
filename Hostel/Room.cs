using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel
{

    // Enum виды номерев
    public enum RoomType
    {
        Single,//одноместный
        Double,//двухместный
        Suite,//люкс
        HalfSuite,//полулюкс
        DoubleWithSofa//двухместный с диваном
    }

    public abstract class Room : IRoom
    {
        public int Number { get; }
        public RoomType Type { get; }
        public double Price { get; }
        public bool IsOccupied { get; private set; }

        protected Room(int number, RoomType type, double price)
        {
            Number = number;
            Type = type;
            Price = price;
            IsOccupied = false;
        }

        //public virtual void CheckIn()
        //{
        //    IsOccupied = true;
        //}

        //public virtual void CheckOut()
        //{
        //    IsOccupied = false;
        //}
        public void CheckIn() => IsOccupied = true;
        public void CheckOut() => IsOccupied = false;
    }

    // Concrete Room classes
    public class SingleRoom : Room
    {
        public SingleRoom(int number) : base(number, RoomType.Single, 70) { }
    }

    public class DoubleRoom : Room
    {
        public DoubleRoom(int number) : base(number, RoomType.Double, 100) { }
    }

    public class Suite : Room
    {
        public Suite(int number) : base(number, RoomType.Suite, 120) { }
    }

    public class HalfSuite : Room
    {
        public HalfSuite(int number) : base(number, RoomType.HalfSuite, 90) { }
    }

    public class DoubleWithSofa : Room
    {
        public DoubleWithSofa(int number) : base(number, RoomType.DoubleWithSofa, 110) { }
    }
}
