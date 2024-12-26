

namespace ClassLibrary
{
    public abstract class Room
    {
        public int Number { get; } // номер комнаты
        public RoomType Type { get; set; } // тип номера
        public double Price { get; } // Цена
        public int Max_Count_People { get; set; } // max count people
        public int Real_People { get; set; } // real count of people
        public bool Occupied { get; set; } // buzy or not
        public ICostCounter _ICost { get; set; }
        protected Room() { }
        protected Room(int num, double cost, ICostCounter iCost)
        {
            Number = num;
            Price = cost;
            Occupied = false;
            _ICost = iCost;
        }
    }

    // Concrete Room classes
    public class SingleRoom : Room
    {
        // цена и название
        public SingleRoom() : base()
        {
            Type = RoomType.Single;
            Max_Count_People = 1;
        }
        public SingleRoom(int num, double cost, ICostCounter icost) : base(num, cost, icost)
        {
            Type = RoomType.Single;
            Max_Count_People = 1;
        }
      
    }

    public class DoubleRoom : Room
    {
        //public int Real_People { get; set; } // real count of people
        public DoubleRoom() : base()
        {
            Type = RoomType.Double;
            Max_Count_People = 2;
            Real_People = 0;
        }
        public DoubleRoom(int num, double cost, ICostCounter icost) : base(num, cost, icost)
        {
            Type = RoomType.Double;
            Max_Count_People = 2;
            Real_People = 0;
        }
        
    }

    public class SuiteRoom : Room
    {
        public SuiteRoom() : base()
        {
            Type = RoomType.Suite;
            Max_Count_People = 1;
        }
        public SuiteRoom(int num, double cost, ICostCounter icost) : base(num, cost, icost)
        {
            Type = RoomType.Suite;
            Max_Count_People = 1;
        }
      
    }

    public class HalfSuiteRoom : Room
    {
        public HalfSuiteRoom() : base()
        {
            Type = RoomType.HalfSuite;
            Max_Count_People = 1;
        }
        public HalfSuiteRoom(int num, double cost, ICostCounter icost) : base(num, cost, icost)
        {
            Type = RoomType.HalfSuite;
            Max_Count_People = 1;
        }
        
    }

    public class DoubleWithSofaRoom : Room
    {
        //public int Real_People { get; set; } // real count of people
        public DoubleWithSofaRoom() : base()
        {
            Type = RoomType.DoubleWithSofa;
            Max_Count_People = 2;
            Real_People = 0;
        }
        public DoubleWithSofaRoom(int num, double cost, ICostCounter icost) : base(num, cost, icost)
        {
            Type = RoomType.DoubleWithSofa;
            Max_Count_People = 2;
            Real_People = 0;
        }
      
    }

}


/*
    SingleRoom
    DoubleRoom
    SuiteRoom
    HalfSuiteRoom 
    DoubleWithSofaRoom 
 */
