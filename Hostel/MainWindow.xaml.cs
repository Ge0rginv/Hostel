using System;
using System.Windows;
using System.Windows.Controls;

namespace Hostel
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BookRoom_Click(object sender, RoutedEventArgs e)
        {
            // Логика для бронирования номера
            string customerName = CustomerNameTextBox.Text;
            string roomType = ((ComboBoxItem)RoomTypeComboBox.SelectedItem).Content.ToString();
            DateTime checkInDate = CheckInDatePicker.SelectedDate ?? DateTime.Now;
            DateTime checkOutDate = CheckOutDatePicker.SelectedDate ?? DateTime.Now;

            // Здесь добавьте логику для бронирования
            // Например, вызов метода для бронирования и обновление статуса

            RoomStatusTextBox.Text += $"Бронирование для {customerName} в {roomType} с {checkInDate.ToShortDateString()} по {checkOutDate.ToShortDateString()}\n";
        }

        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            // Логика для оформления выезда
            // Например, вызов метода для оформления выезда и обновление статуса

            RoomStatusTextBox.Text += $"Клиент {CustomerNameTextBox.Text} выехал.\n";
        }
    }
}
