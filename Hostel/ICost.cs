using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel
{
    public interface ICost
    {
        double Cost(double roomPrice, int daysOccupied, int amoutOfPeople = 0, int maxPeople = 0);
    }
    public class CountRoomPrice : ICost
    {
        // универсальный класс для подсчёта стоимости и создания экземпляра интерфейса
        // если нужна полная стоимость - пишем только цену и кол-во дней.
        // иначе количество людей и максимум для комнаты.
        public double Cost(double roomPrice, int daysOccupied, int amountOfPeople = 0, int maxPeople = 0)
        {
            double discount = 0.7;
            double discountForTwo = 0.8;
            double cost = 0;
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
                        cost = daysOccupied * roomPrice * discountForTwo; break;
                    }
            }
            return cost;
        }
    }
}
