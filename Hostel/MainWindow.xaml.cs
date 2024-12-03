using Hostel;
using System.Collections.Generic;
using System.Windows;

namespace HotelBooking
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Пример начальных данных для DataGrid
            RoomDataGrid.ItemsSource = new List<RoomDetails>
            {
                new RoomDetails { RoomType = "Single", Quantity = 5, Price = 100 },
                new RoomDetails { RoomType = "Double", Quantity = 3, Price = 150 },
                new RoomDetails { RoomType = "Suite", Quantity = 1, Price = 113 },
                new RoomDetails { RoomType = "HalfSuite", Quantity = 3, Price = 170 },
                new RoomDetails { RoomType = "DoubleWithSofa", Quantity = 2, Price = 140 },
                new RoomDetails { RoomType = "Double", Quantity = 3, Price = 159 }
            };
        }

        // Обработчик для кнопки "Изменить данные"
        private void ChangeDataButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSection("DataEntry");
        }

        // Обработчик для кнопки "Старт"
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSection("Results");
            ResultsTextBox.Text = "Здесь будет отображена информация о доступных номерах.";
        }

        // Обработчик для кнопки "Назад"
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSection("Main");
        }

        // Обработчик для кнопки "Начать генерацию"
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Генерация данных началась! Проверьте результаты позже.", "Генерация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Метод для управления видимостью секций
        private void ShowSection(string section)
        {
            MainScreen.Visibility = section == "Main" ? Visibility.Visible : Visibility.Collapsed;
            DataEntrySection.Visibility = section == "DataEntry" ? Visibility.Visible : Visibility.Collapsed;
            ResultsSection.Visibility = section == "Results" ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    // Модель для данных о номерах
    public class RoomDetails
    {
        public string RoomType { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
