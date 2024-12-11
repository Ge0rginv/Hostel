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
        public ICost _ICost {  get; set; }

        protected Room(int num, double cost, ICost iCost)
        {
            Number = num;
            Price = cost;
            Days = 0;
            Occupied = false;
            _ICost = iCost;
        }
    }

    // Concrete Room classes
    public class SingleRoom : Room
    {
        // цена и название
        public SingleRoom(int num, double cost, ICost icost) : base(num, cost, icost)
        {
            Type = RoomType.Single;
            Max_Count_People = 1;
        }
        public double Cost()
        {
            double cost = _ICost.Cost(Price, Days);
            return cost;
        }
    }

    public class DoubleRoom : Room
    {
        public int Real_People { get; set; } // real count of people
        public DoubleRoom(int num, double cost, ICost icost) : base(num, cost, icost)
        {
            Type = RoomType.Double;
            Max_Count_People = 2;
            Real_People = 0;
        }
        public double Cost()
        {
            double cost = 0;
            if (Real_People == Max_Count_People)
            {
                cost = _ICost.Cost(Price, Days);
            }
            else
            {
                cost = _ICost.Cost(Price, Days, Real_People, Max_Count_People);
            }
            return cost;
        }
    }

    public class Suite : Room
    {
        public Suite(int num, double cost, ICost icost) : base(num, cost, icost)
        {
            Type = RoomType.Suite;
            Max_Count_People = 1;
        }
        public double Cost()
        {
            double cost = _ICost.Cost(Price, Days);
            return cost;
        }
    }

    public class HalfSuite : Room
    {
        public HalfSuite(int num, double cost, ICost icost) : base(num, cost, icost)
        {
            Type = RoomType.HalfSuite;
            Max_Count_People = 1;
        }
        public double Cost()
        {
            double cost = _ICost.Cost(Price, Days);
            return cost;
        }
    }

    public class DoubleWithSofa : Room
    {
        public int Real_People { get; set; } // real count of people
        
        public DoubleWithSofa(int num, double cost, ICost icost) : base(num, cost, icost)
        {
            Type = RoomType.DoubleWithSofa;
            Max_Count_People = 2;
            Real_People = 0;
        }
        public double Cost()
        {
            double cost = 0;
            if (Real_People == Max_Count_People)
            {
                cost = _ICost.Cost(Price, Days);
            }
            else
            {
                cost = _ICost.Cost(Price, Days, Real_People,Max_Count_People);
            }
            return cost;
        }
    }

}
