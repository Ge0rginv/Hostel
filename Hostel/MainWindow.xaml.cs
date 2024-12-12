using Hostel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HotelBooking
{
    public partial class MainWindow : Window
    {
        Modeling modeling = new Modeling();
        bool ReadInf = false;
        public MainWindow()
        {
            InitializeComponent();

          
        }

        // Обработчик для кнопки "Изменить данные"
        private void ChangeDataButton_Click(object sender, RoutedEventArgs e)
        {
            ReadInf = true;
            ShowSection("DataEntry");
            // в массивы загрузить информацию из формы : 
            /*
            public int M { get; set; } // количество дней моделирования
            public int time_step { get; set; } // промежуток появления заявок
            public int[] CountRooms { get; set; } // количество номеров каждого типа
            public double[] CostRooms { get; set; } // стоимость каждого типа номеров
            */
        }

        // Обработчик для кнопки "Старт"
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if(!ReadInf) modeling = new Modeling();
            ShowSection("Results");
            ResultsTextBox.Text = "";
            modeling.Start();
            string res = modeling.GetInf();
            // ResultsTextBox сделать шире, чтобы все данные было хорошо видно
            ResultsTextBox.Text = res;
            ReadInf = false;
        }

        // Обработчик для кнопки "Назад"
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSection("Main");
            //modeling = new Modeling();
        }

        // Обработчик для кнопки "Начать генерацию"
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            // надо ли это сообщение? если нет, то убирай
            MessageBox.Show("Генерация данных началась! Проверьте результаты позже.", "Генерация", MessageBoxButton.OK, MessageBoxImage.Information);
            ShowSection("Results");
            if (!ReadInf) modeling = new Modeling();
            ///
            modeling.M = (int)DaysSlider.Value;// количество дней
            modeling.time_step = (int)TimeSlider.Value;// промежуток подачи 
            modeling.CountRooms[0]=(int)PriseSingle.Value;
            modeling.CountRooms[1]=(int)PriceDouble.Value;
            modeling.CountRooms[2] =(int)PriceSuite.Value;
            modeling.CountRooms[3] =(int)PriceHalfSuite.Value;
            modeling.CountRooms[4] = (int)PriceDoubleWithSofa.Value;
            modeling.CostRooms[0] = int.Parse(CntDouble.Text);
            modeling.CostRooms[1] = int.Parse(CntSingle.Text);
            modeling.CostRooms[2] = int.Parse(CntSuite.Text);
            modeling.CostRooms[3] = int.Parse(CntHalfSuite.Text);
            modeling.CostRooms[4] = int.Parse(CntDoubleWithSofa.Text);



            ResultsTextBox.Text = "";
            modeling.Start();
            string res = modeling.GetInf();
            ResultsTextBox.Text = res;
        }

        // Метод для управления видимостью секций
        private void ShowSection(string section)
        {
            MainScreen.Visibility = section == "Main" ? Visibility.Visible : Visibility.Collapsed;
            DataEntrySection.Visibility = section == "DataEntry" ? Visibility.Visible : Visibility.Collapsed;
            ResultsSection.Visibility = section == "Results" ? Visibility.Visible : Visibility.Collapsed;
        }

       

        private void PriseSingle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;

        }

        private void PriceDouble_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;

        }

        private void PriceSuite_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;

        }

        private void PriceHalfSuite_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;

        }

        private void PriceDoubleWithSofa_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;

        }

        private void TimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;
        }

        private void DaysSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;   
        }

        private void CntSingle_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CntDouble_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CntSuite_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CntHalfSuite_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CntDoubleWithSofa_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

    // Модель для данных о номерах
    // я не использовала, если и тебе не надо, то можешь убрать
    public class RoomDetails
    {
        public string RoomType { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }


    public class Modeling // класс моделирования
    {
        public int time { get; set; } // переменная времени в часах
        public int M { get; set; } // количество дней моделирования
        public int time_step { get; set; } // промежуток появления заявок
        public int[] CountRooms { get; set; } // количество номеров каждого типа
        public double[] CostRooms { get; set; } // стоимость каждого типа номеров
        public RequestGenerator requestGenerator { get; set; } // класс для генерации случайных данных
        public List<List<double>> persent_busy_typeofrooms { get; set; } // лист с процентами занятости каждого типа номера, нужен для получения итоговых результатов моделирования
        public int[] cnt_buzy { get; set; } // количество занятых номеров каждого типа по индексам
        public int goodRequest { get; set; } // количество успешно обработанных заявок
        public int bedRequest { get; set; } // количество необработанных заявок
        public Hotel hostel { get; set; } // сам класс хостела
        public int SumBuzyRooms { get; set; } // суммарное количество занятых номеров
        public List<double> MidSumBuzyRooms { get; set; } // лист с процентами занятости всей гостиницы

        public Modeling()
        {
            time = 0;
            requestGenerator = new RequestGenerator();
            M = requestGenerator.RandomNumberCreatorInSection(20, 30);
            time_step = requestGenerator.RandomNumberCreatorInSection(1, 5);
            persent_busy_typeofrooms = new List<List<double>>();
            for (int i = 0; i < 5; ++i)
            {
                persent_busy_typeofrooms.Add(new List<double>());
            }
            cnt_buzy = new int[5];
            for (int i = 0; i < 5; ++i)
            {
                cnt_buzy[i] = 0;
            }
            CountRooms = new int[5];
            for (int i = 0; i < 5; ++i)
            {
                CountRooms[i] = requestGenerator.RandomNumberCreatorInSection(1, 30);
            }
            CostRooms = new double[5];
            double startCost = requestGenerator.RandomNumberCreatorInSection(1, 10000) + requestGenerator.RandomNumberCreatorDouble();
            for (int i = 0; i < 5; ++i)
            {
                CostRooms[i] = startCost * (1 + requestGenerator.RandomNumberCreatorDouble());
            }
            goodRequest = 0;
            bedRequest = 0;
            SumBuzyRooms = 0;
            MidSumBuzyRooms = new List<double>();
        }
        public void Start()
        {
            // словарь для облегчения работы с типами номеров
            Dictionary<int, string> important = new Dictionary<int, string>();
            important.Add(0, "Single");
            important.Add(1, "HalfSuite");
            important.Add(2, "Suite");
            important.Add(3, "Double");
            important.Add(4, "DoubleWithSofa");

            int sum = 0;
            for (int i = 0; i < 5; i++)
                sum += CountRooms[i];

            //reception = new Reception(CountRooms, CostRooms);
            hostel = new Hotel(CountRooms, CostRooms);
            while (time < M * 24)
            {
                if (time % time_step == 0) // генерация заявки
                {
                    string type = requestGenerator.room_type_random();
                    int checkInDate = requestGenerator.day(time / 24, M);
                    int checkOutDate = requestGenerator.day(checkInDate, M);
                    int res = hostel.BookRoom(type, checkInDate, checkOutDate);
                    Console.WriteLine($"{time}  {type} {checkInDate} {checkOutDate}");
                    if (res == -1) ++bedRequest;
                    else ++goodRequest;
                }

                List<int> del = new List<int>();
                for (int i = 0; i < hostel.occupied.Count; i++)
                {
                    if (hostel.occupied.Values[i] == time / 24)
                        del.Add(hostel.occupied.Keys[i]);

                }

                // проверка какой номер занят
                // пересчет процента загруженности отдельных номеров
                SumBuzyRooms = 0;
                for (int i = 0; i < 5; ++i)
                {
                    cnt_buzy[i] = hostel.buzyRooms(important[i], time);
                    SumBuzyRooms += cnt_buzy[i];
                    persent_busy_typeofrooms[i].Add(cnt_buzy[i] * 100 / CountRooms[i]);
                }

                // ??? статистика заселения номмеров - мб количество занятых сейчас
                // счет количество занятых номеров
                MidSumBuzyRooms.Add(SumBuzyRooms * 100 / sum);



                //ResultsTextBox.Text = "Здесь будет отображена информация о доступных номерах.";
                // день или время (день + время 24ч или время в часах с начала моделирования
                // статистика выполненных заявок: хорошие заявки ко всем заявкам
                // по категориям процент загруженности по формуле : (занятые номера данного типа * 100) / (количество номеров данного типа)
                // процент загруженности гостиницы : (занятые номера * 100) / (количество номеров)
                // 
                ++time;
            }

        }
        public string GetInf()
        {
            // статистика выполненных заявок: хорошие заявки ко всем заявкам
            // по категориям процент загруженности по формуле : (занятые номера данного типа * 100) / (количество номеров данного типа)
            // процент загруженности гостиницы : (занятые номера * 100) / (количество номеров)
            string resalt = ""; // строка с итоговой информацией полученной в ходе моделирования
            double x;
            string[] names ={
                "Single/одноместный",
                "Double/двухместный",
                "Suite/люкс",
                "HalfSuite/полулюкс",
                "DoubleWithSofa/двухместный с диваном"
            };


            //    M { get; set; } // количество дней моделирования
            //    time_step { get; set; } // промежуток появления заявок
            //    CountRooms { get; set; } // количество номеров каждого типа
            //    CostRooms { get; set; } // стоимость каждого типа номеров


            resalt += "Количество дней моделирования : " + M.ToString() + "\n";
            resalt += "Промежуток появления заявок : " + time_step.ToString() + "\n";
            resalt += "Количество номеров каждого типа : " + "\n";
            for (int i = 0; i < 5; ++i)
            {
                resalt += " * " + names[i] + ' ' + CountRooms[i].ToString() + '\n';
            }
            resalt += "Стоимость каждого типа номеров : " + "\n";
            for (int i = 0; i < 5; ++i)
            {
                resalt += " * " + names[i] + ' ' + Math.Round(CostRooms[i] * 100 / 100).ToString() + '\n';
            }


            resalt += "=========== Результаты моделирования =========== \n";

            //resalt += "Статистика заселения номеров: \n";
            // метод взятия данных

            resalt += "Статистика выполненных заявок: \n";
            if (goodRequest == 0) x = 100;
            else
                x = (goodRequest) * 100 / (goodRequest + bedRequest);
            resalt += x.ToString() + "% \n";

            resalt += "Процент загруженности : \n";

            resalt += "* одноместных номеров: \n";
            x = 0;
            for (int i = 0; i < persent_busy_typeofrooms[0].Count; ++i)
                x += persent_busy_typeofrooms[0][i];
            x /= persent_busy_typeofrooms[0].Count;
            resalt += Math.Round(x * 100 / 100).ToString() + " %\n";

            resalt += "* двухместных номеров: \n";
            x = 0;
            for (int i = 0; i < persent_busy_typeofrooms[3].Count; ++i)
                x += persent_busy_typeofrooms[3][i];
            x /= persent_busy_typeofrooms[3].Count;
            resalt += Math.Round(x * 100 / 100).ToString() + " %\n";

            resalt += "* полулюкс номеров: \n";
            x = 0;
            for (int i = 0; i < persent_busy_typeofrooms[1].Count; ++i)
                x += persent_busy_typeofrooms[1][i];
            x /= persent_busy_typeofrooms[1].Count;
            resalt += Math.Round(x * 100 / 100).ToString() + " %\n";

            resalt += "* люкс номеров: \n";
            x = 0;
            for (int i = 0; i < persent_busy_typeofrooms[2].Count; ++i)
                x += persent_busy_typeofrooms[2][i];
            x /= persent_busy_typeofrooms[2].Count;
            resalt += Math.Round(x * 100 / 100).ToString() + " %\n";

            resalt += "* двухместных с диваном номеров: \n";
            x = 0;
            for (int i = 0; i < persent_busy_typeofrooms[4].Count; ++i)
                x += persent_busy_typeofrooms[4][i];
            x /= persent_busy_typeofrooms[4].Count;
            resalt += Math.Round(x * 100 / 100).ToString() + " %\n";

            resalt += "Процент загруженности гостиницы: \n";
            x = 0;
            for (int i = 0; i < MidSumBuzyRooms.Count; ++i)
                x += MidSumBuzyRooms[i];
            x /= MidSumBuzyRooms.Count;
            resalt += Math.Round(x * 100 / 100).ToString() + " %\n";

            return resalt;
        }
    }
    public class RequestGenerator // класс генерации случайных данных для заявок
    {
        private Random _random = new Random();

        public string room_type_random() // генерация типа номера для бронирования
        {
            Dictionary<int, string> important = new Dictionary<int, string>();
            important.Add(1, "Single");
            important.Add(2, "HalfSuite");
            important.Add(3, "Suite");
            important.Add(4, "Double");
            important.Add(5, "DoubleWithSofa");
            int num = _random.Next(1, 5);
            return important[num];
        }
        public int day(int first, int last) // генерация дней заселения
        {
            int num = _random.Next(first, last);
            return num;
        }
        public int RandomNumberCreatorInSection(int a, int b) // генерация случайного числа из заданного промежутка
        {
            int num = _random.Next(a, b);
            return num;
        }
        public double RandomNumberCreatorDouble() // генерация случайного числа вещественного типа
        {
            return _random.NextDouble();
        }

    }
}

