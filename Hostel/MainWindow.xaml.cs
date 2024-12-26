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
using ClassLibrary;

namespace HotelBooking
{
    public class TextShower : INotifyPropertyChanged
    {
        string sourceData;
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "A")
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

            textInput = new TextShower() { SourceData = "" };
            this.DataContext = textInput;
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
            textInput.SourceData = "";
            List<string> res = modeling.Start(ReadInf);
            await Task.Run(() =>
            {
                for (int i = 0; i < res.Count; ++i)
                {
                    Dispatcher.Invoke(() => { textInput.SourceData = res[i]; });
                    for (int j = 0; j < 3 && !StopOutput; j++)
                    {
                        Thread.Sleep(500);
                    }
                }
            });
            GetInformation getInformation = new GetInformation();
            getInformation.GetInf(modeling);
            textInput.SourceData = getInformation.result;
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
            modeling = new Modeling((int)DaysSlider.Value, (int)TimeSlider.Value, GetCountRooms(), GetCostRooms());

            MessageBox.Show("Генерация данных началась! Проверьте результаты позже.", "Генерация", MessageBoxButton.OK, MessageBoxImage.Information);

            StopOutput = false;
            if (!ReadInf) modeling = new Modeling();
            ShowSection("Results");
            textInput.SourceData = "";
            List<string> res = modeling.Start(ReadInf);
            await Task.Run(() =>
            {
                for (int i = 0; i < res.Count; ++i)
                {
                    Dispatcher.Invoke(() => { textInput.SourceData = res[i]; });
                    for (int j = 0; j < 3 && !StopOutput; j++)
                    {
                        Thread.Sleep(500);
                    }
                }
            });
            GetInformation getInformation = new GetInformation();
            getInformation.GetInf(modeling);
            textInput.SourceData = getInformation.result;
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
        public Dictionary<RoomType, double> GetCostRooms()
        {

            Dictionary<RoomType, double> costRooms = new Dictionary<RoomType, double>();
            int cntSingle, cntDouble, cntSuite, cntHalfSuite, cntDoubleWithSofa;
            if (CntSingle.Text == "")
            {
                cntSingle = 100;
            }
            else
            {
                cntSingle = int.Parse(CntSingle.Text);
            }
            costRooms.Add(RoomType.Single, cntSingle);
            if (CntDouble.Text == "")
            {
                cntDouble = 100;
            }
            else
            {
                cntDouble = int.Parse(CntDouble.Text);
            }
            costRooms.Add(RoomType.Double, cntDouble);
            if (CntSuite.Text == "")
            {
                cntSuite = 100;
            }
            else
            {
                cntSuite = int.Parse(CntSuite.Text);
            }
            costRooms.Add(RoomType.Suite, cntSuite);
            if (CntHalfSuite.Text == "")
            {
                cntHalfSuite = 100;
            }
            else
            {
                cntHalfSuite = int.Parse(CntHalfSuite.Text);
            }
            costRooms.Add(RoomType.HalfSuite, cntHalfSuite);
            if (CntDoubleWithSofa.Text == "")
            {
                cntDoubleWithSofa = 100;
            }
            else
            {
                cntDoubleWithSofa = int.Parse(CntDoubleWithSofa.Text);
            }
            costRooms.Add(RoomType.DoubleWithSofa, cntDoubleWithSofa);
            return costRooms;
        }
        public Dictionary<RoomType, int> GetCountRooms()
        {

            Dictionary<RoomType, int> countRooms = new Dictionary<RoomType, int>();
            countRooms.Add(RoomType.Single, (int)PriseSingle.Value);
            countRooms.Add(RoomType.Double, (int)PriceDouble.Value);
            countRooms.Add(RoomType.Suite, (int)PriceSuite.Value);
            countRooms.Add(RoomType.HalfSuite, (int)PriceHalfSuite.Value);
            countRooms.Add(RoomType.DoubleWithSofa, (int)PriceDoubleWithSofa.Value);
            return countRooms;
        }
    }
}

