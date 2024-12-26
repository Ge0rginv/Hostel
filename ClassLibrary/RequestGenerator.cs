using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{

    public interface IRequestGenerator
    {
        RoomType RoomTypeRandom();
        int GenerationRandomDay(int first, int last);
    }

    public class RequestGenerator :IRequestGenerator// класс генерации случайных данных для заявок
    {
        private Random _random = new Random();
        private Dictionary<int, RoomType> important;
        public RequestGenerator()
        {
            important = new Dictionary<int, RoomType>();
            int i = 1;
            foreach (RoomType roomType in Enum.GetValues(typeof(RoomType)))
            {
                important.Add(i, roomType);
                i++;
            }
            //important.Add(1, RoomType.Single);
            //important.Add(2, RoomType.HalfSuite);
            //important.Add(3, RoomType.Suite);
            //important.Add(4, RoomType.Double);
            //important.Add(5, RoomType.DoubleWithSofa);
        }
        public RoomType RoomTypeRandom() // генерация типа номера для бронирования
        {
            int num = _random.Next(1, important.Count + 1);
            return important[num];
        }
        public int GenerationRandomDay(int first, int last) // генерация дней заселения
        {
            int num = _random.Next(first, last);
            return num;
        }
        
    }

    public static class RandomForModelInitialization
    {
        static Random rnd = new Random();
        public static int RandomNumberCreatorInSection(int a, int b) // генерация случайного числа из заданного промежутка
        {
            int num = rnd.Next(a, b);
            return num;
        }
        public static double RandomNumberCreatorDouble() // генерация случайного числа вещественного типа
        {
            return rnd.NextDouble();
        }
    }
}
