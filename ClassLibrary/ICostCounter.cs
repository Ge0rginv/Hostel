using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public interface ICostCounter
    {
        double Cost(Room r);
    }
    public class SingleRoomCostCounter : ICostCounter
    {
        public double Cost(Room r)
        {
            double discount = 0.7;
            double discountForTwo = 1.0;
            double cost = 0;
            if (r.Max_Count_People == 2)
            {
                switch (r.Real_People)
                {
                    case 0:
                        {
                            cost = r.Price; break;
                        }
                    case 1:
                        {
                            cost = r.Price * discount; break;
                        }
                    case 2:
                        {
                            cost = r.Price * discountForTwo; break;
                        }
                }
            }
            else
            {
                cost = r.Price;
            }
            return cost;
        }
    }
    public class DoubleRoomCostCounter : ICostCounter
    {
        public double Cost(Room r)
        {
            double discount = 0.7;
            double discountForTwo = 1.0;
            double cost = 0;
            if (r.Max_Count_People != r.Real_People)
            {
                switch (r.Real_People)
                {
                    case 0:
                        {
                            cost = r.Price; break;
                        }
                    case 1:
                        {
                            cost = r.Price * discount; break;
                        }
                    case 2:
                        {
                            cost = r.Price * discountForTwo; break;
                        }
                }
            }
            else
            {
                cost = r.Price;
            }
            return cost;
        }
    }
    public class SuiteRoomCostCounter : ICostCounter
    {
        public double Cost(Room r)
        {
            double discount = 0.7;
            double discountForTwo = 1.0;
            double cost = 0;
            if (r.Max_Count_People != r.Real_People)
            {
                switch (r.Real_People)
                {
                    case 0:
                        {
                            cost = r.Price; break;
                        }
                    case 1:
                        {
                            cost = r.Price * discount; break;
                        }
                    case 2:
                        {
                            cost = r.Price * discountForTwo; break;
                        }
                }
            }
            else
            {
                cost = r.Price;
            }
            return cost;
        }
    }
    public class HalfSuiteRoomCostCounter : ICostCounter
    {
        public double Cost(Room r)
        {
            double discount = 0.7;
            double discountForTwo = 1.0;
            double cost = 0;
            if (r.Max_Count_People != r.Real_People)
            {
                switch (r.Real_People)
                {
                    case 0:
                        {
                            cost = r.Price; break;
                        }
                    case 1:
                        {
                            cost = r.Price * discount; break;
                        }
                    case 2:
                        {
                            cost = r.Price * discountForTwo; break;
                        }
                }
            }
            else
            {
                cost = r.Price;
            }
            return cost;
        }
    }
    public class DoubleWithSofaRoomCostCounter : ICostCounter
    {
        public double Cost(Room r)
        {
            double discount = 0.7;
            double discountForTwo = 1.0;
            double cost = 0;
            if (r.Max_Count_People != r.Real_People)
            {
                switch (r.Real_People)
                {
                    case 0:
                        {
                            cost = r.Price; break;
                        }
                    case 1:
                        {
                            cost = r.Price * discount; break;
                        }
                    case 2:
                        {
                            cost = r.Price * discountForTwo; break;
                        }
                }
            }
            else
            {
                cost = r.Price;
            }
            return cost;
        }
    }
}
