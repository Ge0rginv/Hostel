using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel
{
    public interface ICost
    {
        double Cost(double roomPrice, int daysOccupied, int amoutOfPeople = 1, int maxPeople = 1);
    }
    public class CountRoomPrice : ICost
    {
        // универсальный класс для подсчёта стоимости и создания экземпляра интерфейса
        // если нужна полная стоимость - пишем только цену и кол-во дней.
        // иначе количество людей и максимум для комнаты.
        public double Cost(double roomPrice, int daysOccupied, int amountOfPeople = 1, int maxPeople = 1)
        {
            //double discount = 0.7;
            double discount = 0.7;
            double discountForTwo = 1.0;
            double cost = 0;
            if(maxPeople == 2)
            {
                switch (amountOfPeople)
                {
                    case 0:
                        {
                            cost = daysOccupied * roomPrice; break;
                        }
                    case 1:
                        {
                            cost = daysOccupied * roomPrice * discount; break;
                        }
                    case 2:
                        {
                            cost = daysOccupied * roomPrice; break;
                        }
                }
            }
            else
            {
                cost = daysOccupied * roomPrice; 
                //switch (amountOfPeople)
                //{
                //    case 0:
                //        {
                //            cost = daysOccupied * roomPrice; break;
                //        }
                //    case 1:
                //        {
                //            cost = daysOccupied * roomPrice; break;
                //        }
                //}
            }
            return cost;
        }
    }
}
