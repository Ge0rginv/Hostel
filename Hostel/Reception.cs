using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel
{
    internal class Reception
    {
        public int TotalRooms { get; }
        public Hotel hostel { get; }
        Reception(int totalRooms) 
        {
            TotalRooms = totalRooms;
            hostel = new Hotel(TotalRooms);
        }
        
    }
}
