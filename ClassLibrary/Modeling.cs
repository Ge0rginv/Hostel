using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public struct RoomAndDayOfEviction : IComparable<RoomAndDayOfEviction>
    {
        public int First;
        public int Second;
        public RoomAndDayOfEviction(int first, int second)
        {
            this.First = first;
            this.Second = second;
        }
        public int CompareTo(RoomAndDayOfEviction point)
        {
            if (this.First == point.First)
                return Second.CompareTo(point.Second);
            return First.CompareTo(point.First);
        }
        public override string ToString()
        {
            return string.Format($"{First} {Second}");
        }
    }
    public class Modeling // класс моделирования
    {
        public int time { get; set; } // переменная времени в часах
        public int M { get; set; } // количество дней моделирования
        public int TimeStep { get; set; } // промежуток появления заявок
        public Dictionary<RoomType, int> CountRooms { get; set; } // количество номеров каждого типа
        public Dictionary<RoomType, double> CostRooms { get; set; } // стоимость каждого типа номеров
        //public RequestGenerator requestGenerator { get; set; } // класс для генерации случайных данных
        public Dictionary<RoomType,List<int>> PersentBusyTypeOfRooms { get; set; } // лист с процентами занятости каждого типа номера, нужен для получения итоговых результатов моделирования
        public int goodRequest { get; set; } // количество успешно обработанных заявок
        public int bedRequest { get; set; } // количество необработанных заявок
        public Hotel hostel { get; set; } // сам класс хостела
        public int SumBuzyRooms { get; set; } // суммарное количество занятых номеров
        public List<int> MidSumBuzyRooms { get; set; } // лист с процентами занятости всей гостиницы
        public double FinalCost { get; set; }
        private Dictionary<int, RoomType> important; // словарь для облегчения работы с типами номеров

        public Modeling()
        {
            FinalCost = 0;
            CreateModel();
            SumBuzyRooms = 0;
            MidSumBuzyRooms = new List<int>(); 
            important = new Dictionary<int, RoomType>();
            important.Add(0, RoomType.Single);
            important.Add(1, RoomType.HalfSuite);
            important.Add(2, RoomType.Suite);
            important.Add(3, RoomType.Double);
            important.Add(4, RoomType.DoubleWithSofa);
        }

        public Modeling(int m, int timeStep, Dictionary<RoomType, int> countRooms, Dictionary<RoomType, double> costRooms)
        {
            FinalCost = 0;
            M = m;
            TimeStep = timeStep;
            
            CountRooms = countRooms;
            CostRooms = costRooms;
            goodRequest = 0;
            bedRequest = 0;
            SumBuzyRooms = 0;
            MidSumBuzyRooms = new List<int>();

            hostel = new Hotel(CountRooms, CostRooms);

            important = new Dictionary<int, RoomType>();
            important.Add(0, RoomType.Single);
            important.Add(1, RoomType.HalfSuite);
            important.Add(2, RoomType.Suite);
            important.Add(3, RoomType.Double);
            important.Add(4, RoomType.DoubleWithSofa);
            PersentBusyTypeOfRooms = new Dictionary<RoomType, List<int>>();
            for (int i = 0; i < countRooms.Count; ++i)
            {
                PersentBusyTypeOfRooms.Add(important[i], new List<int>());
            }
        }

        public List<string> Start(bool newInf)
        {
            List<string> result = new List<string>();
            time = 0;
            FinalCost = 0;
            goodRequest = 0;
            bedRequest = 0;
            if (!newInf)
            {
                CreateModel();
            }
            RoomGenerator newRoom = new RoomGenerator(new RequestGenerator());

            while (time < M * 24)
            {
                if (time % 24 == 14)
                {
                    for (int i = 0; i < hostel.bookingItems.Count; ++i)
                    {
                        if (hostel.bookingItems[i].FirstDay == time / 24)
                        {
                            if (hostel.CheckIn(hostel.bookingItems[i]))
                                ++goodRequest;
                            //else ++bedRequest;
                        }
                    }
                }
                if (time % 24 == 12) 
                {
                    FinalCost += hostel.CheckOut(time / 24);
                }



                if (time % TimeStep == 0) // генерация заявки
                {
                    newRoom.GenerateRoom(time, M);
                    hostel.BookRoom(newRoom.type, newRoom.checkInDate, newRoom.checkOutDate);
                }


                if (time % 24 == 23)
                {
                    SumBuzyRooms = 0;
                    foreach (RoomType room in Enum.GetValues(typeof(RoomType))) // перебор всех типов комнат
                    {
                        int cntBuzy = hostel.buzyRooms(room);
                        SumBuzyRooms += cntBuzy;
                        PersentBusyTypeOfRooms[room].Add(cntBuzy);
                    }
                    MidSumBuzyRooms.Add(SumBuzyRooms);

                    result.Add(Inform());
                }
                ++time;
            }
            bedRequest = hostel.bookingItems.Count;
            return result;
        }

        public string Inform()
        {
            /*
            <текущая дата>
            Список событий:
            <номер> <тип> <время проживания(не засиления и отсиления, а количество часов)(только при выселении)> 
            */

            string inf = "";
            inf += "Текущий день: " + (time / 24 + 1).ToString() + '\n';
            inf += "Список событий:\n";

            foreach (RoomType room in Enum.GetValues(typeof(RoomType))) // перебор всех типов комнат
            {
                for(int i = 0; i < hostel.rooms[room].Count; ++i)
                {
                    inf += hostel.rooms[room][i].Number.ToString() + "     " + room.ToString() + "     ";
                    if(hostel.rooms[room][i].Occupied) inf += "занят" + "     ";
                    else inf += "свободен" + "     ";
                    inf += '\n';
                }

            }
            return inf;
        }
        public void CreateModel()
        {
            M = RandomForModelInitialization.RandomNumberCreatorInSection(20, 30);
            TimeStep = RandomForModelInitialization.RandomNumberCreatorInSection(1, 5);
            CountRooms = new Dictionary<RoomType, int>();
            foreach (RoomType room in Enum.GetValues(typeof(RoomType))) // перебор всех типов комнат
            {
                CountRooms.Add(room, RandomForModelInitialization.RandomNumberCreatorInSection(1, 30));
            }
            CostRooms = new Dictionary<RoomType, double>();
            double startCost = RandomForModelInitialization.RandomNumberCreatorInSection(1, 10000) + RandomForModelInitialization.RandomNumberCreatorDouble();
            foreach (RoomType room in Enum.GetValues(typeof(RoomType))) // перебор всех типов комнат
            {
                CostRooms.Add(room, startCost * (1 + RandomForModelInitialization.RandomNumberCreatorDouble()));
            }

            PersentBusyTypeOfRooms = new Dictionary<RoomType, List<int>>();
            foreach (RoomType room in Enum.GetValues(typeof(RoomType))) // перебор всех типов комнат
            {
                PersentBusyTypeOfRooms.Add(room,new List<int>());
            }
            goodRequest = 0;
            bedRequest = 0;
            SumBuzyRooms = 0;
            MidSumBuzyRooms = new List<int>();
            hostel = new Hotel(CountRooms, CostRooms);
        }
    }
}
