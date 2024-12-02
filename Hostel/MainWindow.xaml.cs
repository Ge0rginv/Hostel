using System.Windows;

namespace HotelBooking
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик для кнопки "Изменить данные"
        private void ChangeDataButton_Click(object sender, RoutedEventArgs e)
        {
            // Скрываем главные кнопки
            MainScreen.Visibility = Visibility.Collapsed;

            // Показываем элементы для изменения данных
            DataEntrySection.Visibility = Visibility.Visible;
        }

        // Обработчик для кнопки "Старт"
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Скрываем главные кнопки и элементы для изменения данных
            MainScreen.Visibility = Visibility.Collapsed;
            DataEntrySection.Visibility = Visibility.Collapsed;

            // Показываем результаты
            ResultsSection.Visibility = Visibility.Visible;

            // Пример текста в результатах
            ResultsTextBox.Text = "Здесь будут отображены доступные номера.";
        }
    }
}
