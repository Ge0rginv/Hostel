using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel
{
    public interface IRoom
    {
        int Number { get; }
        RoomType Type { get; }
        double Price { get; }
        bool IsOccupied { get; }
        void CheckIn();
        void CheckOut();
    }

    
}
