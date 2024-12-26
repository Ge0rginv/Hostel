using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class RoomGenerator
    {
        public RoomType type {  get; set; }
        public int checkInDate { get; set; }
        public int checkOutDate { get; set; }
        public IRequestGenerator requestGenerator { get; set; }
        public RoomGenerator(IRequestGenerator RequestGenerator)
        {
            requestGenerator = RequestGenerator;
        }
        
        public void GenerateRoom(int time, int M)
        {
            type = requestGenerator.RoomTypeRandom();
            checkInDate = requestGenerator.GenerationRandomDay(time / 24, M);
            checkOutDate = requestGenerator.GenerationRandomDay(checkInDate, M);
        }
    }
}
