

namespace ClassLibrary
{
    public class BookingItem
    {
        public Room room { get; set; }
        public int FirstDay { get; set; }
        public int LastDay { get; set; }
        public BookingItem(Room _Room, int firstDay, int lastDay) 
        {
            room = _Room;
            FirstDay = firstDay;
            LastDay = lastDay;
        }
        public double Cost()
        {
            return (LastDay - FirstDay) * room._ICost.Cost(room);
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
