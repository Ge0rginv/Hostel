using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel
{
    public interface IHotel
    {
        bool BookRoom(RoomType type);
        void CheckOut(int roomNumber);
        void DisplayRoomStatus();
    }
}
