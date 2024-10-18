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
        public int People_cnt { get; }
        public double Price { get; }
        public bool IsOccupied { get; private set; }

        protected Room(int number, RoomType type, int people_cnt, double price)
        {
            Number = number;
            Type = type;
            People_cnt = people_cnt;
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
        // цена и название
        public SingleRoom(int number, int people_cnt) : base(number, RoomType.Single, people_cnt , 70) { }
    }

    public class DoubleRoom : Room
    {
        public DoubleRoom(int number, int people_cnt) : base(number, RoomType.Double, people_cnt, 100) { }
    }

    public class Suite : Room
    {
        public Suite(int number, int people_cnt) : base(number, RoomType.Suite, people_cnt, 120) { }
    }

    public class HalfSuite : Room
    {
        public HalfSuite(int number, int people_cnt) : base(number, RoomType.HalfSuite, people_cnt, 90) { }
    }

    public class DoubleWithSofa : Room
    {
        public DoubleWithSofa(int number, int people_cnt) : base(number, RoomType.DoubleWithSofa, people_cnt, 110) { }
    }
}
