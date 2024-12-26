using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class GetInformation
    {
        public string result {  get; set; }
        public string[] names { get; set; }
        public GetInformation() 
        {
            result = "";
            names = new string[] {
                "Single/одноместный",
                "Double/двухместный",
                "Suite/люкс",
                "HalfSuite/полулюкс",
                "DoubleWithSofa/двухместный с диваном"
            };
        }
        public void GetInf(Modeling model)
        {
            // статистика выполненных заявок: хорошие заявки ко всем заявкам
            // по категориям процент загруженности по формуле : (занятые номера данного типа * 100) / (количество номеров данного типа)
            // процент загруженности гостиницы : (занятые номера * 100) / (количество номеров)
            result = ""; // строка с итоговой информацией полученной в ходе моделирования
            double AuxiliaryVariable;

            result += "=========== Исходные данные моделирования =========== \n";
            result += "Количество дней моделирования : " + model.M.ToString() + "\n";
            result += "Промежуток появления заявок : " + model.TimeStep.ToString() + "\n";
            result += "Информация о номерах каждого типа : " + "\n";


            foreach (RoomType room in Enum.GetValues(typeof(RoomType))) // перебор всех типов комнат
            {
                result += " * " + room.ToString() + "   \n     Количество: " + model.CountRooms[room].ToString();
                result += "    Цена за номер: " + (Math.Round(model.CostRooms[room] * 100) / 100.0).ToString() + '\n';
                
            }

            result += "=========== Результаты моделирования =========== \n";

            result += "Полученная прибыль: " + (Math.Round(model.FinalCost * 100) / 100.0).ToString() + '\n';

            result += "Статистика выполненных заявок: \n";
            if (model.goodRequest == 0) AuxiliaryVariable = 0;
            else
                AuxiliaryVariable = (model.goodRequest) * 100.0 / (model.goodRequest + model.bedRequest);
            result += (Math.Round(AuxiliaryVariable*100)/100.0).ToString() + "% \n";

            result += "Процент загруженности : \n";
            foreach (RoomType room in Enum.GetValues(typeof(RoomType))) // перебор всех типов комнат
            {
                result += " * " + room.ToString() + "  ";
                AuxiliaryVariable = 0;
                for (int i = 0; i < model.PersentBusyTypeOfRooms[room].Count; ++i)
                    AuxiliaryVariable += (double)model.PersentBusyTypeOfRooms[room][i] / (double)model.CountRooms[room];
                AuxiliaryVariable /= model.PersentBusyTypeOfRooms[room].Count;
                result += Math.Round(AuxiliaryVariable * 100).ToString() + " %\n";
                result += "    Цена за номер: " + (Math.Round(model.CostRooms[room] * 100) / 100.0).ToString() + '\n';

            }

            result += "Процент загруженности гостиницы: \n";
            AuxiliaryVariable = TheNumberOfAllRooms(model.CountRooms, model.MidSumBuzyRooms);
            result += Math.Round(AuxiliaryVariable * 100).ToString() + " %\n";

        }
        public double TheNumberOfAllRooms(Dictionary<RoomType, int> CountRooms, List<int> MidSumBuzyRooms)
        {
            double AuxiliaryVariable = 0;
            double sumCount = 0;
            foreach(var roomType in CountRooms)
            {
                sumCount += roomType.Value;
            }
            for (int i = 0; i < MidSumBuzyRooms.Count; ++i)
                AuxiliaryVariable += MidSumBuzyRooms[i] / sumCount;
            AuxiliaryVariable /= MidSumBuzyRooms.Count;
            return AuxiliaryVariable;
        }
    }
}
/*
 foreach (RoomType room in Enum.GetValues(typeof(RoomType))) // перебор всех типов комнат
            {
                for (int i = 0; i < CountRooms[room]; ++i)
                {
                    if (!rooms[room][i].Occupied)
                    {
                        rooms[room][i].Occupied = false;
                }
            }
}
 */