using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel
{
    public interface IRoom
    {
        //int Number { get; } // количество номеров
        //RoomType Type { get; } // тип номера
        //double Prise { get; } // Цена
        //int Max_Count_People { get; } // макс количество человек
        // Привелегии
        double Cost(); 
        //bool IsOccupied { get; }
        //void CheckIn();
        //void CheckOut();
    }

    
}
