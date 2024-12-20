using Hostel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HotelBooking
{
    public class TextShower : INotifyPropertyChanged
    {
        string sourceData;
        //CancellationToken token = (CancellationToken)obj;
        public string SourceData
        {
            get
            {
                return sourceData;
            }
            set
            {
                sourceData = value;
                NotifyPropertyChanged();
            }
        }

        //public bool IsCancellationRequested { get; internal set; }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public partial class MainWindow : Window
    {
        TextShower textInput;
        Modeling modeling = new Modeling();
        bool ReadInf = false;
        bool StopOutput = false;
        public MainWindow()
        {
            InitializeComponent();

            textInput = new TextShower() { SourceData = "BlaBlaBla" };
            // New binding object using the path of 'Name' for whatever source object is used
            var nameBindingObject = new Binding("SourceData");

            // Configure the binding
            nameBindingObject.Mode = BindingMode.TwoWay;
            nameBindingObject.Source = textInput;
            //nameBindingObject.Converter = NameConverter.Instance;
            nameBindingObject.ConverterCulture = new CultureInfo("ru-RU");

            // Set the binding to a target object. The TextBlock.Name property on the NameBlock UI element
            BindingOperations.SetBinding(ResultsTextBox, TextBox.TextProperty, nameBindingObject);
        }

        // Обработчик для кнопки "Изменить данные"
        private void ChangeDataButton_Click(object sender, RoutedEventArgs e)
        {
            ReadInf = true;
            modeling = new Modeling();
            ShowSection("DataEntry");
        }

        // Обработчик для кнопки "Старт"
        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StopOutput = false;
            if (!ReadInf) modeling = new Modeling();
            ShowSection("Results");
            ResultsTextBox.Text = "";
            List<string> res = modeling.Start();
            await Task.Run(() =>
            {
                for (int i = 0; i < res.Count && !StopOutput; ++i)
                {
                    //textInput.SourceData += $"\nЭто строка номер {i}";
                    textInput.SourceData = res[i];
                    Thread.Sleep(1000);
                }
            });
            ResultsTextBox.Text = modeling.GetInf();
            ReadInf = false;
        }

        // Обработчик для кнопки "Назад"
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSection("Main");
            StopOutput = true;
        }

        // Обработчик для кнопки "Начать генерацию"
        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            ReadInf = true;
            modeling = new Modeling();
            modeling.M = (int)DaysSlider.Value;// количество дней
            modeling.time_step = (int)TimeSlider.Value;// промежуток подачи 
            modeling.CountRooms[0] = (int)PriseSingle.Value;
            modeling.CountRooms[1] = (int)PriceDouble.Value;
            modeling.CountRooms[2] = (int)PriceSuite.Value;
            modeling.CountRooms[3] = (int)PriceHalfSuite.Value;
            modeling.CountRooms[4] = (int)PriceDoubleWithSofa.Value;
            int c0, c1, c2, c3, c4;
            if (CntDouble.Text == "")
            {
                c0 = 100;
            }
            else
            {
                c0 = int.Parse(CntDouble.Text);
            }
            modeling.CostRooms[0] = c0;
            if (CntSingle.Text == "")
            {
                c1 = 100;
            }
            else
            {
                c1 = int.Parse(CntSingle.Text);
            }
            modeling.CostRooms[1] = c1;
            if (CntSuite.Text == "")
            {
                c2 = 100;
            }
            else
            {
                c2 = int.Parse(CntSuite.Text);
            }
            modeling.CostRooms[2] = c2;
            if (CntHalfSuite.Text == "")
            {
                c3 = 100;
            }
            else
            {
                c3 = int.Parse(CntHalfSuite.Text);
            }
            modeling.CostRooms[3] = c3;
            if (CntDoubleWithSofa.Text == "")
            {
                c4 = 100;
            }
            else
            {
                c4 = int.Parse(CntDoubleWithSofa.Text);
            }
            modeling.CostRooms[4] = c4;


            MessageBox.Show("Генерация данных началась! Проверьте результаты позже.", "Генерация", MessageBoxButton.OK, MessageBoxImage.Information);

            StopOutput = false;
            if (!ReadInf) modeling = new Modeling();
            ShowSection("Results");
            ResultsTextBox.Text = "";
            List<string> res = modeling.Start();
            await Task.Run(() =>
            {
                for (int i = 0; i < res.Count && !StopOutput; ++i)
                {
                    //textInput.SourceData += $"\nЭто строка номер {i}";
                    textInput.SourceData = res[i];
                    Thread.Sleep(1000);
                }
            });
            ResultsTextBox.Text = modeling.GetInf();
            ReadInf = false;
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
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return;
            string input = textBox.Text;
            if (string.IsNullOrWhiteSpace(input))
            {
                textBox.Text = "";
                return;
            }

            if (int.TryParse(input, out int price))
            {
  
                if (price <= 0)
                {
                    textBox.Text = "1";
                }
                else if (price > 10000)
                {
                    textBox.Text = "10000";
                }
            }

            else
            {
                textBox.Text = "1";
            }
            

            textBox.SelectionStart = textBox.Text.Length;
        }

        private void CntDouble_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return;
            string input = textBox.Text;
            if (string.IsNullOrWhiteSpace(input))
            {
                textBox.Text = "";
                return;
            }

            if (int.TryParse(input, out int price))
            {

                if (price <= 0)
                {
                    textBox.Text = "1";
                }
                else if (price > 10000)
                {
                    textBox.Text = "10000";
                }
            }

            else
            {
                textBox.Text = "1";
            }


            textBox.SelectionStart = textBox.Text.Length;
        }

        private void CntSuite_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return;
            string input = textBox.Text;
            if (string.IsNullOrWhiteSpace(input))
            {
                textBox.Text = "";
                return;
            }

            if (int.TryParse(input, out int price))
            {

                if (price <= 0)
                {
                    textBox.Text = "1";
                }
                else if (price > 10000)
                {
                    textBox.Text = "10000";
                }
            }

            else
            {
                textBox.Text = "1";
            }


            textBox.SelectionStart = textBox.Text.Length;
        }

        private void CntHalfSuite_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return;
            string input = textBox.Text;
            if (string.IsNullOrWhiteSpace(input))
            {
                textBox.Text = "";
                return;
            }

            if (int.TryParse(input, out int price))
            {

                if (price <= 0)
                {
                    textBox.Text = "1";
                }
                else if (price > 10000)
                {
                    textBox.Text = "10000";
                }
            }

            else
            {
                textBox.Text = "1";
            }


            textBox.SelectionStart = textBox.Text.Length;
        }

        private void CntDoubleWithSofa_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return;
            string input = textBox.Text;
            if (string.IsNullOrWhiteSpace(input))
            {
                textBox.Text = "";
                return;
            }

            if (int.TryParse(input, out int price))
            {

                if (price <= 0)
                {
                    textBox.Text = "1";
                }
                else if (price > 10000)
                {
                    textBox.Text = "10000";
                }
            }

            else
            {
                textBox.Text = "1";
            }


            textBox.SelectionStart = textBox.Text.Length;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // закрытие окна
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
            public double final_cost { get; set; }

            public Modeling()
            {
                time = 0;
                final_cost = 0;
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
            public List<string> Start()
            {
                List<string> resalt = new List<string>();
                // словарь для облегчения работы с типами номеров
                Dictionary<int, string> important = new Dictionary<int, string>();
                important.Add(0, "Single");
                important.Add(1, "HalfSuite");
                important.Add(2, "Suite");
                important.Add(3, "Double");
                important.Add(4, "DoubleWithSofa");

                time = 0;
                final_cost = 0;
                requestGenerator = new RequestGenerator();
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
                goodRequest = 0;
                bedRequest = 0;
                SumBuzyRooms = 0;
                MidSumBuzyRooms = new List<double>();

                int sum = 0;
                for (int i = 0; i < 5; i++)
                    sum += CountRooms[i];

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

                    List<pair> del = new List<pair>();
                    for (int i = 0; i < hostel.occupied.Count; i++)
                    {
                        if (hostel.occupied[i].s == time / 24)
                            del.Add(hostel.occupied[i]);

                    }

                    for (int i = 0; i < del.Count; i++)
                    {
                        final_cost += hostel.CheckOut(del[i].f, del[i].s);
                    }
                    del.Clear();

                    SumBuzyRooms = 0;
                    for (int i = 0; i < 5; ++i)
                    {
                        cnt_buzy[i] = hostel.buzyRooms(important[i], time);
                        SumBuzyRooms += cnt_buzy[i];
                        persent_busy_typeofrooms[i].Add(cnt_buzy[i] * 100 / CountRooms[i]);
                    }

                    MidSumBuzyRooms.Add(SumBuzyRooms * 100 / sum);


                    /*
                     Single,//одноместный
        Double,//двухместный
        Suite,//люкс
        HalfSuite,//полулюкс
        DoubleWithSofa//двухместный с диваном
                     */
                    if (time % 24 == 0)
                    {
                        string inf = "";
                        inf += "Текущий день: " + (time / 24 + 1).ToString() + '\n';
                        inf += "Список событий:\n";
                        for (int i = 0; i < hostel.singleRooms.Length; ++i)
                        {
                            inf += hostel.singleRooms[i].Number.ToString() + "     " + "одноместный" + "     " + (Math.Round(hostel.singleRooms[i].Cost() * 100) / 100).ToString() + "     ";
                            if (hostel.singleRooms[i].FirstDay <= time / 24 && hostel.singleRooms[i].LastDay >= time / 24 && hostel.singleRooms[i].Days != 0) inf += "занят" + "     ";
                            else if (hostel.singleRooms[i].Days != 0) inf += "забронирован" + "     ";
                            else inf += "свободен" + "     ";
                            inf += '\n';
                        }
                        for (int i = 0; i < hostel.doubleRooms.Length; ++i)
                        {
                            inf += hostel.doubleRooms[i].Number.ToString() + "     " + "двухместный" + "     " + (Math.Round(hostel.doubleRooms[i].Cost() * 100) / 100).ToString() + "     ";
                            if (hostel.doubleRooms[i].FirstDay <= time / 24 && hostel.doubleRooms[i].LastDay >= time / 24 && hostel.doubleRooms[i].Days != 0) inf += "занят" + "     ";
                            else if (hostel.doubleRooms[i].Days != 0) inf += "забронирован" + "     ";
                            else inf += "свободен" + "     ";
                            inf += '\n';
                        }
                        for (int i = 0; i < hostel.suiteRooms.Length; ++i)
                        {
                            inf += hostel.suiteRooms[i].Number.ToString() + "     " + "люкс" + "     " + (Math.Round(hostel.suiteRooms[i].Cost() * 100) / 100).ToString() + "     ";
                            if (hostel.suiteRooms[i].FirstDay <= time / 24 && hostel.suiteRooms[i].LastDay >= time / 24 && hostel.suiteRooms[i].Days != 0) inf += "занят" + "     ";
                            else if (hostel.suiteRooms[i].Days != 0) inf += "забронирован" + "     ";
                            else inf += "свободен" + "     ";
                            inf += '\n';
                        }


                        for (int i = 0; i < hostel.halfSuiteRooms.Length; ++i)
                        {
                            inf += hostel.halfSuiteRooms[i].Number.ToString() + "     " + "полулюкс" + "     " + (Math.Round(hostel.halfSuiteRooms[i].Cost() * 100) / 100).ToString() + "     ";
                            if (hostel.halfSuiteRooms[i].FirstDay <= time / 24 && hostel.halfSuiteRooms[i].LastDay >= time / 24 && hostel.halfSuiteRooms[i].Days != 0) inf += "занят" + "     ";
                            else if (hostel.halfSuiteRooms[i].Days != 0) inf += "забронирован" + "     ";
                            else inf += "свободен" + "     ";
                            inf += '\n';
                        }


                        for (int i = 0; i < hostel.doubleWithSofaRooms.Length; ++i)
                        {
                            inf += hostel.doubleWithSofaRooms[i].Number.ToString() + "     " + "двухместный с диваном" + "     " + (Math.Round(hostel.doubleWithSofaRooms[i].Cost() * 100) / 100).ToString() + "     ";
                            if (hostel.doubleWithSofaRooms[i].FirstDay <= time / 24 && hostel.doubleWithSofaRooms[i].LastDay >= time / 24 && hostel.doubleWithSofaRooms[i].Days != 0) inf += "занят" + "     ";
                            else if (hostel.doubleWithSofaRooms[i].Days != 0) inf += "забронирован" + "     ";
                            else inf += "свободен" + "     ";
                            inf += '\n';
                        }
                        /*
                         <текущая дата>
                        Список событий:
                        <выселился/заселился> <номер> <тип> <цена за час> <время проживания(не засиления и отсиления, а количество часов)(только при выселении)> 
                        <общая цена(только при выселении)>
                         */

                        resalt.Add(inf);
                        //ResultsTextBox.Text = inf;
                        //Thread.Sleep(100);
                    }
                    ++time;
                }
                return resalt;
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

                resalt += "=========== Исходные данные моделирования =========== \n";
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
                    resalt += " * " + names[i] + ' ' + (Math.Round(CostRooms[i] * 100) / 100).ToString() + '\n';
                }


                resalt += "=========== Результаты моделирования =========== \n";

                resalt += "Полученная прибыль: " + (Math.Round(final_cost * 100) / 100).ToString() + '\n';

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
}

